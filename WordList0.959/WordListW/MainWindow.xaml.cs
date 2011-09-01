using System;
using System.Collections.Generic;
using System.Data;
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
using WordListW.Controls;
using WordListW.Core;
using WordListW.Dao;
using WordListW.RemoveInFuture;
using Xceed.Wpf.DataGrid;
using Xceed.Wpf.DataGrid.Converters;
using Xceed.Wpf.DataGrid.Print;
using Xceed.Wpf.DataGrid.Stats;
using Xceed.Wpf.DataGrid.ValidationRules;
using Xceed.Wpf.DataGrid.Views;
using Xceed.Wpf.DataGrid.Views.Surfaces;
using Xceed.Wpf.Controls;
using Xceed.Wpf.DataGrid.ThemePack;
using Xceed.Wpf.DataGrid.Settings;
using Xceed.Wpf.DataGrid.Export;
using CheckBox = System.Windows.Controls.CheckBox;


namespace WordListW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        AvailableWords wordDictionary;

        private WordATime wordATimeWin;

      

       

        public MainWindow()
        {

            initializeBackend();
            InitializeComponent();
            wordIterator.WordDictionary = wordDictionary;
            wordIterator.initializeUI();

            wordATimeWin = new WordATime();
           
            wordATimeWin.init(wordDictionary.UnMasteredWordList[0], wordDictionary.UnMasteredWordList);
            focusWords.windowWordATime = wordATimeWin;
            focusWords.WordDictionary = wordDictionary;
            focusWords.initializeUI();
            
            //wordDictionary.searchWord("abacus");

            search1.setUI(wordDictionary);

           // wordTab1.CurrentWord = wordDictionary.WordList[0];
          //  wordTab1.initializeUI();

          
        }

        public void initializeBackend()
        {
            wordDictionary = new AvailableWords();
           
            WordListDiffMeaning meaningList = new WordListDiffMeaning();
  
        }


        private void Window_Initialized(object sender, EventArgs e)
        {
            Xceed.Wpf.DataGrid.Licenser.LicenseKey = "DGP40-5WEA3-FRL84-9YXA";



        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                wordATimeWin.Owner = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

       

   

 

//        private void btnMastered_Click(object sender, RoutedEventArgs e)
//        {
//            WordDaoImpl wordDao = new WordDaoImpl();
//            List<Word> words = (List<Word>)xdgFocusWords.ItemsSource;
//
//            MessageBox.Show(xdgFocusWords.SelectedItems.Count.ToString());
//            if(xdgFocusWords.SelectedItems.Count!=0)
//            {
//                MessageBox.Show("updatedd");
//                foreach (Object selectedWord in xdgFocusWords.SelectedItems)
//                {
//                    Word word = (Word) selectedWord;
                    //word.IsMastered = true;
//                    MessageBox.Show(word.IsMastered.ToString());
//                    wordDao.updateNote((Word)selectedWord);
//                }
//            }
//            xdgFocusWords.ItemsSource = wordDao.getFirstNunmasteredWords(15);
//        }


//        private void xdgFocusWords_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
//        {
//            windowWordATime.Show((Word)xdgFocusWords.SelectedItem);
//            windowWordATime.Focus();
//        }
    }
}
