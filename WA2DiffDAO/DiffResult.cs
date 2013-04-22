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
using System.Xml.Serialization;


namespace Wa2.DaoClasses
{
    [DataContract]
    public class DiffResult
    {

        [DataMember(Order = 0)]
        public LinkedList<Tuple<int, ChangeType, string>> data { get; set; }
        [DataMember(Order = 1)]
        public Boolean isFinished { get; set; }
        [DataMember(Order = 2)]
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
            isFinished = false;
            data = new LinkedList<Tuple<int,ChangeType, string>>();
        }

        public DiffResult() { }

        public void addLine(int lineNo,ChangeType typeOfChange, string line)
        {
            data.AddLast(new Tuple<int,ChangeType, string>(lineNo,typeOfChange, line));
        }

        public LinkedList<Tuple<int, ChangeType, string>> getRawData()
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
            foreach (Tuple<int,ChangeType, String> line in data)
            {
                sb.Append(line.Item1).Append(" ").Append(line.Item2).Append(" ").Append(line.Item3).Append("\n");
            }
            return sb.ToString();
        }

    }
}
