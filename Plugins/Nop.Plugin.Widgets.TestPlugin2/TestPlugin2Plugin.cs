using Nop.Core;
using Nop.Plugin.Widgets.TestPlugin2;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.TestPlugin1
{
    public class TestPlugin2Plugin : BasePlugin, IWidgetPlugin
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;

        #endregion

        #region Ctor

        public TestPlugin2Plugin(ILocalizationService localizationService,
            IWebHelper webHelper,
            ISettingService settingService)
        {
            _localizationService = localizationService;
            _webHelper = webHelper;
            _settingService = settingService;
        }

        #endregion

        public bool HideInWidgetList => false;

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsTestPlugin2";
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string>
            {
                PublicWidgetZones.HomepageTop
            });
        }


        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsTestPlugin2/Configure";
        }

        public override async Task InstallAsync()
        {
            var settings = new TestPlugin2Settings
            {
                WidgetDisplayName = "Saját widget"
            };
            await _settingService.SaveSettingAsync(settings);

            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Widgets.TestPlugin2.WidgetDisplayName"] = "Display name",
            });

            await base.InstallAsync();
        }

    }
}