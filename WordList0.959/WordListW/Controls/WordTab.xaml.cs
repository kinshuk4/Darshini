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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WordListW.Core;
using WordListW.Dao;

namespace WordListW.Controls
{
    /// <summary>
    /// Interaction logic for WordTab.xaml
    /// </summary>
    public partial class WordTab : UserControl
    {
        private bool isSynonymPopulated;

        public AvailableWords WordDictionary { get; set; }
        public WordTab()
        {
            InitializeComponent();
            
        }

        public Word CurrentWord{ get; set;}
        public List<Word> WordList { get; set; }

        private WordListIterator myIterator;
        public void initializeBackend(List<Word> wordList,Word word_)
        {
            isSynonymPopulated = false;
            WordList = wordList;
            CurrentWord = word_;
             myIterator = new WordListIterator(WordList);
        }
        public void initializeUI()
        {

            tblkWord.Text = CurrentWord.Name;

            populateSynonyms();
            homoPhone1.initializeUI(CurrentWord);
            

        }
        public void setText(Word word_)
        {
            CurrentWord = word_;
            tblkWord.Text = CurrentWord.Name;
            tbMeaning.Text = CurrentWord.Meaning;
            populateSynonyms();
        }

        private void populateSynonyms()
        {
            if(!isSynonymPopulated){
              WordRelation relation = new WordRelation();
             List<string> words = relation.getSynonymsForWord(CurrentWord.Name);
              populateStack(stackSynonym,words);
                isSynonymPopulated = true;

            }
            else
            {
                depopulateStack(stackSynonym);
                WordRelation relation = new WordRelation();
                List<string> words = relation.getSynonymsForWord(CurrentWord.Name);
                populateStack(stackSynonym, words);
            }

     }

        private void populateStack(StackPanel stk , List<string > words)
        {
            try
            {
                if (words.Count != 0)
                {
                    foreach (String s in words)
                    {
                        Button linkButton = new Button();
                        linkButton.Content = s;
                        linkButton.Style = alfa.Style;
                        linkButton.FontWeight = alfa.FontWeight;
                        linkButton.FontStyle = alfa.FontStyle;
                        linkButton.FontFamily = alfa.FontFamily;
                        linkButton.FontSize = alfa.FontSize;
                        linkButton.Click += new RoutedEventHandler(linkButton_Click);
                        stackSynonym.Children.Add(linkButton);

                        TextBlock tb = new TextBlock();
                        tb.Text = "   ";
                        stackSynonym.Children.Add(tb);
                    }
                }
            }catch(Exception e)
            {
            }
        }

        private void depopulateStack(StackPanel stk)
        {
            stk.Children.RemoveRange(0,stk.Children.Count);
        }
        void linkButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button) sender;
            MessageBox.Show(btn.Content.ToString());
            try{
            this.setText(WordDictionary.getWord4Name(btn.Content.ToString())[0]);
                }catch(Exception xe)
                {
                    MessageBox.Show("Word does not exist in dictionary");
                    lblMessage.Content = "Word does not exist in dictionary";
                }

        }


        #region Iterator Buttons
        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            enableFirst2();
            Word word = myIterator.Next();
            if (word != null)
            {
                setText(word);
            }
            else if (myIterator.Current == myIterator.Count)
            {
                MessageBox.Show("Reached end of words");
                disableLast2();
            }
            else
            {
                MessageBox.Show("Reached end of words");
                disableLast2();
            }


        }
        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
           
            setText(myIterator.Last());
            disableLast2();
            enableFirst2();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
      
            Word word = myIterator.Back();
            if (word != null)
            {
                setText(word);
            }
            else
            {
                MessageBox.Show("Reached start of words");
                disableFirst2();
                enableLast2();
            }
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
          
            setText(myIterator.First());
            disableFirst2();
            enableLast2();
        }
        #region Enabling-Disabling
        private void enableFirst2()
        {
            btnBack.IsEnabled = true;
            btnFirst.IsEnabled = true;
        }
        private void disableFirst2()
        {
            btnBack.IsEnabled = false;
            btnFirst.IsEnabled = false;
        }
        private void enableLast2()
        {
            btnForward.IsEnabled = true;
            btnLast.IsEnabled = true;
        }
        private void disableLast2()
        {
            btnForward.IsEnabled = false;
            btnLast.IsEnabled = false;
        }
        #endregion
        #endregion

    }
}
