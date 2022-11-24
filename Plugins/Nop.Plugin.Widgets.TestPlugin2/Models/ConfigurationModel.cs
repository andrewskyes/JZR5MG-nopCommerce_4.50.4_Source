using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.TestPlugin2.Models
{
    public record ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }
        
        [NopResourceDisplayName("Plugins.Widgets.TestPlugin2.WidgetDisplayName")]
        public string WidgetDisplayName { get; set; }
        public bool WidgetDisplayName_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.TestPlugin2.CategoryNumber")]
        public int CategoryNumber { get; set; }
        public bool CategoryNumber_OverrideForStore { get; set; }
    }
}