using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using moe.Jixun.Dummies;
using moe.Jixun.Plugin;

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
    }
}