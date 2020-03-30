using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileParse.Assets.Operation
{
    public interface IOperation
    {
        //bool IsComplete { get; set; }

        void Do();

        //void Back();
    }
}
