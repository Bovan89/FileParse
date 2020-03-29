using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileParse.Assets.Operation
{
    public class MoveOperation : IOperation
    {
        protected string FromPath { get; set; }

        protected string ToPath { get; set; }

        protected string FileName { get; set; }

        public bool IsComplete { get; set; }

        public MoveOperation(string fromPath, string toPath)
        {
            FromPath = fromPath;
            ToPath = toPath;
            FileName = Path.GetFileName(fromPath);            
        }

        public void Do()
        {
            File.Move(FromPath, Path.Combine(ToPath, FileName));

            IsComplete = true;
        }

        public void Back()
        {
            File.Move(Path.Combine(ToPath, FileName), FromPath);

            IsComplete = false;
        }
    }
}
