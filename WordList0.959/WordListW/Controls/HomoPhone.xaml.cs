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
    /// Interaction logic for HomoPhone.xaml
    /// </summary>
    public partial class HomoPhone : UserControl
    {
        
        public string Word { get; set; }
        public HomoPhone()
        {
            InitializeComponent();
           
        }

        public void initializeUI(Word word)
        {
            Word = word.Name;
            Homophones hp = new Homophones();

            try
            {
                List<Word> stringList = hp.getHomonymsWords(Word);
            }
            catch(Exception e)
            {
               
            }
            xdgHomoPhones.ItemsSource = hp.getHomonymsForWord("accessory");
        }
    }
}
