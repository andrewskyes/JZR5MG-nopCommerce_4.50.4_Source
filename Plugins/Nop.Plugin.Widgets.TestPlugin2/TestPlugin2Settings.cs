using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.TestPlugin2
{
    public class TestPlugin2Settings : ISettings
    {
        public string WidgetDisplayName { get; set; }
        public int CategoryNumber { get; set; }
    }
}