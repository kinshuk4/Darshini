using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordListW.Core;

namespace WordListW.Dao
{
  public  interface IWordDao
    {
      List<Word> getWord(String word);
       List<Word> getAllWords();
       List<Word> getFirstNunmasteredWords(int n);
       void insertWord(Word word);
       void updateNote(Word word);


       List<Word> getAllUnmasteredWords();
    }
}
