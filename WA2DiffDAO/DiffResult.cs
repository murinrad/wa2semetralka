using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure;


namespace Wa2.DaoClasses
{
    [DataContract]
    public class DiffResult : TableEntity
    {
        public Boolean isFinished { get; set; }
        public int jobID { get; set; }
        public String diff { get; set; }

        public DiffResult(int jobID)
        {
            this.jobID = jobID;
            this.RowKey = jobID + "";
            this.PartitionKey = jobID + "";
            isFinished = false;
        }

        public DiffResult() { }


    }
}
