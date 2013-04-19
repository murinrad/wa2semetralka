using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa2.Operations;

namespace DiffRequestProcessor
{
    class DiffOP : DiffOperation
    {
        public string getDiff(Wa2.DaoClasses.DiffRequest req)
        {
            return req.original;
        }
    }
}
