using System;
using System.Diagnostics;
using System.IO;
using System.Management.Instrumentation;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using moe.Jixun.model;
using moe.Jixun.Plugin;

namespace moe.Jixun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static MainWindow Instance { get; set; }
        private PluginManager _plugins;
        private Global _data;

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();

            _data = Global.Instance;

            DataContext = _data;





            var location = Assembly.GetExecutingAssembly().Location;
            // ReSharper disable once AssignNullToNotNullAttribute
            var pluginDirectory = Path.Combine(Path.GetDirectoryName(location), "Plugin");


            _plugins = new PluginManager(_data);
            _plugins.LoadPlugins(pluginDirectory);
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            await _plugins.SearchBookAsync(TextSearchBox.Text);
        }

        private async void BookSearchLists_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var book = BookSearchLists.SelectedItem as BookEntity;
            if (book == null)
            {
                Debug.WriteLine("No book was selected.");
                return;
            }

            _data.StatusText = "正在获取书籍信息...";


            _data.StatusText = $"正在下载 {book.BookMeta.Name} 章节列表...";
            try
            {
                await book.BookMeta.DownloadChapterList();
            }
            catch (Exception ex)
            {
                _data.StatusText = $"{book.BookMeta.Name} 的章节列表获取失败!";
            }

            var bookDialog = new ChaptersList(book)
            {
                Owner = this
            };

            bookDialog.Show();

            _data.StatusText = "章节列表加载完成!";
        }
    }
}
