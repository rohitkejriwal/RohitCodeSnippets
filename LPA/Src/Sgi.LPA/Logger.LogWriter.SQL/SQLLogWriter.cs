using Logger.Core;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logger.LogWriter.SQL
{
    public class SQLLogWriter : ILogWriter, IDisposable
    {
        private ISQLLogConfig _config;

        public SQLLogWriter(ISQLLogConfig config)
        {
            _config = config;
        }

        public decimal GetDataSize()
        {
            string commandText;
            decimal size = 0;

            string connectionString = ConfigurationManager.AppSettings["LogConnectionString"]; ;
            SqlConnection connnection = null;

            try
            {
                connnection = new SqlConnection(connectionString);
                commandText = @"SELECT SUM(mf.size)/1024 Size_MBs FROM sys.master_files mf INNER JOIN sys.databases d ON d.database_id = mf.database_id WHERE d.name = 'LoggerPerformance' GROUP BY d.name";
                SqlCommand cmd = new SqlCommand(commandText, connnection);

                connnection.Open();
                var output = cmd.ExecuteScalar();
                size = Convert.ToDecimal(output);
                connnection.Close();
            }
            catch
            { }
            finally
            {
                if (connnection != null && connnection.State == ConnectionState.Open)
                {
                    connnection.Close();
                }
            }
            return size;
        }

        public void WriteLog(Domain.LogBaseData log)
        {
            log._id = string.Format("{0}_{1}_{2}_{3}", Utility.GetRandomNumber(4), Utility.GetRandomString(4), Utility.GetTimeStamp(), log.TransactionId);
            LogMessage(log);
        }

        public void LogMessage(Domain.LogBaseData logObj)
        {
            string message = string.Empty, commandText;
            string connectionString = ConfigurationManager.AppSettings["LogConnectionString"];
            SqlConnection connnection = null;

            message = Newtonsoft.Json.JsonConvert.SerializeObject(logObj);
            message = message.Replace("'", "''");

            try
            {
                connnection = new SqlConnection(connectionString);
                commandText = string.Format("INSERT INTO [dbo].[SQLLog]([LogMessage])VALUES('{0}')", message);
                SqlCommand cmd = new SqlCommand(commandText, connnection);
                connnection.Open();
                cmd.ExecuteNonQuery();
                connnection.Close();
            }
            catch
            { }
            finally
            {
                if (connnection != null && connnection.State == ConnectionState.Open)
                {
                    connnection.Close();
                }
            }
        }

        public void Dispose()
        {
        }
    }
}