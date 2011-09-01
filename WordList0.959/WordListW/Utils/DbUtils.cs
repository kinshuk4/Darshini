using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace WordListW.Utils
{
    public class DbUtils
    {
        //../../db/WordsDB.accdb
        
        private static string provider = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
        private static string fixedFileLocation = @"D:\lyf\programs\wpf\WordList\WordList0.931\WordListW\db\WordsDB.accdb";
        private static string relativeDBFileLocation = "../../db/WordsDB.accdb";
        public static string exeFileLocation = "./WordsDB.accdb;";

        public static string oldFileLoc =
            @"D:\lyf\programs\wpf\WordList\b4 0.92\WordList0.92\WordListW\db\WordsDB.accdb";
        public static string fixexelocation =
            @"D:\lyf\programs\wpf\WordList\WordList0.92\WordListW\bin\Debug\WordsDB.accdb;";
      //  private static string password = "User Id=Admin;Password=rekha;";
        private static  string password2 = "Jet OLEDB:Database password=rekha;";

       //+ password2;
        private static string fileLoc = "/WordListW;component/db/WordsDB.accdb";
        //string conConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;WordsDBConnectionString  |DataDirectory|
        private static string projDataSource = @"Data Source=..\..\db\WordsDB.accdb;Persist Security Info=True;";

        //con string is provider + datasource
        
        public static string conConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\db\WordsDB.accdb;Persist Security Info=True;"+password2;

        private static string GetConnectionString()
        {
            string connectionString = "";

            XmlDocument xd = new XmlDocument();
           // xd.Load(Application.executable + ".config"); //imp this line, to check comment
            XmlNodeList nodeList = xd.GetElementsByTagName("connectionStrings");

            foreach (XmlNode node in nodeList)
            {
                XmlNode node2 = node.ChildNodes[0];
                if (node2.Attributes[0].Value.EndsWith("ConnectionString"))
                    connectionString = node2.Attributes[1].Value;
            }

            return connectionString;
        }

        //sql connections
        /// Returns the results of a SQL Query in the form of a DataTable
        public static DataTable SQLSelect(SqlCommand cmdSQLQuery)
        {
            //Get connection string
            //string conConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            string conConnectionString=null;
            SqlConnection SQLDatabaseConnection = new SqlConnection(conConnectionString);
            //Perform Command
            cmdSQLQuery.Connection = SQLDatabaseConnection;
            DataSet dsPageInfo = new DataSet();
            SqlDataAdapter daPageInfo = new SqlDataAdapter(cmdSQLQuery);
            SQLDatabaseConnection.Open();
            daPageInfo.Fill(dsPageInfo);
            SQLDatabaseConnection.Close();
            return dsPageInfo.Tables[0];
        }
        /// Executes a SQL Command
        public static void ExecuteSQLCommand(SqlCommand CommandToExecute)
        {
            //get connection sring
            
            SqlConnection SQLDatabaseConnection = new SqlConnection(conConnectionString);
            //execute command
            CommandToExecute.Connection = SQLDatabaseConnection;
            SQLDatabaseConnection.Open();
            CommandToExecute.ExecuteNonQuery();
            SQLDatabaseConnection.Close();
        }


        //ole db connection
        /// Returns the results of a SQL Query in the form of a DataTable
        public static DataTable SQLSelect(OleDbCommand cmdOleDbCommand)
        {
            OleDbConnection OleDbDatabaseConnection = new OleDbConnection(conConnectionString);
            //Perform Command
            cmdOleDbCommand.Connection = OleDbDatabaseConnection;
            DataSet dsPageInfo = new DataSet();
            OleDbDataAdapter daPageInfo = new OleDbDataAdapter(cmdOleDbCommand);
            OleDbDatabaseConnection.Open();
            daPageInfo.Fill(dsPageInfo);
            OleDbDatabaseConnection.Close();
            return dsPageInfo.Tables[0];
        }
        /*do not use
        public static OleDbDataReader SQLSelect2Reader(OleDbCommand cmdOleDbCommand)
        {
            OleDbConnection OleDbDatabaseConnection = new OleDbConnection(conConnectionString);
            //Perform Command
            cmdOleDbCommand.Connection = OleDbDatabaseConnection;

            
            OleDbDatabaseConnection.Open();
            
             OleDbDataReader dbReader = cmdOleDbCommand.ExecuteReader();
             return dbReader;
            //OleDbDatabaseConnection.Close();
            //return dbReader;
        }*/
        /// Executes a SQL Command
        public static void ExecuteSQLCommand(OleDbCommand CommandToExecute)
        {

           
            OleDbConnection OleDbDatabaseConnection = new OleDbConnection(conConnectionString);
            //execute command
            CommandToExecute.Connection = OleDbDatabaseConnection;
            OleDbDatabaseConnection.Open();
            CommandToExecute.ExecuteNonQuery();
            OleDbDatabaseConnection.Close();
        }
    }
}
