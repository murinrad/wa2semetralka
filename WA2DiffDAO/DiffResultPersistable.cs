using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure;
using System.ComponentModel;
using Wa2.DaoClasses;
using System.IO;
using Newtonsoft.Json;

namespace WA2DiffDAO
{
    public class DiffResultPersistable : TableEntity
    {



        public String serializedData { get; set; }
        public DiffResult data
        {
            get
            {
                return getDiffResult(serializedData);
            }
            set
            {
                serializedData = diffToString(value);
            }
        }
        public String test { get; set; }

        public DiffResultPersistable(DiffResult data)
        {
            this.data = data;
            data.addLine(0, DiffResult.ChangeType.NO_CHANGE, "asd");
            this.RowKey = data.jobID + "";
            this.PartitionKey = data.jobID + "";
            this.test = "asdasdasdasdasda" + data.jobID;
        }

        public DiffResultPersistable()
        {
            // this.data = new DiffResult(-1);
            // data.addLine(0,DiffResult.ChangeType.NO_CHANGE, "adasdas"); // dummy shit
            // data.isFinished = true;
        }

        private static DiffResult getDiffResult(String json)
        {
            return JsonConvert.DeserializeObject<DiffResult>(json);
        }
        private String diffToString(DiffResult res)
        {
            return JsonConvert.SerializeObject(res);
        }

    }
}
