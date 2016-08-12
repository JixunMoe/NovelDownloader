using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using moe.Jixun.Plugin;
using Microsoft.Win32;

namespace moe.jixun.Plugin.TxtExport
{
    class TxtExport: IPluginExport
    {
        private static readonly SaveFileDialog SaveDialog;

        static TxtExport()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            SaveDialog = new SaveFileDialog();


            SaveDialog.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
            SaveDialog.OverwritePrompt = true;
            SaveDialog.CheckPathExists = true;
            SaveDialog.DefaultExt = "*.txt";
        }



        public string PackageName => "Plugin.TxtExport";
        public PluginType Type => PluginType.Exporter;
        public string DisplayName => "Txt 导出工具";
        private const string Seperator = "---------------------------------";

        public async Task DownloadChapters(List<IBookChapter> chapters)
        {
            // 没有待下载的章节
            if (chapters.Count == 0) return;

            // 防止冲突；选择下载路径
            string filename;
            lock (SaveDialog)
            {
                if (SaveDialog.ShowDialog() == false)
                    return;

                filename = SaveDialog.FileName;
            }

            using (var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                var book = chapters[0].Book;
                await writer.WriteAsync($"《{book.Name}》\n" +
                             $"作者：{book.Author}\n" +
                             $"来源：{book.Plugin.DisplayName}\n" +
                             $"{Seperator}");

                foreach (var chapter in chapters)
                {
                    await writer.WriteAsync($"\n\n章节: {chapter.Name}\n\n");
                    var chapterStr = await chapter.DownloadChapter();
                    await writer.WriteAsync(chapterStr);
                }

                await writer.WriteAsync($"\n\n{Seperator}");
            }
        }
    }
}
