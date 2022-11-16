using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.TestPlugin1
{
    public class Class1 : BasePlugin, IWidgetPlugin
    {
        public bool HideInWidgetList => false;

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "Saját Plugin";
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string>
            {
                PublicWidgetZones.AddressBottom,
                PublicWidgetZones.OrderSummaryBillingAddress,
                PublicWidgetZones.OrderSummaryShippingAddress,
                PublicWidgetZones.OrderDetailsBillingAddress,
                PublicWidgetZones.OrderDetailsShippingAddress,

                AdminWidgetZones.OrderBillingAddressDetailsBottom,
                AdminWidgetZones.OrderShippingAddressDetailsBottom
            });
        }
    }
}