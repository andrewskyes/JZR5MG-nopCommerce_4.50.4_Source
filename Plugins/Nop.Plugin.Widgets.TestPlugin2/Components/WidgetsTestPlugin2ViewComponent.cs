using Microsoft.AspNetCore.Mvc;
using Nop.Services.Logging;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.TestPlugin2.Components
{
    [ViewComponent(Name = "WidgetsTestPlugin2")]
    internal class WidgetsTestPlugin2ViewComponent : NopViewComponent
    {
        
        private readonly TestPlugin2Settings _testPlugin2Settings;
        private readonly ILogger _logger;



        public WidgetsTestPlugin2ViewComponent(TestPlugin2Settings testPlugin2Settings, ILogger logger)
        {
            _testPlugin2Settings = testPlugin2Settings;
            _logger = logger;
        }


        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var script = "";
            var routeData = Url.ActionContext.RouteData;

            try
            {
                script = "<script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':\\r\\nnew Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],\\r\\nj=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=\\r\\n'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);\\r\\n})(window,document,'script','dataLayer','\" + _testPlugin2Settings.WidgetDisplayName + \"');</script>";
            }
            catch (Exception ex)
            {
                await _logger.InsertLogAsync(Core.Domain.Logging.LogLevel.Error, "Nem sikerült létrehozni a GTM kódot", ex.ToString());
            }
            return View("~/Plugins/Widgets.GoogleAnalytics/Views/PublicInfo.cshtml", script);
        }
    }
}
