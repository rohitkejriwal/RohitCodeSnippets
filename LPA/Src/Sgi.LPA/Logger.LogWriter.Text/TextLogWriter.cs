using Logger.Core;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace Logger.LogWriter.Text
{
    public class TextLogWriter : ILogWriter
    {
        private static object objLock = new object();
        private ITextLogConfig _config;
        private FileStream fileStream;

        public TextLogWriter(ITextLogConfig config)
        {
            lock (objLock)
            {
                _config = config;
                fileStream = null;
                DirectoryInfo logDirInfo = null;
                var logFileInfo = new FileInfo(_config.FilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
            }
        }

        public void WriteLog(Domain.LogBaseData log)
        {
            LogMessage(log);
        }

        public decimal GetDataSize()
        {
            string logFilePath = ConfigurationManager.AppSettings["LogFilePath"];
            FileInfo logFileInfo = new FileInfo(logFilePath);
            return (logFileInfo.Length / (decimal)1048576);
        }

        public void LogMessage(Domain.LogBaseData logObj)
        {
            lock (objLock)
            {
                var filePath = _config.FilePath;
                filePath = UpadateFilePath(filePath);
                fileStream = new FileStream(filePath, FileMode.Append);
                StreamWriter log = new StreamWriter(fileStream);
                string message = string.Empty;
                message = Newtonsoft.Json.JsonConvert.SerializeObject(logObj);
                log.WriteLine(string.Format("[{0}][{1}]{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture), logObj.Level, message));
                log.Close();
                fileStream.Close();
            }
        }

        private string UpadateFilePath(string path)
        {
            var startIndex = path.IndexOf("{{");
            if (startIndex != -1)
            {
                var endIndex = path.IndexOf("}}");
                var formatdata = path.Substring(startIndex + 2, endIndex - startIndex);
                var format = formatdata.Replace("}}", "");
                path = path.Replace("{{" + formatdata, DateTime.Now.ToString(format));
            }
            return path;
        }
    }
}