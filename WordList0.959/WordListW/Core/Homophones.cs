using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using WordListW.Dao;

namespace WordListW.Core
{
   public  class Homophones
    {
       private IDictionary<string, List<string>> homophoneStringDic;
       private IDictionary<string, List<Word>> homoPhoneWordSet;
       private IDictionary<string, int> homoPhoneSet;
       private IDictionary<string, List<Word>> homoPhoneSetPerWord;
       private HomonymDao hdao;
       public Homophones()
       {
           homophoneStringDic= new Dictionary<string, List<string>>();
           homoPhoneWordSet = new Dictionary<string, List<Word>>();
           homoPhoneSet = new Dictionary<string, int>();
           hdao = new HomonymDao();
           initialize();
       }
       private void initialize()
       {
//           List<List<string>> wholeHomoSet = hdao.getAllHomonyms();
//           foreach (List<string> list in wholeHomoSet)
//           {
//               Add(list);
//           }


           IDictionary<string, int> wholeHomoSet = hdao.getAllHomonymSet();
           foreach (KeyValuePair<string, int> keyValuePair in wholeHomoSet)
           {
               String key = keyValuePair.Key;
               String[] words = key.Split(',');

               foreach (string word in words)
               {
                   List<string> homoList = restOfWords(words, word);
                   Add(word,homoList);
               }
            
               
           }

       }

       public void Add(string word, List<string > wordList)
       {
           if(!homophoneStringDic.ContainsKey(word))
               homophoneStringDic.Add(word,wordList);
           else
           {
               List<string> list = homophoneStringDic[word];
               foreach (string s in wordList)
               {
                   if(!list.Contains(s))
                       list.Add(s);
               }
           }
       }
       public void Add(List<String> words)
       {
           foreach (string word in words)
           {
               if(homophoneStringDic.ContainsKey(word))
               {
                   foreach (string word2 in restOfWords(words, word))
                   {
                       if(homophoneStringDic[word].Contains(word2))
                           continue;
                       else
                       {
                           homophoneStringDic[word].Add(word2);
                       }
                   }
               }
               else
               homophoneStringDic.Add(word,restOfWords(words,word));
           }
           
       }

       private List<string> restOfWords(List<string> words, string word)
       {
           List<string > restWords = new List<string>();
           foreach (string restWord in words)
           {
               if(restWord.Equals(word))
                   continue;
               else
                   restWords.Add(restWord);
           }
           return restWords;
       }

       private List<string > restOfWords(string[] words, string word)
       {
            List<string > restWords = new List<string>();
            foreach (string restWord in words)
            {
                if (restWord.Equals(word))
                    continue;
                else
                    restWords.Add(restWord);
            }
            return restWords;
       }

       public void setAllHomoPhones()
       {
           List<List<string>> wholeHomoSet = hdao.getAllHomonyms();
           foreach (List<string> list in wholeHomoSet)
           {
               list.Sort();
               string csvWords = "";
               int i = 0;
               foreach (string s in list)
               {
                   csvWords += s+",";
               }
               if(!homoPhoneSet.ContainsKey(csvWords))
               {
                   homoPhoneSet.Add(csvWords,i++);
               }
           }


       }

       /// <summary>
       /// Get All homonyms for the word text(excluding text)
       /// </summary>
       /// <param name="text"></param>
       /// <returns></returns>
       public List<string > getHomonymsForWord(string text)
       {
           
           return homophoneStringDic[text];
       }

       /// <summary>
       /// Returns word list containg the word itself and its homonyms
       /// </summary>
       /// <param name="text"></param>
       /// <returns></returns>
       public List<Word> getHomonymsWords(string text)
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

    }
}
