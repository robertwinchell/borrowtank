using System;
using System.Collections.Concurrent;
using System.IO;
using System.Web;
using System.Web.Configuration;

namespace ASOL.HireThings.Logger
{
    public class FileLogger:BaseLogger
    {
        //TODO: Add File Path here for Program Data Folder ProgramData/HireThings/Portal
        // File Name : 2015-07-03.txt



        private string _filePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
      
        
        private string _projectName = WebConfigurationManager.AppSettings["ProjectName"];
        private string _moduleName = WebConfigurationManager.AppSettings["ModuleName"];
        public FileLogger(ref ConcurrentQueue<Message> que) : base(ref que) { }

        public override void StartLogging()
        {
            if (_projectName != null && _moduleName != null && _projectName != string.Empty && _moduleName != string.Empty)
                _filePath = String.Format("{0}\\{1}\\{2}", _filePath, _projectName, _moduleName);
            else
                _filePath = String.Format("{0}\\{1}", _filePath, "Logger");
            if (!System.IO.Directory.Exists(_filePath))
                System.IO.Directory.CreateDirectory(_filePath);

            string fileName = string.Format("{0}/{1}.txt", _filePath,  DateTime.Now.ToString("yyyy-MM-dd"));
       

            using (System.IO.StreamWriter sw = new StreamWriter(new System.IO.FileStream(fileName, FileMode.OpenOrCreate)))
            {

                Message msg = null;
                while (true)
                {
                    if (_queue.TryDequeue(out msg) == false)
                    {
                        break;
                    }

                    string ssds= msg.ToString();
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(msg.ToString());    
                }

                sw.Flush();
                sw.Close(); 
            }


            //Delete files older than one week - This will be triggered every time an exception is logged -> can be optimized
            string[] files = Directory.GetFiles(_filePath);

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.LastAccessTime < DateTime.Now.AddDays(-7))
                    fi.Delete();
            }

            OnLogCompleted(); 
        }
    }
}