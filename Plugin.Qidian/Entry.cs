using moe.Jixun.Plugin;

namespace moe.jixun.Plugin.Qidian
{
    public class Entry: IPluginEntry
    {
        public void Boot(PluginManager pm)
        {
            pm.Register(new QiDian());
        }
    }
}
