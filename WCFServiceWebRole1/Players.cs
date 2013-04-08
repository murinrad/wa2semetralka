using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WCFServiceWebRole1
{
    [DataContract]
    public class Players
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Sports { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }

    }
}