using System.ComponentModel;
using System.Runtime.CompilerServices;
using moe.Jixun.Plugin;

namespace moe.Jixun.model
{
    public class ChapterEntity: INotifyPropertyChanged
    {
        private IBookChapter _chapter;
        public IBookChapter Chapter
        {
            get { return _chapter; }
            set { _chapter = value; OnPropertyChanged(); }
        }
        
        public ChapterEntity()
        {
        }
        
        public ChapterEntity(IBookChapter chapter)
        {
            Chapter = chapter;
        }

        public void SetChapter(IBookChapter chapter)
        {
            Chapter = chapter;
        }


        private bool _checked;
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; OnPropertyChanged(); }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}