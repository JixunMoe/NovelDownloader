using System.Collections.Generic;
using System.Threading.Tasks;

namespace moe.Jixun.Plugin
{
    public interface IPluginExport: IPluginBase
    {
        Task DownloadChapters(List<IBookChapter> chapters);
    }
}