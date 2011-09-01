using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using WordListW.Utils;

namespace WordListW.Dao
{
    public class SynonymDao
    {
        private static string SQL_GetSynonymsForWord = "Select Synonym_List from Synonyms where Word=@word";
        public  string[] getSynonymsForWord(string word)
        {
            OleDbCommand cmdMyQuery = new OleDbCommand(SQL_GetSynonymsForWord);
            cmdMyQuery.Parameters.AddWithValue("@Word", word);
            DataTable dTable = DbUtils.SQLSelect(cmdMyQuery);
            string csvHomoList = "";
            foreach (DataRow dr_ in dTable.Rows)
            {
                csvHomoList = (string)dr_["Synonym_List"];

            }

            return csvHomoList.Split(',');
        }
    }
}
