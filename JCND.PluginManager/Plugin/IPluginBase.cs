namespace moe.Jixun.Plugin
{
    /// <summary>
    /// 插件基础接口，所有插件都基于该接口。
    /// </summary>
    public interface IPluginBase
    {
        /// <summary>
        /// 插件包名
        /// </summary>
        string PackageName { get; }

        /// <summary>
        /// 插件类型
        /// 站点内容提供，继承 <see cref="IPluginProvider"/> 并填写 <code>PluginType.SiteProvider</code>
        /// 后期内容处理，继承 <see cref="IPluginProcessor"/> 并填写 <code>PluginType.PostProcessor</code>
        /// </summary>
        PluginType Type { get; }

        /// <summary>
        /// 获取插件显示名称
        /// </summary>
        string DisplayName { get; }
    }

    /// <summary>
    /// 插件类型
    /// </summary>
    public enum PluginType
    {
        /// <summary>
        /// 网站插件 - 提供一个新的搜索/下载插件
        /// </summary>
        SiteProvider,

        /// <summary>
        /// 后期处理 - 例如去除广告文字、特殊格式化
        /// </summary>
        PostProcessor,

        /// <summary>
        /// 导出插件 - 将下载完成的书籍导出到不同的格式。
        /// </summary>
        Exporter
    }
}