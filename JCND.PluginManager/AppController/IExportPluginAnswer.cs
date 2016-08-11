using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moe.Jixun.Plugin.AppController
{
    public interface IExportPluginAnswer
    {
        bool Remember { get; }
        IPluginExport Plugin { get; }
        bool Cancel { get; }
    }
}
