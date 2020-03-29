using FileParse.Assets.Operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileParse.Assets.Task
{
    public class TransferTask : ITask
    {
        public string  From { get; set; }

        public string To { get; set; }

        public string FilePattern { get; set; }

        public string ErrorMessage { get; set; }

        public List<IOperation> OperationList { get; set; }                

        public TransferTask(string from, string to, string filePattern = null)
        {
            From = from;
            To = to;
            FilePattern = filePattern;

            Prepare();
        }

        void Prepare()
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
                OperationList.Add(new MoveOperation(item, To));
            }
        }

        public bool Run()
        {
            try
            {
                foreach (var operation in OperationList)
                {
                    operation.Do();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Cancel();
                return false;
            }

            return true;
        }

        public void Cancel()
        {
            foreach (var operation in OperationList.Where(o => o.IsComplete))
            {
                try
                {
                    operation.Back();
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
