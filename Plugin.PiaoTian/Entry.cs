using moe.Jixun.Plugin;

namespace moe.jixun.Plugin.PiaoTian
{
    public class Entry: IPluginEntry
    {
        public void Boot(PluginManager pm)
        {
            pm.Register(new PiaoTian());
        }
    }
}
