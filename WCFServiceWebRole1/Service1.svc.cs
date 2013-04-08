using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Wa2.DaoClasses;
namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public const String queueName = "testQueue";

        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString"));
        static CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
        static CloudTable table;
        static String connStringToQueue = CloudConfigurationManager.GetSetting("cz.ctu.fee.murinrad.azure.servicebus.ConnectionString");
        static NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(connStringToQueue);
        static QueueClient queueClient;
        static Service1()
        {
            table = tableClient.GetTableReference("MyTestTable");
            table.CreateIfNotExists();
            if (!namespaceManager.QueueExists(queueName))
            {
                namespaceManager.CreateQueue(queueName);
            }
            queueClient = QueueClient.CreateFromConnectionString(connStringToQueue, queueName);
        }

        public List<Players> getAllPlayers()
        {
            Players p = new Players();
            p.Name = "Carlos";
            p.Sports = "MMM";
            p.Country = "Labrador";
            p.ImageUrl = "asdfa";
            List<Players> list = new List<Players>();
            list.Add(p);
            return list;
        }


        public string putJob(DiffRequest req)
        {
            DiffRequest package = req;
            if (!jobInMemory(req.hash))
            {
                queueClient.Send(new BrokeredMessage(req));
                insertIntoTable(req.hash);

            }
            return package.hash + "";
        }

        public void insertIntoTable(int hash)
        {
            DiffResult newVal = new DiffResult((hash));
            TableOperation op = TableOperation.Insert(newVal);
            table.Execute(op);
        }

        private Boolean jobInMemory(int hash)
        {
            TableQuery<DiffResult> query = new TableQuery<DiffResult>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, hash + ""));
            IEnumerable<DiffResult> res = table.ExecuteQuery(query);
            List<DiffResult> result = res.ToList<DiffResult>();
            return result.Count != 0;

        }

        public string getResult(String hash)
        {
            TableQuery<DiffResult> query = new TableQuery<DiffResult>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, hash));
            IEnumerable<DiffResult> res = table.ExecuteQuery(query);
            String last = "N/A";
            Boolean isThere = false;
            foreach (DiffResult r in res)
            {
                last = r.jobID + "";
                isThere = true;

            }
            if (!isThere)
            {
                try
                {
                    DiffResult newVal = new DiffResult(int.Parse(hash));
                    TableOperation op = TableOperation.Insert(newVal);
                    table.Execute(op);
                }
                catch (FormatException e)
                {
                    return "The ID needs to be a number";
                }
                catch (OverflowException e)
                {
                    return "ID value too large";
                }

            }
            return last;
        }
    }
}
