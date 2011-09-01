using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordListW.Dao;

namespace WordListW.Core
{
   public  class WordRelation
    {
       private CsvSetDao cDao;
       private IDictionary<string, int> homoWholeSet;
       private IDictionary<string, int> synoWholeSet;
       private IDictionary<string, int> antoWholeSet;

       //Contains wordName and corresponding homophoneNames
       private IDictionary<string, List<string>> homophoneStringDic;
       private IDictionary<string, List<string>> synoStringDic;
       public WordRelation()
       {
           cDao = new CsvSetDao();
           homophoneStringDic = new Dictionary<string, List<string>>();
           synoStringDic = new Dictionary<string, List<string>>();

           initialize();
       }
       private void initialize()
       {
            homoWholeSet = cDao.getAllHomonymSet();
           ManipulateSet(homoWholeSet, homophoneStringDic);

           synoWholeSet = cDao.getSynonymSet();
           ManipulateSet(synoWholeSet, synoStringDic);



       }

       public void ManipulateSet(IDictionary<string, int> wholeSet, IDictionary<string, List<string>> StringDic)
       {
           foreach (KeyValuePair<string, int> keyValuePair in wholeSet)
           {
               String key = keyValuePair.Key;
               String[] words = key.Split(',');

               foreach (string word in words)
               {
                   List<string> homoList = restOfWords(words, word);
                   Add(word, homoList, StringDic);
               }


           }
       }

       public void Add(string word, List<string> wordList, IDictionary<string, List<string>> StringDic)
       {
           if (!StringDic.ContainsKey(word))
               StringDic.Add(word, wordList);
           else
           {
               List<string> list = StringDic[word];
               foreach (string s in wordList)
               {
                   if (!list.Contains(s))
                       list.Add(s);
               }
           }
       }


       /// <summary>
       /// Returns list of strings having the strings other than word
       /// </summary>
       /// <param name="text"></param>
       /// <returns></returns>
       private List<string> restOfWords(string[] words, string word)
       {
           List<string> restWords = new List<string>();
           foreach (string restWord in words)
           {
               if (restWord.Equals(word))
                   continue;
               else
                   restWords.Add(restWord);
           }
           return restWords;
       }

       /// <summary>
       /// Returns list of strings having the strings other than word
       /// </summary>
       /// <param name="text"></param>
       /// <returns></returns>
       private List<string> restOfWords(List<string> words, string word)
       {
           List<string> restWords = new List<string>();
           foreach (string restWord in words)
           {
               if (restWord.Equals(word))
                   continue;
               else
                   restWords.Add(restWord);
           }
           return restWords;
       }


       /// <summary>
       /// Get All homonyms for the word text(excluding text)
       /// </summary>
       /// <param name="text"></param>
       /// <returns></returns>
       public List<string> getHomonymsForWord(string text)
       {

           return homophoneStringDic[text];
       }

       /// <summary>
       /// Returns word list containg the word itself and its homonyms
       /// </summary>
       /// <param name="text"></param>
       /// <returns></returns>
       public List<Word> getHomonymsWordListSet(string text)
       {
           IWordDao wordDao = new WordDaoImpl();
           List<string> stringList = getHomonymsForWord(text);
           List<Word> wordList = new List<Word>();
           List<Word> wl_ = wordDao.getWord(text);
           foreach (Word word in wl_)
           {
               wordList.Add(word);
           }

           foreach (string wordText in stringList)
           {
               List<Word> words = wordDao.getWord(wordText);
               foreach (Word word in words)
               {
                   wordList.Add(word);
               }
           }
           return wordList;
       }



       /// <summary>
       /// Get All homonyms for the word text(excluding text)
       /// </summary>
       /// <param name="text"></param>
       /// <returns></returns>
       public List<string> getSynonymsForWord(string text)
       {
           if(synoStringDic.ContainsKey(text))
           return synoStringDic[text];
           else
           {
               return null;
           }
       }

       /// <summary>
       /// Returns word list containg the word itself and its homonyms
       /// </summary>
       /// <param name="text"></param>
       /// <returns></returns>
       public List<Word> getSynonymsWordListSet(string text)
       {
           IWordDao wordDao = new WordDaoImpl();
           List<string> stringList = getSynonymsForWord(text);
           List<Word> wordList = new List<Word>();
           List<Word> wl_ = wordDao.getWord(text);
           foreach (Word word in wl_)
           {
               wordList.Add(word);
           }

           foreach (string wordText in stringList)
           {
               List<Word> words = wordDao.getWord(wordText);
               foreach (Word word in words)
               {
                   wordList.Add(word);
               }
           }
           return wordList;
       }





    }
}
