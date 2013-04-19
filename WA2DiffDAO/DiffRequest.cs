using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Wa2.DaoClasses
{
    [DataContract]
  public  class DiffRequest
    {
        [DataMember(Order = 0)]
        public String original { get; set; }
        [DataMember(Order = 1)]
        public String edited {get;set;}
        [DataMember(Order = 2)]
        public int hash { get; set; }

        public DiffRequest(String original, String edited)
        {
            this.original = original;
            this.edited = edited;
            String concat = String.Concat(original, edited);
            hash = concat.GetHashCode();
        }

        public DiffRequest() { }
    }
}
