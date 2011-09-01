using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;
using WordListW.Core;
using WordListW.Dao;


namespace WordListW.Controls
{
    /// <summary>
    /// Interaction logic for FocusWords.xaml
    /// </summary>
    public partial class FocusWords : UserControl
    {

        public WordATime windowWordATime{ get; set;}
        private int NumberOfWords;
        private int focusCounter;
        public AvailableWords WordDictionary{ get; set;}
        public List<Word> FocusList { get; set; }
        private ObservableCollection<Word> personList = new ObservableCollection<Word>();

        public FocusWords()
        {
            InitializeComponent();
            NumberOfWords = 15;
            focusCounter = 0;
            
         //   initializeUI();
        }

        public void initializeUI()
        {


             FocusList = WordDictionary.getNUnmasteredWords(NumberOfWords, 0);

            xdgFocusWords.ItemsSource = FocusList;
            lblTotalWordCount.Content += WordDictionary.Count.ToString();
            lblMasteredWordCount.Content += WordDictionary.MasteredCount.ToString();
            lblUnMasteredWordCount.Content += WordDictionary.UnMasteredCount.ToString();

        }

        private void chkbxIsMastered_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // MessageBox.Show("double");
            Xceed.Wpf.Controls.CheckBox chk = (Xceed.Wpf.Controls.CheckBox)sender;
            chk.IsChecked = true;

        }

        private void chkbxIsMastered_MouseEnter(object sender, MouseEventArgs e)
        {
            tbInfo.Text =
                "Check the box for marking word as \"Mastered\", and double click for more information like synonyms(Currently synonyms are supported)";
        }




        private void xdgFocusWords_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!windowWordATime.IsActive)
            {
                Word currWord = (Word) xdgFocusWords.SelectedItem;
                windowWordATime.wordTab1.setText(currWord);
                windowWordATime.wordTab1.WordDictionary = WordDictionary;
                windowWordATime.Show();
                
            }
        }



        private void chkbxIsMastered_Checked(object sender, RoutedEventArgs e)
        {
            Xceed.Wpf.Controls.CheckBox chk = (Xceed.Wpf.Controls.CheckBox)sender;
            if(chk.IsChecked!=true) chk.IsChecked = true;
           
            Word word = (Word)xdgFocusWords.SelectedItem;
         //   MessageBox.Show("check" + word);

            WordDaoImpl wordDao = new WordDaoImpl();
            Word selectedWord = (Word)xdgFocusWords.SelectedItem;
           // MessageBox.Show(selectedWord.IsMastered.ToString());
            selectedWord.IsMastered = true;
            wordDao.updateNote((Word)selectedWord);
            //MessageBox.Show(selectedWord.Name);
            // xdgFocusWords.Columns.Clear();
            xdgFocusWords.ItemsSource =
            wordDao.getFirstNunmasteredWords(15);
        }

//
//        private void chkbxIsMastered_Checked(object sender, RoutedEventArgs e)
//        {
//
//
//            WordDaoImpl wordDao = new WordDaoImpl();
//            Word selectedWord = (Word)xdgFocusWords.SelectedItem;
//            MessageBox.Show(selectedWord.IsMastered.ToString());
//            selectedWord.IsMastered = true;
//            wordDao.updateNote((Word)selectedWord);
//            MessageBox.Show(selectedWord.Name);
            // xdgFocusWords.Columns.Clear();
//            xdgFocusWords.ItemsSource =
//            wordDao.getFirstNunmasteredWords(15);
//        }




        private void chkbxSelectAll_Checked(object sender, RoutedEventArgs e)
        {
//      CheckBox chk = sender as CheckBox; 
//	   bool check = chk.IsChecked.Value; 
//	   if (check)    
//	   {
//           foreach (Word p in FocusList) 
//       { 
//	       personList.Add(p); 
//	           chk = xdgFocusWords.Columns[2].GetCellContent(p) as CheckBox; 
//	           if (chk != null) 
//	           chk.IsChecked = true; 
//	       } 
//   } 
//	   else
//   { 
//	   foreach (Word p in FocusList) 
//       { 
//	       chk = xdgFocusWords.Columns[2].GetCellContent(p) as CheckBox; 
//	       if (chk != null) 
//	       chk.IsChecked = false; 
//	       } 
//   personList.Clear(); 
//	   }
            }

           

          
//            foreach (CheckBox checkBox in chkList)
//            {
//                checkBox.IsChecked = true;
//            })
//            this.xdgFocusWords.EndEdit();
 //       }
//        private void RemoveRowHighlights(object item)
//        {
//            Xceed.Wpf.DataGrid.DataRow row = this.xdgFocusWords.GetContainerFromItem(item) as Xceed.Wpf.DataGrid.DataRow;
//            Xceed.Wpf.DataGrid.DataCell c;
//            if (row != null)
//            {
//                c = row.Cells[0];
//                Xceed.Wpf.Controls.CheckBox chk = (Xceed.Wpf.Controls.CheckBox)c;
//                chk.IsChecked = true;
//            }
//        }

        private void btnNext15_Click(object sender, RoutedEventArgs e)
        {
            xdgFocusWords.ItemsSource = WordDictionary.getNUnmasteredWords(NumberOfWords, ++focusCounter);
        }

        private void xdgFocusWords_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
        List<CheckBox> checkboxes = new List<CheckBox>();




        void chk_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            bool check = chk.IsChecked.Value;
            foreach (CheckBox chkbox in checkboxes)
                chkbox.IsChecked = check;
        }

        private void xdgFocusWords_MouseLeave(object sender, MouseEventArgs e)
        {
            tbInfo.Text = "";
        }








    }
}
