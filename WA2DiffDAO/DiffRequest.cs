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
        public String[] original { get; set; }
        [DataMember(Order = 1)]
        public String[] edited {get;set;}
        [DataMember(Order = 2)]
        public int hash { get; set; }

        public DiffRequest(String[] original, String[] edited)
        {
            this.original = original;
            this.edited = edited;
            calculateHashCode();
            

            
           
        }

        public void calculateHashCode()
        {
            if (original == null || edited == null) return;
            StringBuilder sb = new StringBuilder();
            foreach (String s in original)
            {
                sb.Append(s);
            }
            foreach (String s in edited)
            {
                sb.Append(s);
            }
            String concat = sb.ToString();
            hash = concat.GetHashCode();


        }


        public DiffRequest() { }
    }
}
