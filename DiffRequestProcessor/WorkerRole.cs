using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Wa2.DaoClasses;
using WA2DiffDAO;

namespace DiffRequestProcessor
{
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString"));
        static CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
        static CloudTable table;
        const string QueueName = "testQueue";

        // QueueClient is thread-safe. Recommended that you cache 
        // rather than recreating it on every request
        QueueClient Client;
        bool IsStopped;
        DiffOP operation = new DiffOP();

        public override void Run()
        {
            while (!IsStopped)
            {
                try
                {
                    // Receive the message
                    BrokeredMessage receivedMessage = null;
                    receivedMessage = Client.Receive();
                    if (receivedMessage != null)
                    {
                        // Process the message
                        Trace.WriteLine("Processing");
                        DiffRequest req = receivedMessage.GetBody<DiffRequest>();
                        DiffResult result = operation.getDiff(req);
                        result.isFinished = true;
                        TableQuery<DiffResultPersistable> query = new TableQuery<DiffResultPersistable>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, req.hash + ""));

                        List<DiffResultPersistable> r = table.ExecuteQuery<DiffResultPersistable>(query).ToList<DiffResultPersistable>();
                        if (r.Count == 0) continue; // request expired
                        DiffResultPersistable persistedResult = r[0];
                        
                        persistedResult.data = result;
                        TableOperation insert = TableOperation.InsertOrReplace(persistedResult);
                        table.Execute(insert);
                    }
                }
                catch (MessagingException e)
                {
                    if (!e.IsTransient)
                    {
                        Trace.WriteLine("ERR" + e.Message);
                        throw;
                    }

                    Thread.Sleep(10000);
                }
                catch (OperationCanceledException e)
                {
                    if (!IsStopped)
                    {
                        Trace.WriteLine(e.Message);
                        throw;
                    }
                }
                catch (InvalidOperationException e)
                {
                    Trace.WriteLine(e.Message);
                }

            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;
            table = tableClient.GetTableReference("MyTestTable");
            table.CreateIfNotExists();
            // Create the queue if it does not exist already
            string connectionString = "Endpoint=sb://ordersqueue-ns.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=rZElLWjvRPKxYQu4KML1gBfUghJPzYa8gpmJMTqmf8s=";
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            System.Timers.Timer t = new System.Timers.Timer(1000*60*10);
            t.Elapsed += new System.Timers.ElapsedEventHandler(purgeTableOfOldEntries);
            t.Start();


            // Initialize the connection to Service Bus Queue
            Client = QueueClient.CreateFromConnectionString(connectionString, QueueName);
            IsStopped = false;
            return base.OnStart();
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            IsStopped = true;

            Client.Close();
            base.OnStop();
        }

        public void purgeTableOfOldEntries(Object o,System.Timers.ElapsedEventArgs args)
        {
            TableQuery<DiffResultPersistable> query = new TableQuery<DiffResultPersistable>().Where(TableQuery.GenerateFilterConditionForDate
                ("timestamp", QueryComparisons.LessThan, new DateTimeOffset(Environment.TickCount, new TimeSpan(0).Subtract(new TimeSpan(0, 5, 0)))));
            IEnumerable<DiffResultPersistable> toDelete = table.ExecuteQuery<DiffResultPersistable>(query);
            TableOperation delete;
            foreach (DiffResultPersistable res in toDelete)
            {
                delete = TableOperation.Delete(res);
                table.Execute(delete);
            }
        }
    }
}
