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
        private string  From { get; set; }

        private string To { get; set; }

        private string FilePattern { get; set; }

        private List<string> FileList { get; set; }

        public List<string> ResultFileList { get; set; }        

        public TransferTask(string from, string to, string filePattern = null)
        {
            From = from;

            To = to;

            FilePattern = filePattern;            
        }

        public TransferTask(List<string> fileList, string to)
        {   
            To = to;

            FileList = fileList;            
        }        

        public bool Run()
        {
            try
            {
                if (FileList == null && !string.IsNullOrEmpty(From))
                {
                    string[] files = null;
                    if (string.IsNullOrEmpty(FilePattern))
                    {
                        files = Directory.GetFiles(From);
                    }
                    else
                    {
                        files = Directory.GetFiles(From, FilePattern);
                    }

                    FileList = new List<string>();

                    foreach (string item in files)
                    {
                        FileList.Add(item);
                    }
                }

                if (FileList?.Count > 0)
                {
                    if (!Directory.Exists(To))
                    {
                        Directory.CreateDirectory(To);
                    }

                    ResultFileList = new List<string>();

                    foreach (var file in FileList)
                    {
                        string newFileName = Path.Combine(To, Path.GetFileName(file));

                        File.Move(file, newFileName);

                        ResultFileList.Add(newFileName);
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Rollback();

                throw;
            }

            return true;            
        }

        public void Rollback()
        {
            if (!string.IsNullOrEmpty(From) && ResultFileList != null)
            {
                foreach (var file in ResultFileList)
                {
                    string newFileName = Path.Combine(From, Path.GetFileName(file));

                    File.Move(file, newFileName);
                }

                ResultFileList.Clear();
            }
        }
    }
}
