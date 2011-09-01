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

namespace WordListW.Controls
{
    /// <summary>
    /// Interaction logic for WordIterator.xaml
    /// </summary>
    public partial class WordIterator : UserControl
    {
        bool additionalInfoFlag;
        private AvailableWords wordDictionary;
        private Word currentWord;
        private WordListIterator myIterator;
        public AvailableWords WordDictionary
        {
            get { return wordDictionary; }
            set { wordDictionary = value; }
        }
       

        public WordIterator()
        {
            InitializeComponent();
            tblSentence.Text = "";
            currentWord = new Word();
           
        }


         public void initializeUI(){

             additionalInfoFlag = false;
             myIterator = new WordListIterator(wordDictionary.WordList);

             string[] listTypes = {"All Words", "UnMastered Words", "Mastered Words"};
             cbxListSelector.ItemsSource = listTypes;
             cbxListSelector.SelectedIndex = 0;
            
            lblNoOfWords.Content += wordDictionary.Count.ToString();
            lblWords2Master.Content += wordDictionary.UnMasteredCount.ToString();
            lblWordsMastered.Content += wordDictionary.MasteredCount.ToString();
            setText(myIterator.First());
             
        }

        private void setText(Word word)
        {
            txtWord.Text = word.Name;
            txtMeaning.Text = word.Meaning;
            txtNote.Text = word.AdditionalInfo;
            tblSentence.Text = word.Sentence;
        }

        private Word getText()
        {
            Word editedWord = new Word();
            editedWord.Name = txtWord.Text.ToString();
            editedWord.Meaning = txtMeaning.Text.ToString();
            editedWord.AdditionalInfo = txtNote.Text;
            return editedWord;
        }
        private void changeAvailableWords()
        {
            Word editedWord = new Word();
            editedWord = getText();
            wordDictionary.checkNAdd(editedWord);
        }
        private void checkNotePresent()
        {
            if (additionalInfoFlag)
                changeAvailableWords();
        }

        #region --Iterator Button--
        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            enableFirst2();
            checkNotePresent();
            Word word = myIterator.Next();
            if (word != null)
            {
                setText(word);
            }
            else if(myIterator.Current==myIterator.Count)
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
            checkNotePresent();
            setText(myIterator.Last());
            disableLast2();
            enableFirst2();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            checkNotePresent();
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
            checkNotePresent();
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

        private void txtNote_TextChanged(object sender, TextChangedEventArgs e)
        {
            additionalInfoFlag = true;
        }

        private void chkBxIsMastered_Checked(object sender, RoutedEventArgs e)
        {
            currentWord = getText();
            wordDictionary.markWordAsMastered(currentWord);
            btnForward_Click(sender,e);
        }

        private void cbxListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbxListSelector.SelectedValue.ToString())
            {
                case "All Words" : myIterator.setInit(WordDictionary.WordList);
                    setText(myIterator.First());
                    break;
                case "UnMastered Words" :myIterator.setInit(WordDictionary.UnMasteredWordList);
                    setText(myIterator.First());
                    break;
                case "Mastered Words": myIterator.setInit(WordDictionary.WordList);
                    setText(myIterator.First());
                    break;
            }
        }

    }
}
