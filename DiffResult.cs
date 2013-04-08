using System;


namespace Wa2.DaoClasses
{
    public class DiffResult
    {
        Boolean isFinished {get;set;};
        Integer jobID {get;set;}
        String diff {get;set;}

        DiffResult(Integer jobID) {
            this.jobID = jobID;
            isFinished = false;
        }

    }
}