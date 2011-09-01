using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordListW.Utils;
using WordListW.Dao;

namespace WordListW.Core
{
   public class AvailableWords
    {

        public HashSet<string> WordsNameOnlyList { get; set; }
        public List<Word> WordList{ get; set; }
        public List<Word> UnMasteredWordList { get; set; }
        public List<Word> MasteredWordList { get; set; }
        public WDictionary WDictionary { get; set; }
        public int Count { get; private set; }
        public int MasteredCount { get; set; }
        public int UnMasteredCount { get { return UnMasteredWordList.Count; } }

        #region Initialize
        public AvailableWords()
        {
            WordList=new List<Word>();
            WDictionary = new WDictionary();
            WordsNameOnlyList=new HashSet<string>();

            fetchAllWordsFromDB();
            Count = WordList.Count;
            MasteredCount = Count - UnMasteredCount;
        }

       private void fetchAllWordsFromDB()
       {
           WordDaoImpl dao  = new WordDaoImpl();

           WordList = dao.getAllWords();
           foreach (Word word in WordList)
           {
               WDictionary.Add(word);
               WordsNameOnlyList.Add((word.Name));
           }

           UnMasteredWordList = dao.getAllUnmasteredWords();
          
       }
        #endregion

       public void checkNAdd(Word word)
        {
            Word word2BChanged = new Word();
            if (WordList.Contains(word))
            {
                WordList.ElementAt<Word>(WordList.IndexOf(word)).AdditionalInfo = word.AdditionalInfo;
                WordDaoImpl dao = new WordDaoImpl();
                dao.updateNote(word);
            }
        }
        public WordListDiffMeaning searchWord(String word)
        {
            WordListDiffMeaning sameWordDifferentMeaning = new WordListDiffMeaning();
            sameWordDifferentMeaning= WDictionary.getWordForName2(word);
            return sameWordDifferentMeaning;
        }

       public List<Word> getWord4Name(String word)
       {
           List<Word> sameWordDiffMeaning = WDictionary.getWordForName(word);
           return sameWordDiffMeaning;
       }

      
        #region --Search--
        public List<string> getAllWordsStarting(string substring)
        {
            List<string> matchFound = new List<string>();
            foreach (string s in WordsNameOnlyList)
            {
                if(s.Contains(substring))
                    matchFound.Add(s);
                
            }
            return matchFound;
        }
        #endregion

        #region Mastered Parameter
        public void markWordAsMastered(Word currentWord)
        {
            WordDaoImpl dao = new WordDaoImpl();
            dao.updateNote(currentWord);
            UnMasteredWordList.Remove(currentWord);

        }

       public List<Word> getNUnmasteredWords(int N,int i=0)
       {
           return UnMasteredWordList.GetRange(i*N, N);
       }
        #endregion

       public List<String>  getAllWordsNameOnly()
       {
           return WordsNameOnlyList.AsEnumerable().ToList();
       }
    }
}
