﻿using System;
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
        public string From { get; set; }
        public string ErrorFolder { get; set; }
        public string FilePattern { get; set; }
        public string ErrorMessage { get; set; }
        public List<IOperation> OperationList { get; set; }
        private List<Good> Goods { get; set; }
        public ParseTask(string from, string errorFolder, string filePattern = null)
        {
            From = from;
            ErrorFolder = errorFolder;
            FilePattern = filePattern;

            Prepare();
        }

        public void Prepare()
        {
            Goods = new List<Good>();
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
                OperationList.Add(new ParseOperation(item, Goods));
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
