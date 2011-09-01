using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordListW.Core
{
    public class WordListIterator
    {
        public int Current { get; set; }
        public int Count { get; private set; }
        public List<Word> WordList { get; set; }

        private WordListIterator()
        {
            Current = 0;
            
        }
        public void setInit(List<Word> wordList_)
        {
            Current = 0;
            WordList = wordList_;
            Count = wordList_.Count;
        }
        public WordListIterator(List<Word> wordList_):this()
        {
            WordList = wordList_;
            Count = wordList_.Count;
        }

        
        public Word currentWord(int current)
        {
            return WordList.ElementAt<Word>(current);
        }

        public Word Next()
        {
            if (Current <= Count)
                return WordList.ElementAt<Word>(Current++);
            else
                return null;
        }
        public Word Back()
        {
            if (Current >= 0)
                return WordList.ElementAt<Word>(Current--);
            else
                return null;
        }
        public Word First()
        {
            Current = 0;
            return WordList.ElementAt<Word>(Current);
        }
        public Word Last()
        {
            Current = WordList.IndexOf(WordList.Last<Word>());
            return WordList.Last<Word>();
        }

    }
}
