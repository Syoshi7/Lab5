using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class Logger
    {
        private readonly string L_logFilePath;
        private bool L_append;

        public Logger(string logFilePath, bool append)
        {
            L_logFilePath = logFilePath;
            L_append = append;

            if (!append)
                File.WriteAllText(L_logFilePath, string.Empty);
        }

        private void WriteToFile(string message)
        {
            if (L_append)
            {
                File.AppendAllText(L_logFilePath, message + Environment.NewLine);
            }
            else
            {
                File.WriteAllText(L_logFilePath, message + Environment.NewLine);
                L_append = true;
            }
        }

        public void Info(string message)
        {
            WriteToFile(DateTime.Now + "  Info: " + message);
        }

        public void Error(string message)
        {
            WriteToFile(DateTime.Now + "  Error: " + message);
        }

        public void Warning(string message)
        {
            WriteToFile(DateTime.Now + "  Warning: " + message);
        }
    }
}
