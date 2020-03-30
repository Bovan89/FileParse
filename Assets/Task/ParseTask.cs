using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileParse.Assets.Operation;
using FileParse.Model;

namespace FileParse.Assets.Task
{
    public class ParseTask : ITask
    {
        private List<IOperation> OperationList { get; set; }

        private string SourceFolder { get; set; }

        private List<string> FileList { get; set; }

        private List<GoodData> GoodData { get; set; }

        public ParseTask(List<string> fileList, List<GoodData> goodData, string sourceFolder)
        {
            FileList = fileList;
            GoodData = goodData;
            SourceFolder = sourceFolder;

            Prepare();
        }

        private void Prepare()
        {
            if (FileList?.Count > 0)
            {
                OperationList = new List<IOperation>();

                foreach (string item in FileList)
                {
                    OperationList.Add(new ParseOperation(item, GoodData, SourceFolder));
                }
            }
        }

        public bool Run()
        {
            foreach (var operation in OperationList)
            {
                operation.Do();
            }

            return true;
        }
    }
}
