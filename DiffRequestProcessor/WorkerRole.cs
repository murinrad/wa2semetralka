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
using Microsoft.WindowsAzure.StorageClient;
using Wa2.DaoClasses;

namespace DiffRequestProcessor
{
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        const string QueueName = "testQueue";

        // QueueClient is thread-safe. Recommended that you cache 
        // rather than recreating it on every request
        QueueClient Client;
        bool IsStopped;

        public override void Run()
        {
            while (!IsStopped)
            {
                try
                {
                    // Receive the message
                    Trace.WriteLine("HERE1");
                    BrokeredMessage receivedMessage = null;
                    Trace.WriteLine("HERE2");
                    receivedMessage = Client.Receive();

                    Trace.WriteLine("HERE3");

                    if (receivedMessage != null)
                    {
                        // Process the message
                        Trace.WriteLine("Processing", receivedMessage.GetBody<DiffRequest>().original);
                        /*     DiffRequest req = receivedMessage.GetBody<DiffRequest>();
                             Trace.WriteLine(req.original);
                             Trace.WriteLine(req.edited);
                             Trace.WriteLine(req.GetHashCode()+"");
                             receivedMessage.Complete();
                             DiffOP op = new DiffOP();
                            Trace.WriteLine(op.getDiff(req))*/
                        ;
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

            // Create the queue if it does not exist already
            string connectionString = "Endpoint=sb://ordersqueue-ns.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=rZElLWjvRPKxYQu4KML1gBfUghJPzYa8gpmJMTqmf8s=";
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

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
    }
}
