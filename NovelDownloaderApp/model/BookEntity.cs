using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using moe.Jixun.Plugin;

namespace moe.Jixun.model
{
    class BookEntity: INotifyPropertyChanged
    {
        private IBookMeta _bookmeta;
        public IBookMeta BookMeta
        {
            get { return _bookmeta; }
            set { _bookmeta = value; OnPropertyChanged(); }
        }

        public BookEntity()
        {
        }

        public BookEntity(IBookMeta bookMeta)
        {
            BookMeta = bookMeta;
        }

        public void SetBook(IBookMeta bookMeta)
        {
            BookMeta = bookMeta;
        }

        public virtual List<ChapterEntity> Chapters
            => BookMeta
                .Chapters
                .Select(chapter => new ChapterEntity(chapter))
                .ToList();

        public virtual string Site => BookMeta.Plugin.DisplayName;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
