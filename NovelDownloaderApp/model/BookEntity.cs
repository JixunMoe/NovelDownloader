using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
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
            UpdateChapters();
        }
        
        public virtual List<ChapterEntity> Chapters { get; protected set; }

        public void UpdateChapters()
        {
            Chapters = BookMeta
                   .Chapters
                   .Select(chapter => new ChapterEntity(chapter))
                   .ToList();
            
            Debug.WriteLine($"Updated chapters! Got {Chapters.Count} chapters.");
        }

        public virtual List<IBookChapter> SelectedChaptersForPlugin
            => Chapters
                .Where(chapter => chapter.Checked)
                .Select(chapter => chapter.Chapter).ToList();


        public virtual string Site => BookMeta.Plugin.DisplayName;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
