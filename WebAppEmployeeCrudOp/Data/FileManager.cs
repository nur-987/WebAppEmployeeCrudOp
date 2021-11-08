using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebAppEmployeeCrudOp.Data
{
    public class FileManager
    {
        public string ReadAllText(string FileName)
        {
            string content = File.ReadAllText(FileName);
            return content;
        }

        public void WriteAllText (string FileName, string Input)
        {
            File.WriteAllText(FileName, Input);
        }
    }
}