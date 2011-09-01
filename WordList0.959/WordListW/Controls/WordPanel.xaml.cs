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
    /// Interaction logic for WordPanel.xaml
    /// </summary>
    public partial class WordPanel : UserControl
    {
        private Word _word;
        public WordPanel()
        {
            InitializeComponent();
            tbkMeaning.Text = "";
            tbkWord.Text = "";
            tbkSentence.Text = "";
        }

        public void setUI(Word word)
        {
            _word = word;
            tbkWord.Text = word.Name;
            tbkMeaning.Text = word.Meaning;
            tbkSentence.Text = word.Sentence;
        }

    }
}
