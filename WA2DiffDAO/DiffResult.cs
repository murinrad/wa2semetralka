using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure;
using System.ComponentModel;


namespace Wa2.DaoClasses
{
    [DataContract]
    public class DiffResult : TableEntity
    {

        public LinkedList<Tuple<ChangeType,String>> data;
        public Boolean isFinished { get; set; }
        public int jobID { get; set; }

        public enum ChangeType
        {
            [Description("+")]
            ADDITION,
            [Description("-")]
            REMOVAL,
            [Description("")]
            NO_CHANGE
        }

        public DiffResult(int jobID)
        {
            this.jobID = jobID;
            //rowKey and PartitionKey is inherited from table entity and server as unique identifiers of the object in the hashtable
            this.RowKey = jobID + "";
            this.PartitionKey = jobID + "";
            isFinished = false;
            data = new LinkedList<Tuple<ChangeType, string>>();
        }

        public void addLine(ChangeType typeOfChange, string line)
        {
            data.AddLast(new Tuple<ChangeType, String>(typeOfChange, line));
        }

        public LinkedList<Tuple<ChangeType, String>> getRawData()
        {
            return data;
        }

        public void clearData()
        {
            data.Clear();
        }

        public String toString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Tuple<ChangeType, String> line in data)
            {
                sb.Append(line.Item1).Append(" ").Append(line.Item2).Append("\n");
            }
            return sb.ToString();
        }
        public DiffResult() { }


    }
}
