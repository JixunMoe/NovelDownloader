using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
using moe.Jixun.Plugin;
using Microsoft.Win32;

namespace moe.Jixun
{
    /// <summary>
    /// Interaction logic for ChaptersList.xaml
    /// </summary>
    public partial class ChaptersList : Window
    {
        internal static SaveFileDialog sfd = new SaveFileDialog();

        private readonly BookEntity _book;

        internal ChaptersList(BookEntity book)
        {
            InitializeComponent();
            
            _book = book;
            DataContext = _book;

            // 从原模型拉取章节数据
            _book.UpdateChapters();

#if DEBUG
            BtnDebugView.Click += (sender, args) =>
            {
                foreach (var chapter in _book.Chapters)
                {
                    Debug.WriteLine($"Chapter: {chapter.Chapter.Name} / {chapter.Checked}");
                }
            };
#else
            BtnDebugView.Visibility = Visibility.Collapsed;
#endif
        }

        private IEnumerable<ChapterEntity> ChaptersToChange
            => ListChapters.SelectedItems.Count == 0
                ? ListChapters.Items.Cast<ChapterEntity>()
                : ListChapters.SelectedItems.Cast<ChapterEntity>();

        private List<IBookChapter> SelectedChapters
            => _book.Chapters
                .Where(c => c.Checked)
                .Select(c => c.Chapter)
                .ToList();

        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            var exportPlugin = PluginManager.Instance.GetExportPlugin();
            if (exportPlugin == null)
            {
                Debug.WriteLine("No export plugin selected (null)");
                return;
            }

            await exportPlugin.DownloadChapters(SelectedChapters);
        }

        private void BtnSelectAll_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var chapter in ChaptersToChange)
            {
                chapter.Checked = true;
            }
        }

        private void BtnSelectNone_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var chapter in ChaptersToChange)
            {
                chapter.Checked = false;
            }
        }

        private void BtnToggleAll_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var chapter in ChaptersToChange)
            {
                chapter.Checked = !chapter.Checked;
            }
        }
    }
}
