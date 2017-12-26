using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Loko.Utils
{
    public static class Logger
    {
        public static string appPath = Directory.GetCurrentDirectory();
        public static string logPath = Path.Combine(appPath, "logs");

        /// <summary>
        /// The method tries to open the log file, creates it if it doesn't exists yet and writes a log message into it
        /// The method will alaways precede the filename with the date so we dont end up with a huge log.
        /// </summary>
        /// <param name="filename">The name of the log file</param>
        /// <param name="text">The log message that is to be written inside</param>
        public static void WriteLog(string filename, string text)
        {
            StreamWriter log;
            string date = DateTime.Now.ToString("ddMMyyyy");
            string fn = date + "_" + filename;
            fn = Path.Combine(logPath, fn);

            Debug.WriteLine(text);

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            if (!File.Exists(fn))
            {
                log = new StreamWriter(fn);
            }
            else
            {
                log = File.AppendText(fn);
            }

            log.WriteLine(DateTime.Now.ToString() + " " + text);
            log.Flush();
            log.Close();

        }


        /// <summary>
        /// Same method as above
        /// </summary>
        /// <param name="filename">The name of the log file</param>
        /// <param name="e">The exception message that is to be written inside</param>
        public static void WriteLog(string filename, Exception e)
        {
            WriteLog(filename, e.ToString());
        }


    }
}
