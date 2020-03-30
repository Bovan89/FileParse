using FileParse.Assets.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileParse.Assets.Task
{
    public interface ITask
    {
        bool Run();                
    }
}
