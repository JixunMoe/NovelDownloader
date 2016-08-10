using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using moe.Jixun.model;

namespace moe.Jixun
{
    /// <summary>
    /// Interaction logic for ChaptersList.xaml
    /// </summary>
    public partial class ChaptersList : Window
    {
        internal ChaptersList(BookEntity book)
        {
            InitializeComponent();
            DataContext = book;
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListChapters_OnKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void ListChapters_OnKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void BtnToggleSelected_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var selectedItem in ListChapters.SelectedItems)
            {
                var chapter = selectedItem as ChapterEntity;

                if (chapter != null)
                    chapter.Checked = !chapter.Checked;
            }
        }
    }
}
