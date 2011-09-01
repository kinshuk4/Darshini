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
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : UserControl
    {
        private AvailableWords wordDictionary;
        public AvailableWords WordDictionary
        {
            get { return wordDictionary; }
            set { wordDictionary = value; }
        }
       
        public Search()
        {
            InitializeComponent();
            lblTryAgain.Text = "";
           
        }

        public void setUI(AvailableWords wordDictionary_)
        {
            wordDictionary = wordDictionary_;
            tbxAutoComplete.ItemsSource = wordDictionary.getAllWordsNameOnly();
        }

        public void setPropertyOnPanel(WordPanel wpanel,Word word)
        {
            wpanel.setUI(word);
            wpanel.BorderBrush = wordPanel1.BorderBrush;
            wpanel.BorderThickness = wordPanel1.BorderThickness;
            wpanel.Width = 300;
          
        }
        public void setWord(Word word)
        {
            WordPanel wordPanel = new WordPanel();
           

            if (stkWordDefinition.Children.Count == 1)
            {
                setPropertyOnPanel(wordPanel,word);
                stkWordDefinition.Children.Add(wordPanel);
            }
            else
            {
                stkWordDefinition.Children.RemoveRange(1, stkWordDefinition.Children.Count);
                setPropertyOnPanel(wordPanel, word);
                stkWordDefinition.Children.Add(wordPanel);

            }
        }
        public void setWords(List<Word> words)
        {
            if(stkWordDefinition.Children.Count!=1)
                stkWordDefinition.Children.RemoveRange(1,stkWordDefinition.Children.Count);
            foreach (Word word in words)
            {
                WordPanel wordPanel = new WordPanel();
                setPropertyOnPanel(wordPanel,word);

                stkWordDefinition.Children.Add(wordPanel);
            }

        }
//        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
//        {
//            lblTryAgain.Text = "";
//            if (e.Key == Key.Enter)
//            {
//                       btnFind_Click(sender,e);
//            }
//            if(e.Key==Key.Delete)
//            {
//                txtSearch.Text = "";
//            }
//            if(e.Key==Key.Down)
//            {
//                
//            }
//        }

      
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            List<Word> sameWordDiffMeaning=getAllMeaningsForWord();
            try
            {
                if (sameWordDiffMeaning.Count==1)
                  this.setWord(sameWordDiffMeaning[0]);
                else if(sameWordDiffMeaning.Count>1)
                {
                    this.setWords(sameWordDiffMeaning);
                }
            }catch(Exception ex)
            {
                lblTryAgain.Visibility = Visibility.Visible;
                lblTryAgain.Text = tbxAutoComplete.autoTextBox.Text + " does not exist in dictionary";
                stkWordDefinition.Children.RemoveRange(1,stkWordDefinition.Children.Count);
            }
           
        }

        private List<Word> getAllMeaningsForWord()
        {

            List<Word> test = wordDictionary.getWord4Name(tbxAutoComplete.autoTextBox.Text);
            return test;
//            wordList = wordDictionary_.searchWord(txtSearch.Text);
//            return wordList;
//            return wordList.MeaningList;
        }

        private void tbxAutoComplete_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key ==Key.Enter)
                btnFind_Click(sender,e);
        }

//        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
//        {
//            String typedString = txtSearch.Text;
//            List<string> autoList =new List<string>();
//            if (typedString.Length >= 1)
//            {
//                autoList = wordDictionary.getAllWordsStarting(typedString);
//            }
//            if(autoList.Count>0)
//            {
//                lbxSuggestions.ItemsSource = autoList;
//                lbxSuggestions.Visibility = Visibility.Visible;
//            }
//            else
//            {
//                lbxSuggestions.ItemsSource = null;
//                lbxSuggestions.Visibility = Visibility.Collapsed;
//            }
//        }


        #region Autolist
//        private void lbxSuggestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            if(lbxSuggestions.ItemsSource!=null)
//            {
//                lbxSuggestions.Visibility = Visibility.Collapsed;
//                txtSearch.TextChanged -= new TextChangedEventHandler(txtSearch_TextChanged);
//
//                if(lbxSuggestions.SelectedIndex!=-1)
//                {
//                    txtSearch.Text = lbxSuggestions.SelectedItem.ToString();
//                    btnFind_Click(sender,e);
//                }
//                txtSearch.TextChanged+=new TextChangedEventHandler(txtSearch_TextChanged);
//            }
//        }
        #endregion



    }
}
