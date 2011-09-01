using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WordListW.Core;

namespace WordListW
{
    /// <summary>
    /// Interaction logic for WordATime.xaml
    /// </summary>
    public partial class WordATime : Window
    {
        private Word currentWord;

        public WordATime()
        {
            InitializeComponent();
            
        }

        

        public Word CurrentWord { get { return currentWord; }
            private set { currentWord = value; }
        }
        public List<Word> UnMasteredWord { get; set; }

        public void init(Word word, List<Word> wordList)
        {
            CurrentWord = word;
            UnMasteredWord = wordList;

            wordTab1.initializeBackend(UnMasteredWord,currentWord);
            
        }
        public void Show(Word word)
        {
            currentWord = word;
            
            this.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            this.Hide();
            e.Cancel = true;

        }



       

    }
}
