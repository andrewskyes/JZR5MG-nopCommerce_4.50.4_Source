using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject1
{
    public class Class1 : BasePlugin, IWidgetPlugin
    {
        public bool HideInWidgetList => true;

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "Saját widget";
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.AccountNavigationAfter, PublicWidgetZones.AccountNavigationBefore });
        }
    }
}