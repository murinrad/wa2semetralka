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
        public LinkedList<ResultLine> data { get; set; }
        [DataMember(Order = 1)]
        public Boolean isFinished { get; set; }
        [DataMember(Order = 2)]
        public int jobID { get; set; }
        [DataMember(Order = 3)]
        public DiffRequest requestData { get; set; }

        

        public DiffResult(int jobID,DiffRequest req)
        {
            this.jobID = jobID;
            //rowKey and PartitionKey is inherited from table entity and server as unique identifiers of the object in the hashtable
            isFinished = false;
            data = new LinkedList<ResultLine>();
            requestData = req;
        }

        public DiffResult() { }

        public void addLine(ResultLine line)
        {
            data.AddLast(line);
        }

        public LinkedList<ResultLine> getRawData()
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
            foreach (ResultLine line in data)
            {
                sb.Append(line.typeOfChange).Append(" ").Append("bogus").Append(" ").Append("bogus").Append("\n");
            }
            return sb.ToString();
        }

    }
    [DataContract]
    public class ResultLine
    {
        public enum ChangeType
        {
            [Description("+")]
            ADDITION,
            [Description("-")]
            REMOVAL,
            [Description("")]
            NO_CHANGE
        }
         [DataMember(Order = 0)]
        public int[] file1Lines {get;set;}
         [DataMember(Order = 1)]
        public int[] file2Lines {get;set;}
         [DataMember(Order = 2)]
        public ChangeType typeOfChange {get;set;}

        public ResultLine(ChangeType typeOfChange, int file1From, int file1To, int file2From, int file2To)       
        {
            file1Lines = new int[2];
            file2Lines = new int[2];
            file1Lines[0] = file1From;
            file1Lines[1] = file1To;
            file2Lines[0] = file2From;
            file2Lines[1] = file2To;
            this.typeOfChange = typeOfChange;
                
        }


    }
}
