using FileParse.Assets.Task;
using FileParse.Model;
using FileParse.ParseDbContext;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileParse.Assets
{
    public class ParseProcess
    {
        public bool IsSuccess { get; private set; }

        public string Error { get; private set; }

        protected string InSubFolder { get; set; }

        protected string ProcessSubFolder { get; set; }

        protected string ErrorSubFolder { get; set; }

        protected string FilePattern { get; set; }

        protected string ConnectionString { get; set; }

        public void SetParm(IConfiguration config)
        {
            InSubFolder = config.GetValue<string>("InFolder");
            ProcessSubFolder = config.GetValue<string>("ProcessFolder");
            ErrorSubFolder = config.GetValue<string>("ErrorFolder");
            FilePattern = config.GetValue<string>("FilePattern");
            ConnectionString = config.GetValue<string>("connectionString");
        }

        public void Start(string folder)
        {
            string sourceFolder = Path.Combine(folder, InSubFolder);
            string destinationFolder = Path.Combine(folder, ProcessSubFolder);
            string errorFolder = Path.Combine(folder, ErrorSubFolder);
            string backFolder = sourceFolder;

            List<string> filelist = null;

            try
            {
                TransferTask transTask = new TransferTask(sourceFolder, destinationFolder, FilePattern);
                if (transTask.Run())
                {
                    filelist = transTask.ResultFileList;

                    backFolder = errorFolder;

                    List<GoodData> goods = new List<GoodData>();

                    ParseTask parseTask = new ParseTask(filelist, goods, folder);
                    if (parseTask.Run())
                    {
                        if (goods.Count > 0)
                        {
                            SaveTask saveTask = new SaveTask(goods, ConnectionString);
                            saveTask.Run();                            
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                Error = ex.Message;

                Move(filelist, backFolder);

                return;
            }

            IsSuccess = true;
        }

        private void Move(List<string> fileList, string toFolder)
        {
            var transferTask = new TransferTask(fileList, toFolder);
            transferTask.Run();
        }
    }
}
