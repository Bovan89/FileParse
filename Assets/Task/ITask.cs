using FileParse.Assets.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileParse.Assets.Task
{
    public abstract class ITask
    {
        //bool IsSuccess { get; }

        //string Error { get; set; }

        //List<IOperation> OperationList { get; set; }

        public abstract bool Run();

        //void Cancel();
    }
}
