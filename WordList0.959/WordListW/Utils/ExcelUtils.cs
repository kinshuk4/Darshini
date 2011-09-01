using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace WordListW.Utils
{
    public class ExcelUtils
    {
        private static string provider = "Provider=Microsoft.Jet.OleDb.4.0;";
        private static string source = @"Data Source=D:\lyf\programs\wpf\WordList\WordList0.9\WordListW\db\to be used\homophone.xls;";
        private static string extended = "Extended Properties=Excel 8.0";

        private static string connectionString = provider + source + extended;
        public static  DataTable SQLSelect()
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", connection);
            OleDbDataReader dr;

            connection.Open();
            dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            DataTable excelData = new DataTable("ExcelData");
            excelData.Load(dr);

            return excelData;
        }

        public static DataTable SQLSelect(string path)
        {
            String conString = provider + path + extended;
            OleDbConnection connection = new OleDbConnection(conString);
            OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", connection);
            OleDbDataReader dr;

            connection.Open();
            dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            DataTable excelData = new DataTable("ExcelData");
            excelData.Load(dr);

            return excelData;
        }

    }
}
