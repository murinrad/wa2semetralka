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
            int pom;
            foreach (ResultLine line in data)
            {
                if (line.file1Lines[0] == line.file1Lines[1])
                {
                    sb.Append(line.file1Lines[0]);
                }
                else
                {
                    sb.Append(line.file1Lines[0]).Append(",").Append(line.file1Lines[1]);
                }

                sb.Append(ResultLine.ChangeTypeDescription(line.typeOfChange));
                if (line.file2Lines[0] == line.file2Lines[1])
                {
                    sb.Append(line.file2Lines[0]);
                }
                else
                {
                    sb.Append(line.file2Lines[0]).Append(",").Append(line.file2Lines[1]);
                }
                sb.Append("\n");
                switch (line.typeOfChange)
                {
                    case ResultLine.ChangeType.ADDITION:
                        pom = line.file2Lines[0];
                        while (pom <= line.file2Lines[1])
                        {
                            sb.Append("> ").Append(requestData.edited[pom - 1]);
                            sb.Append("\n");
                            pom++;
                        }
                        break;
                    case ResultLine.ChangeType.REMOVAL:
                        pom = line.file1Lines[0];
                        while (pom <= line.file1Lines[1])
                        {
                            sb.Append("< ").Append(requestData.original[pom - 1]);
                            sb.Append("\n");
                            pom++;
                        }
                        break;
                    case ResultLine.ChangeType.CHANGE:
                        pom = line.file1Lines[0];
                        while (pom <= line.file1Lines[1])
                        {
                            sb.Append("< ").Append(requestData.original[pom - 1]);
                            sb.Append("\n");
                            pom++;
                        }
                        sb.Append("------\n");
                        pom = line.file2Lines[0];
                        while (pom <= line.file2Lines[1])
                        {
                            sb.Append("> ").Append(requestData.edited[pom - 1]);
                            sb.Append("\n");
                            pom++;
                        }
                        break;
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

    }
    [DataContract]
    public class ResultLine
    {
        public enum ChangeType
        {
            [Description("a")]
            ADDITION,
            [Description("d")]
            REMOVAL,
            [Description("c")]
            CHANGE
        }

        public static string ChangeTypeDescription(Enum ChangeType)
        {
            System.Reflection.FieldInfo fi = ChangeType.GetType().GetField(ChangeType.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return ChangeType.ToString();
            }
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
