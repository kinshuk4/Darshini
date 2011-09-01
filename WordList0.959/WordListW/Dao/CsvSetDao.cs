using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using WordListW.Utils;

namespace WordListW.Dao
{
    public class CsvSetDao
    {
        private static string SQL_GetAllHomophones = "Select Word,Homophone_list from Homophones";
        private static string SQL_GetHomymsForWord = "Select Homophone_list from Homophones where Word=@word";

        private static string Sql_GetAllSynonyms = "Select Word,Synonym_list from Synonyms";
        private static string SQL_GetSynonymsForWord="Select Synonym_List from Synonyms where Word=@word";

        private static string colHomoList = "Homophone_List";
        private static string colSynoList = "Synonym_List";
        private static string colAntoList = "Antonym_List";

        private string[] getStringArr(DataTable dTable,string list)
        {
            string csvHomoList = "";
            foreach (DataRow dr_ in dTable.Rows)
            {
                csvHomoList = (string)dr_[list];

            }
            return csvHomoList.Split(',');
        }

        private IDictionary<string,int> getAllDictionarySet(DataTable dTable,string list)
        {
            string csvHomoList = "";
            string word = "";
            IDictionary<string, int> fullHomoSet = new Dictionary<string, int>();
            List<string> oneHomoSet = new List<string>();
            int i = 0;
            foreach (DataRow dr_ in dTable.Rows)
            {

                word = (string)dr_["Word"];
                csvHomoList = (string)dr_[list];
                string[] words = csvHomoList.Split(',');
                oneHomoSet.Add(word);
                foreach (string s in words)
                {
                    string t = s.Trim();
                    oneHomoSet.Add(t);
                }
                oneHomoSet.Sort();
                csvHomoList = "";
                foreach (string s in oneHomoSet)
                {
                    csvHomoList += (s + ",");
                }
                csvHomoList = csvHomoList.Substring(0, csvHomoList.Length - 1);
                if (!fullHomoSet.ContainsKey(csvHomoList))
                    fullHomoSet.Add(csvHomoList, i++);
                oneHomoSet.RemoveRange(0, oneHomoSet.Count);
            }
            return fullHomoSet;
        }



        public IDictionary<string, int> getAllHomonymSet()
        {
            OleDbCommand cmdMyQuery = new OleDbCommand(SQL_GetAllHomophones);
            DataTable dTable = DbUtils.SQLSelect(cmdMyQuery);
            return getAllDictionarySet(dTable, colHomoList);
        }
        public string[] getHomonymsForWord(String word)
        {
            OleDbCommand cmdMyQuery = new OleDbCommand(SQL_GetHomymsForWord);
            cmdMyQuery.Parameters.AddWithValue("@Word", word);
            DataTable dTable = DbUtils.SQLSelect(cmdMyQuery);
            return getStringArr(dTable, colHomoList);
        }

        public string[] getSynonymsForWord(string word)
        {
            OleDbCommand cmdMyQuery = new OleDbCommand(SQL_GetSynonymsForWord);
            cmdMyQuery.Parameters.AddWithValue("@Word", word);
            DataTable dTable = DbUtils.SQLSelect(cmdMyQuery);
            return getStringArr(dTable, colSynoList);
        }

    public IDictionary<string ,int > getSynonymSet()
    {
        OleDbCommand cmdMyQuery = new OleDbCommand(Sql_GetAllSynonyms);
        DataTable dTable = DbUtils.SQLSelect(cmdMyQuery);
        return getAllDictionarySet(dTable, colSynoList);
    }
    }
}
