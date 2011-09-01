using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordListW.Utils;

namespace WordListW.Core
{
    public class Word
    {
        string word;
        
        public string Name { get { return word; } set { word = value; } }
        public string Meaning {get; set; }
        public string Sentence { get; set; }
        public string AdditionalInfo{get; set;}
        public bool IsMastered { get; set; }
        public List<String> Synonyms { get; set; }
        public List<String> Antonyms { get; set; }
        public List<String> Homonyms { get; set; }

        public string Sound { get; set; }
        public string Image { get; set; }

        public string Root { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        public string word2Text()
        {
            return word + "," + Meaning + "@#" + AdditionalInfo + "\r\n";
        }

        #region text-word
        public static Word text2Word(String text2word)
        {
            Word word = new Word();
            string copy = text2word;

            int firstCommaIndex = copy.IndexOf(",");
            

            string word2 = null;
            string meaning = null;
            string additionalMeaning = null;

            try
            {
                word2 = copy.Substring(0, firstCommaIndex);
                string wordMeaningPlusAdditionalInfo = copy.Substring(firstCommaIndex + 1);
                int additionalInfoIndex = wordMeaningPlusAdditionalInfo.IndexOf("@#");
                if (additionalInfoIndex != -1)
                {
                    meaning = copy.Substring(firstCommaIndex + 1, additionalInfoIndex);
                    additionalMeaning = wordMeaningPlusAdditionalInfo.Substring(additionalInfoIndex + 2);
                    additionalMeaning = additionalMeaning.Replace("\n","");
                    additionalMeaning = additionalMeaning.Replace("\r", "");
                }
                else
                {
                    meaning = copy.Substring(firstCommaIndex + 1);
                    additionalMeaning = null;
                }
            }
            catch (Exception e)
            {
                
            }

            if (word2 != null)
            {
                word.Name = word2;

                word.Meaning = meaning;
                word.AdditionalInfo = additionalMeaning;
            }
            return word;
        }
        #endregion
        public override String ToString()
        {
            return Name;
        }

//        public override bool Equals(Object wordObj)
//        {
//            if (wordObj is Word)
//           return false;
//            Word word = (Word) wordObj;
//            if(wordObj!=null)
//            if (this.Name.Equals(word.Name) && this.Meaning.Equals(word.Meaning))
//            {
//                return true;
//            }
//            return false;
//        }

    }
}
