using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileParse.Assets.Operation;
using FileParse.ParseDbContext;

namespace FileParse.Assets.Task
{
    public class ParseTask : ITask
    {
        //public string Error { get; set; }

        private List<IOperation> OperationList { get; set; }

        private List<string> FileList { get; set; }

        private List<Good> Goods { get; set; }

        //public bool IsSuccess { get; private set; }

        public ParseTask(List<string> fileList, List<Good> goods)
        {
            FileList = fileList;
            Goods = goods;

            Prepare();
        }

        private void Prepare()
        {            
            OperationList = new List<IOperation>();

            foreach (string item in FileList)
            {
                OperationList.Add(new ParseOperation(item, Goods));
            }
        }

        public override bool Run()
        {
            //try
            //{

            foreach (var operation in OperationList)
            {
                operation.Do();
            }

            return true;

            /*}
            catch (Exception ex)
            {
                Error = ex.Message;

                IsSuccess = false;

                return IsSuccess;
            }

            IsSuccess = true;

            return IsSuccess;*/
        }
    }
}
