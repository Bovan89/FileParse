using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileParse.Assets.Operation;
using FileParse.ParseDbContext;

namespace FileParse.Assets.Task
{
    public class SaveTask : ITask
    {
        private List<Good> Goods { get; set; }
        public string From { get; set; }
        public string ErrorFolder { get; set; }
        public string FilePattern { get; set; }
        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IOperation> OperationList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SaveTask(string from, string errorFolder, List<Good> goods, string filePattern = null)
        {
            From = from;
            ErrorFolder = errorFolder;
            Goods = goods;
            FilePattern = filePattern;

            Prepare();
        }

        public void Prepare()
        {
            foreach (var item in Goods)
            {

            }
        }

        public void Cancel()
        {
            //Перенос в папку ERROR
            var transferTask = new TransferTask(From, ErrorFolder, FilePattern);
            transferTask.Run();
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
    }
}
