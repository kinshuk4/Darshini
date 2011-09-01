using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WordListW.Utils
{
    public class CsvUtils
    {
        /*default token is comma*/
        public static string[] getTokensFromFile(string filePath)
        {
            string text = TextUtils.readFile(filePath);
            string[] words = text.Split(',');
            return words;
        }
        public static string[] getTokensFromFile(string filePath, char token)
        {
            string text = TextUtils.readFile(filePath);
            string[] words = text.Split(token);
            return words;
        }
        public static string[] getTokensFromFile(string filePath, char[] token)
        {
            string text = TextUtils.readFile(filePath);
            string[] words = text.Split(token);
            return words;
        }
        public static string[] getTokensFromFile(string filePath, string token)
        {
            string text = TextUtils.readFile(filePath);
            string[] words = text.Split(token.ToCharArray());
            return words;
        }
        public static string[] getTokensFromString(string text)
        {
            string[] words = new string[10];

            words = text.Split(',');
            return words;
        }
        public static string[] getTokensFromString(string text, string token)
        {
            string[] words = text.Split(token.ToCharArray());
            return words;
        }
        public static string[] getTokensFromString(string text, char token)
        {
            string[] words = new string[10];

            words = text.Split(token);
            return words;
        }
        public static void addTokensToFile(string filePath, string token)
        {
            string text = TextUtils.readFile(filePath);
            text += "," + token;
            TextUtils.dumpTextToFile(filePath, text);
        }








    }
}
