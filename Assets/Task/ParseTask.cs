using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileParse.Assets.Operation;

namespace FileParse.Assets.Task
{
    public class ParseTask : ITask
    {
        public string From { get; set; }
        public string FilePattern { get; set; }
        public string ErrorMessage { get; set; }
        public List<IOperation> OperationList { get; set; }
        public ParseTask(string from, string filePattern = null)
        {
            From = from;
            FilePattern = filePattern;

            Prepare();
        }

        public void Prepare()
        {
            OperationList = new List<IOperation>();

            string[] files = null;
            if (string.IsNullOrEmpty(FilePattern))
            {
                files = Directory.GetFiles(From);
            }
            else
            {
                files = Directory.GetFiles(From, FilePattern);
            }

            foreach (string item in files)
            {
                //OperationList.Add(new MoveOperation(item, To));
            }
        }

        public void Cancel()
        {
            
        }

        public bool Run()
        {
            return true;
        }
    }
}
