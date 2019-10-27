using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TodoApi.DataBaseOperation
{
    public class DBConnection
    {
        private DBConnection()
        {
            connection = new SQLiteConnection(GetConnectionString());
            connection.Open();
        }

        private static DBConnection accesToData = null;
        private static SQLiteConnection connection = null;
        private string commandText = null;
        private Func<SQLiteDataReader, dynamic> FuncHadlingReader;

        public static DBConnection GetConnection()
        {
            accesToData = accesToData == null ? new DBConnection() : accesToData;
            return accesToData;
        }

        public void ExecuteQuery(string commandText, Func<SQLiteDataReader, dynamic> FuncHadlingReader = null)
        {
            this.commandText = commandText;
            this.FuncHadlingReader = FuncHadlingReader;
            Exception ex = null;
            lock (accesToData) 
            {
                try
                {
                    Execute();
                }
                catch (Exception e)
                {
                    ex = new Exception("Error500There Is problem with execute command", e);
                }
            }
            if (ex != null)
                throw ex;
        }

        private void Execute()
        {
            using (SQLiteCommand cmd = new SQLiteCommand(commandText, connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (FuncHadlingReader != null)
                        FuncHadlingReader(reader);
                }
            }
        }

        private string GetConnectionString()
        {
            var locationOfDB = Environment.CurrentDirectory;  //Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); //
            var nameOfDateBase = "ExampleDataBase.db";
            var additionalParameters = ";Version=3; FailIfMissing=True; Foreign Keys=True;";
            var connectionString = string.Format("{0}{1}\\{2}{3}", "Data Source=", locationOfDB, nameOfDateBase, additionalParameters);
            return connectionString;
        }
    }
}