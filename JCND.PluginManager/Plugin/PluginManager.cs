using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace moe.Jixun.Plugin
{
    public class PluginManager
    {
        public static PluginManager Instance { get; private set; }
        private readonly Dictionary<string, IPluginBase> _plugins;
        private readonly IDataModel _app;
        
        public PluginManager(IDataModel app)
        {
            _app = app;
            _plugins = new Dictionary<string, IPluginBase>();
            Instance = this;
        }

        public bool LoadPlugin(string path)
        {
            var entryType = typeof(IPluginEntry);
            var type = Assembly.LoadFile(path).GetTypes()
                .FirstOrDefault(p => entryType.IsAssignableFrom(p) && p.IsClass);

            if (type == null)
            {
                Debug.WriteLine($"{path} does not contain a valid Entry.");
                return false;
            }

            var entry = (IPluginEntry)Activator.CreateInstance(type);
            try
            {
                entry.Boot(this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed! {path} had an exception: {ex}");
                return false;
            }

            return true;
        }

        public void LoadPlugins(string dir)
        {
            var pluginNames = Directory.GetFiles(dir, "Plugin.*.dll");
            foreach (var pluginName in pluginNames)
            {
                Debug.Write($"Loading Plugin: {pluginName}... ");

                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if (LoadPlugin(Path.Combine(dir, pluginName)))
                {
                    Debug.WriteLine("OK!");
                }
                else
                {
                    Debug.WriteLine("Failed!");
                }
            }

            Debug.WriteLine($"{_plugins.Count} plugins loaded.");
        }

        /// <summary>
        /// 注册插件，包名不得重复
        /// </summary>
        /// <param name="plugin">插件名称</param>
        /// <returns>是否注册成功</returns>
        public bool Register(IPluginBase plugin)
        {
            var packageName = plugin.PackageName;
            if (_plugins.ContainsKey(packageName))
            {
                Debug.WriteLine("Failed to load plugin (already exist): " +
                               $"{plugin.DisplayName} ({plugin.PackageName})");
                return false;
            }

            _plugins[packageName] = plugin;
            return true;
        }

        /// <summary>
        /// 请求所有网站搜索一本书，并返回第一页的结果。
        /// </summary>
        /// <param name="name"></param>
        public async Task SearchBookAsync(string name)
        {
            // MainWindow.Instance.BtnSearch.IsEnabled = false;
            
            _app.ClearSearchResults();

            var providers = _plugins
                .Where(plugin => plugin.Value.Type == PluginType.SiteProvider)
                .Select(plugin => plugin.Value as IPluginProvider).ToArray();

            var i = 0;
            var count = providers.Length;
            
            foreach (var pluginProvider in providers)
            {
                i++;

                _app.SetStatusText($"正在搜索第 {i} 个网站 ({pluginProvider.DisplayName})，" +
                                   $"共 {count} 个网站...");
                var results = await pluginProvider.SearchBook(name);

                foreach (var result in results)
                {
                    _app.AddSearchResult(result);
                }
            }
            _app.SetStatusText("搜索完毕!");

            // MainWindow.Instance.BtnSearch.IsEnabled = true;
        }
    }
}