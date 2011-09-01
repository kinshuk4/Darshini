using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordListW.Core
{
    /// <summary>
    /// DataStructure for holding a Name(a word as a string) and list of words with different meaning.
    ///
    /// </summary>
    public class WDictionary
    {
     
        Dictionary<String, List<Word>> wordDic;// = new Dictionary<String, List<Word>>();
        public int Count { get { return wordDic.Count; } }
        public WDictionary()
        {
            
            wordDic = new Dictionary<String, List<Word>>();
        }

        public List<Word> getWordList(string word)
        {
            if (wordDic.ContainsKey(word))
            {
                List<Word> xordList4word = wordDic[word];
                return xordList4word;
            }
            return null;
        }
        public WordListDiffMeaning getWordForName2(string word)
        {
            WordListDiffMeaning wordList = new WordListDiffMeaning();
            wordList.WordKey = word;
            List<Word> sameWordDiffMeaning=getWordList(word);
            foreach(Word listedword in sameWordDiffMeaning)
            {
                wordList.MeaningList.Add(listedword.Meaning);
            }
            return wordList;
        }

        public List<Word> getWordForName(string word)
        {
            if (wordDic.ContainsKey(word))
            {
                List<Word> sameWordDiffMeaning = wordDic[word];
                return sameWordDiffMeaning;
            }
            return null;
        }

        public void Add(Word wordWithSameName)
        {
            List<Word> correspondingWordList=new List<Word>();
           
                if (wordDic.ContainsKey(wordWithSameName.Name))
                {
                    correspondingWordList = wordDic[wordWithSameName.Name];
                    correspondingWordList.Add(wordWithSameName);
                }
                else
                {
                    correspondingWordList.Add(wordWithSameName);
                    wordDic.Add(wordWithSameName.Name, correspondingWordList);
                }
            
        }

        
    }
}
