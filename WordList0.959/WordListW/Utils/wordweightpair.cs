using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WordListW.Utils
{
    class wordweightpair : IComparable
    {
        public int probabillity_weight;
        public string word;
        public wordweightpair(int p, string w)
        {
            probabillity_weight = p;
            word = w;
        }
        public int CompareTo(object cl)
        {
            if (typeof(wordweightpair) == cl.GetType())
            {
                return this.probabillity_weight - ((wordweightpair)cl).probabillity_weight;
            }
            else
            {
                return 0;
            }
        }
    }
    class spell
    {
        private Dictionary<string, int> nWords = new Dictionary<string, int>();
        private List<char> alphabets;
        public spell(String dict)
        {

            /*this is contructor,
             pass dict (a long text of any language (of which you want to build the spell checker)
             all dictionary will be built from this.
            */
            int pos = 0;
            Regex wordpat = new Regex(@"[a-z]+", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = null;
            while ((m = wordpat.Match(dict, pos)).Success)
            {
                string cur_val = m.Value.ToLower();
                if (nWords.ContainsKey(cur_val))
                {
                    nWords[cur_val] = nWords[cur_val] + 1;
                }
                else
                {
                    nWords.Add(cur_val, 0);
                }
                pos = m.Index + m.Length;
            }
            buildalphabet();
        }
        private void buildalphabet()
        {
            alphabets = new List<char>();
            for (char c = 'a'; c <= 'z'; c++)
            {
                alphabets.Add(c);
            }
        }
        private List<string> edits(string word)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < word.Length; ++i)
            {
                /*Delete*/
                result.Add(word.Substring(0, i) + word.Substring(i + 1));
            }
            for (int i = 0; i < word.Length - 1; ++i)
            {
                /*transpose*/
                result.Add(word.Substring(0, i) + word.Substring(i + 1, 1) + word.Substring(i, 1) + word.Substring(i + 2));
            }
            for (int i = 0; i < word.Length; ++i)
            {
                /*alter*/
                foreach (char c in alphabets)
                {
                    result.Add(word.Substring(0, i) + c + word.Substring(i + 1));
                }
            }
            for (int i = 0; i <= word.Length; ++i)
            {
                /*insert*/
                foreach (char c in alphabets)
                {
                    result.Add(word.Substring(0, i) + c + word.Substring(i));
                }
            }
            return result;
        }
        public string correct(string word)
        {
            word = word.ToLower();
            if (nWords.ContainsKey(word))
            {
                return word;
            }
            List<string> list = edits(word);
            List<wordweightpair> candidates = new List<wordweightpair>();


            foreach (string s in list)
            {
                if (nWords.ContainsKey(s))
                {
                    candidates.Add(new wordweightpair(nWords[s], s));
                }
            }
            if (candidates.Count > 0)
            {
                candidates.Sort();
                return candidates[0].word;
            }
            foreach (string s in list)
            {
                List<string> list2 = edits(s);/*second level of edits*/
                foreach (string ss in list2)
                {
                    if (nWords.ContainsKey(ss))
                    {
                        candidates.Add(new wordweightpair(nWords[ss], ss));
                    }
                }
            }
            if (candidates.Count > 0)
            {
                candidates.Sort();
                return candidates[0].word;
            }
            return "NOT FOUND";
        }

    }

}
