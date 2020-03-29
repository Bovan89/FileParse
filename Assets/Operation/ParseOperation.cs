using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileParse.Assets.Operation
{
    public class ParseOperation : IOperation
    {
        public string FilePath { get; set; }
        public bool IsComplete { get; set; }
        public ParseOperation(string filePath)
        {
            FilePath = filePath;
        }
        public void Do()
        {
            
        }
        public void Back()
        {
            throw new NotImplementedException();
        }
    }
}
