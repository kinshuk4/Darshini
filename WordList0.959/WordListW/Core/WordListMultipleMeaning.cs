using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordListW.Core
{
    public class WordListDiffMeaning
    {
        String wordKey;
        List<String> meaningList;
        public WordListDiffMeaning()
        {
             meaningList = new List<string>();
        }

        public WordListDiffMeaning(List<Word> sameWordDiffMeaningList)
        {
            wordKey = sameWordDiffMeaningList[0].Name;
            meaningList = new List<string>();
            foreach (Word word in sameWordDiffMeaningList)
            {
                Add(word);
            }
        }
        


        public string WordKey { get { return wordKey; } set { wordKey = value; } }

       public List<String> MeaningList { get{return meaningList;} set{meaningList=value;} }

       public void Add(Word word)
       {
           meaningList.Add(word.Meaning);
       }

      
    }
}
