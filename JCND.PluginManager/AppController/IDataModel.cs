using System.Collections.Generic;

namespace moe.Jixun.Plugin.AppController
{
    public interface IDataModel
    {
        void ClearSearchResults();
        void SetStatusText(string newStatusText);
        void AddSearchResult(IBookMeta book);

        /// <summary>
        /// 弹出导出插件选择框
        /// 并引导用户选择其中一个。
        /// </summary>
        /// <param name="exportPlugins">导出插件列表</param>
        /// <param name="prefPackage">上次记录的包名</param>
        /// <param name="rememberChoice">是否默认勾选 “不再询问”</param>
        /// <returns></returns>
        IExportPluginAnswer OpenExportPluginChoice(
            List<IPluginExport> exportPlugins,
            IPluginExport prefPackage,
            bool rememberChoice
        );
    }
}
