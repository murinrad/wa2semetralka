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
using WA2DiffDAO;
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



        public string putJob(DiffRequest req)
        {
            req.calculateHashCode();
            DiffRequest package = req;
            if (!jobInMemory(package.hash))
            {
                insertIntoTable(package.hash,req);
                queueClient.Send(new BrokeredMessage(req));
               

            }
            return req.hash + "";
        }

        public void insertIntoTable(int hash,DiffRequest req)
        {
            DiffResult newVal = new DiffResult((hash),req);
            DiffResultPersistable p = new DiffResultPersistable(newVal);
            TableOperation op = TableOperation.Insert(p);
            table.Execute(op);
            DiffResult res = getResult(hash + "");
            if (res.data == null)
            {
                throw new Exception();
            }
        }

        private Boolean jobInMemory(int hash)
        {
            TableQuery<DiffResultPersistable> query = new TableQuery<DiffResultPersistable>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, hash + ""));
            IEnumerable<DiffResultPersistable> res = table.ExecuteQuery(query);
            List<DiffResultPersistable> result = res.ToList<DiffResultPersistable>();
            return result.Count != 0;

        }

        public DiffResult getResult(String hash)
        {
            TableQuery<DiffResultPersistable> query = new TableQuery<DiffResultPersistable>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, hash));
            IEnumerable<DiffResultPersistable> res = table.ExecuteQuery(query);
            DiffResult last = new DiffResult(int.Parse(hash),new DiffRequest(new string[0],new string[0]));
            Boolean isThere = false;
            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotModified;
            foreach (DiffResultPersistable r in res)
            {
                isThere = true;
           //     try
             //   {
                    if (r.data.isFinished)
                    {
                        last = r.data;
                        WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    }
             //   }
             //   catch (NullReferenceException ex)
            //    {
                  //  isThere = false;
           //     }
            }
            if (!isThere)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
            }

            last.requestData = null; //should I return the request data?

            return last;





        }
    }
}
