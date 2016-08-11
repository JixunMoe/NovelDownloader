using moe.Jixun.Plugin;

namespace moe.jixun.Plugin.TxtExport
{
    public class Entry: IPluginEntry
    {
        public void Boot(PluginManager pm)
        {
            pm.Register(new TxtExport());
        }
    }
}
