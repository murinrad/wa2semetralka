using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa2.Operations;
using Wa2.DaoClasses;
namespace DiffRequestProcessor
{
    class DiffOP : DiffOperation
    {

        public Wa2.DaoClasses.DiffResult getDiff(Wa2.DaoClasses.DiffRequest req)
        {
            DiffResult result = new DiffResult(req.hash,req);

            String[] originaltokenized = req.original;
            int i = 0;
            foreach(String s in originaltokenized) {
                result.addLine(new ResultLine(ResultLine.ChangeType.ADDITION,0,1,0,1));
            }
            return result;
        }
    }
}
