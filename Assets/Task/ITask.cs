using FileParse.Assets.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileParse.Assets.Task
{
    interface ITask
    {
        string ErrorMessage { get; set; }

        List<IOperation> OperationList { get; set; }

        bool Run();

        void Cancel();
    }
}
