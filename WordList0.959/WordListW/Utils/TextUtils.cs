using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WordListW.Utils
{
    public class TextUtils
    {
        public static void createFile(string filePath, string fileName)
        {
            StreamWriter sw;
            sw = File.CreateText(filePath + "/" + fileName + ".txt");
        }
        public static string readFile(string filePath)
        {
            StreamReader tr = new StreamReader(filePath);
           
            string text = tr.ReadToEnd();
            tr.Close();

            return text;
        }
        public static void dumpTextToFile(string filePath, string text)
        {
            StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine(text);
            sw.Close();
        }

    }
}
