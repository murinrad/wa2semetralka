using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa2.DaoClasses;

namespace Wa2.Operations
{
   public interface DiffOperation
    {

        /**
         * Performs the diff and returns the string formatted according to the diff string specifications
         **/
      DiffResult getDiff(DiffRequest req);

    }
}
