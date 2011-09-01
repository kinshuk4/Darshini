using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using WordListW.Core;
using WordListW.Utils;

namespace WordListW.Dao
{
    public class HomonymDao
    {
        private static string SQL_GetAllHomophones = "Select Word,Homophone_list from Homophones";
        private static string SQL_GetHomymsForWord = "Select Homophone_list from Homophones where Word=@word";

        /// <summary>
        /// Returns all homonyms in the database
        /// </summary>
        /// <returns></returns>
        public  List<List<String>> getAllHomonyms()
        {
            OleDbCommand cmdMyQuery = new OleDbCommand(SQL_GetAllHomophones);
            DataTable dTable = DbUtils.SQLSelect(cmdMyQuery);
            string csvHomoList = "";
            string word = "";
            List<String> oneHomoSet = new List<string>(); 
            List<List<String>> wholeHomoSet = new List<List<string>>();
            foreach (DataRow dr_ in dTable.Rows)
            {
                
                word = (string) dr_["Word"];
                csvHomoList = (string)dr_["Homophone_list"];
                string[] words = csvHomoList.Split(',');
                oneHomoSet.Add(word);
                foreach (string s in words)
                {
                    string t = s.Trim();
                    oneHomoSet.Add(t);
                }
                wholeHomoSet.Add(oneHomoSet);
                oneHomoSet.RemoveRange(0,oneHomoSet.Count);
            }

            return wholeHomoSet;
        }

       public IDictionary<string,int> getAllHomonymSet()
    {
        OleDbCommand cmdMyQuery = new OleDbCommand(SQL_GetAllHomophones);
        DataTable dTable = DbUtils.SQLSelect(cmdMyQuery);
        string csvHomoList = "";
        string word = "";
           IDictionary<string , int > fullHomoSet = new Dictionary<string, int>();
           List<string> oneHomoSet=new List<string>();
           int i=0;
           foreach (DataRow dr_ in dTable.Rows)
           {

               word = (string)dr_["Word"];
               csvHomoList = (string)dr_["Homophone_list"];
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
               if(!fullHomoSet.ContainsKey(csvHomoList))
                  fullHomoSet.Add(csvHomoList, i++);
               oneHomoSet.RemoveRange(0, oneHomoSet.Count);
           }
           return fullHomoSet;
    }
        public  string[] getHomonymsForWord(String word)
        {
            OleDbCommand cmdMyQuery = new OleDbCommand(SQL_GetHomymsForWord);
            cmdMyQuery.Parameters.AddWithValue("@Word", word);
            DataTable dTable = DbUtils.SQLSelect(cmdMyQuery);
            string csvHomoList="";
            foreach (DataRow dr_ in dTable.Rows)
            {
                 csvHomoList = (string) dr_["Homophone_list"];

            }

            return csvHomoList.Split(',');
        }

        public  List<Word> getHomonymWords(String word)
        {
            string[] words = getHomonymsForWord(word);
            IWordDao dao =new WordDaoImpl();
            List<Word> wordListHomo=new List<Word>();
            List<Word> temp;
            foreach (string s in words)
            {
                temp = dao.getWord(s);
                foreach (Word _w in temp)
                {
                    wordListHomo.Add(_w);
                }
            }
            return wordListHomo;
        }

    }
}
