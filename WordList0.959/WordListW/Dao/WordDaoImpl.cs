using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Data.OleDb;
using WordListW.Utils;
using WordListW.Core;
namespace WordListW.Dao
{
  public  class WordDaoImpl:IWordDao
    {

      #region --Create--
      public static string SQL_Insert_Word = "INSERT INTO DictionaryDB (Word,Meaning) VALUES        (@word,@meaning)";

      public void insertWordList(List<Word> wordList)
      {
          foreach (var word in wordList)
          {
              insertWord(word);
          }
      }

      public void insertWord(Word word)
      {
          OleDbCommand cmdMyQuery = new OleDbCommand(SQL_Insert_Word);
          cmdMyQuery.Parameters.AddWithValue("@word", word.Name);
          if (word.Meaning != null)
              cmdMyQuery.Parameters.AddWithValue("@meaning", word.Meaning);
          else
              cmdMyQuery.Parameters.AddWithValue("@meaning", "To be added soon");
          // cmdMyQuery.Parameters.AddWithValue("@note", word.AdditionalInfo);
          DbUtils.ExecuteSQLCommand(cmdMyQuery);
      }
      #endregion

      #region --Retrieve--

      public static string SQL_GET_WORD_BY_NAME = "SELECT Word,Meaning,Note,isMastered,Sentence FROM DictionaryDB where word=@Word";

      public static string SQL_GET_ALL_WORDS = "SELECT Word,Meaning,Note,isMastered,Sentence FROM DictionaryDB";
      public static string[] SQL_GET_N_UNMASTERED = { "SELECT TOP", " Word,Meaning,Note,isMastered FROM DictionaryDB where isMastered=False" };


      public static string SQL_GET_ALL_UNMASTERED_WORD = "SELECT Word,Meaning,Note,isMastered FROM DictionaryDB where isMastered=False";

      public List<Word> getWord(String word)
      {
          OleDbCommand cmdMyQuery = new OleDbCommand(SQL_GET_WORD_BY_NAME);
          cmdMyQuery.Parameters.AddWithValue("@Word", word);
          DataTable dTable = DbUtils.SQLSelect(cmdMyQuery);
          List<Word> wordList = new List<Word>();
          List<Word> wordFetched = getWordListFrmTable(dTable);

          return wordFetched;

      }

      public List<Word> getAllWords()
      {
          OleDbCommand cmdMyQuery = new OleDbCommand(SQL_GET_ALL_WORDS);

          DataTable dTable = DbUtils.SQLSelect(cmdMyQuery);

          List<Word> wordFetched = getWordListFrmTable(dTable);
          Console.WriteLine(wordFetched[0].Name);
          return wordFetched;
      }

      public List<Word> getAllUnmasteredWords()
      {
          OleDbCommand cmdMyQuery = new OleDbCommand(SQL_GET_ALL_UNMASTERED_WORD);
          DataTable dTable = DbUtils.SQLSelect(cmdMyQuery);

          List<Word> wordFetched = getWordListFrmTable(dTable);
          //Console.WriteLine(wordFetched[0].Word2);
          return wordFetched;
      }
      public List<Word> getFirstNunmasteredWords(int N)
      {
          String sql = SQL_GET_N_UNMASTERED[0] + " " + N + " " + SQL_GET_N_UNMASTERED[1];
          OleDbCommand cmdMyQuery = new OleDbCommand(sql);


          DataTable dTable = DbUtils.SQLSelect(cmdMyQuery);

          List<Word> wordFetched = getWordListFrmTable(dTable);
          //Console.WriteLine(wordFetched[0].Word2);
          return wordFetched;
      }
      #endregion


      #region --Update--
      public static string SQL_Update_Word_Note_Master = "UPDATE DictionaryDB SET [Note] = @note,[isMastered]=@ismastered WHERE Word=@word AND Meaning=@meaning";
      public void updateNote(Word word)
      {
          OleDbCommand cmdMyQuery = new OleDbCommand(SQL_Update_Word_Note_Master);
          cmdMyQuery.Parameters.AddWithValue("@note", word.AdditionalInfo);
          cmdMyQuery.Parameters.AddWithValue("@ismastered", word.IsMastered);
          cmdMyQuery.Parameters.AddWithValue("@word", word.Name);
          cmdMyQuery.Parameters.AddWithValue("@meaning", word.Meaning);
          // cmdMyQuery.Parameters.AddWithValue("@note", word.AdditionalInfo);
          DbUtils.ExecuteSQLCommand(cmdMyQuery);
      }
      #endregion


      #region --Delete--
      private static string SQL_DeleteAllWordsFromDictionary = "DELETE FROM DictionaryDB";
      public void deleteAll()
      {
          OleDbCommand cmdMyQuery = new OleDbCommand(SQL_DeleteAllWordsFromDictionary);
          DbUtils.ExecuteSQLCommand(cmdMyQuery);
      }
      #endregion

      #region --Private Methods--

      private static List<Word> getWordListFrmTable(DataTable dt_)
       {
           List<Word> wordListWithSameWord = new List<Word>();
           foreach(DataRow row_ in dt_.Rows)
           { 
              wordListWithSameWord.Add(getWordFrmRow(row_));
           }
           return wordListWithSameWord;
       }
      private static Word getWordFrmRow(DataRow dr_)
      {
          Word wordFetched = new Word();
          try
          {
              wordFetched.Name = (string)dr_["Word"];
              wordFetched.Meaning = (string)dr_["Meaning"];
              wordFetched.IsMastered = (bool)dr_["isMastered"];
              if (!DBNull.Value.Equals(dr_["Note"]))
                  wordFetched.AdditionalInfo = (string)dr_["Note"].ToString();
              else
              {
                  wordFetched.AdditionalInfo = String.Empty;
              }
              if (!DBNull.Value.Equals(dr_["Sentence"]))
              {
                 
                  wordFetched.Sentence = (string) dr_["Sentence"].ToString();
              }
              else
              {
                  wordFetched.Sentence = String.Empty;
              }

              }catch(Exception exception)
              {
                  //MessageBox.Show(wordFetched.Name);
              }
              finally {
//              try
//                  {
//                      
//                  }
//                  catch (Exception ex)
//                  {
//                  }
                

              }

          return wordFetched;
      }

      #endregion
    }
}
