namespace moe.Jixun.Plugin
{
    public interface IDataModel
    {
        void ClearSearchResults();
        void SetStatusText(string newStatusText);
        void AddSearchResult(IBookMeta book);
    }
}
