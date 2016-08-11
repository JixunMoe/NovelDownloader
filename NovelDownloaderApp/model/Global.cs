using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using moe.Jixun.Dummies;
using moe.Jixun.Plugin;
using moe.Jixun.Plugin.AppController;
using moe.Jixun.Properties;

namespace moe.Jixun.model
{
    internal class Global: INotifyPropertyChanged, IDataModel
    {
        public static Global Instance;

        static Global()
        {
            Instance = new Global();
        }
        
        public Global()
        {
            SearchResults = new ObservableCollection<BookEntity>();

#if DEBUG
            for (int i = 0; i < 10; i++)
                SearchResults.Add(new DummyBookEntity());
#endif
        }

        private string _statusText = "Test";
        public string StatusText
        {
            get { return _statusText; }
            set { _statusText = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BookEntity> _searchresults;
        public ObservableCollection<BookEntity> SearchResults
        {
            get { return _searchresults; }
            set { _searchresults = value; OnPropertyChanged(); }
        }








        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ClearSearchResults()
        {
            SearchResults.Clear();
        }

        public void SetStatusText(string newStatusText)
        {
            StatusText = newStatusText;
        }

        public void AddSearchResult(IBookMeta book)
        {
            SearchResults.Add(new BookEntity(book));
        }

        public IExportPluginAnswer OpenExportPluginChoice(
            List<IPluginExport> exportPlugins,
            IPluginExport prefPackage,
            bool rememberChoice)
        {
            int defIndex = exportPlugins.IndexOf(prefPackage);
            var dlg = new SelectOptionWindow("插件", exportPlugins.Select(p => p.DisplayName).ToList(),
                defIndex, rememberChoice);
            dlg.ShowDialog();
            var answer = dlg.GetResult();
            var plugin = answer.Cancel ? null : exportPlugins[answer.Index];
            return new ExportPluginAnswer(answer.DoNotAsk, plugin, answer.Cancel);
        }

        private class ExportPluginAnswer : IExportPluginAnswer
        {
            public bool Remember { get;}
            public IPluginExport Plugin { get; }
            public bool Cancel { get; }

            public ExportPluginAnswer(bool remember, IPluginExport plugin, bool cancel)
            {
                Remember = remember;
                Plugin = plugin;
                Cancel = cancel;
            }
        }
    }
}