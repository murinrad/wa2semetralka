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
            DiffResult result = new DiffResult(req.hash);
            String original = req.original;
            String[] originaltokenized = original.Split('\n');
            int i = 0;
            foreach(String s in originaltokenized) {
                result.addLine(i++,DiffResult.ChangeType.NO_CHANGE, s);
            }
            return result;
        }
    }
}
