using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.TestPlugin2.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.TestPlugin2.Controllers
{
    [Area(AreaNames.Admin)]
    [AuthorizeAdmin]
    [AutoValidateAntiforgeryToken]
    public class WidgetsTestPlugin2Controller : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        #endregion

        #region Ctor

        public WidgetsTestPlugin2Controller(
            ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        #endregion

        #region Methods
        
        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var testPlugin2Settings = await _settingService.LoadSettingAsync<TestPlugin2Settings>(storeScope);

            var model = new ConfigurationModel
            {
                WidgetDisplayName = testPlugin2Settings.WidgetDisplayName,
                CategoryNumber = testPlugin2Settings.CategoryNumber,
                ActiveStoreScopeConfiguration = storeScope
            };

            if (storeScope > 0)
            {
                model.WidgetDisplayName_OverrideForStore = await _settingService.SettingExistsAsync(testPlugin2Settings, x => x.WidgetDisplayName, storeScope);
                model.CategoryNumber_OverrideForStore = await _settingService.SettingExistsAsync(testPlugin2Settings, x => x.CategoryNumber, storeScope);
            }

            return View("~/Plugins/Widgets.TestPlugin2/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var testPlugin2Settings = await _settingService.LoadSettingAsync<TestPlugin2Settings>(storeScope);

            testPlugin2Settings.WidgetDisplayName = model.WidgetDisplayName;
            testPlugin2Settings.CategoryNumber = model.CategoryNumber;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            await _settingService.SaveSettingOverridablePerStoreAsync(testPlugin2Settings, x => x.WidgetDisplayName, model.WidgetDisplayName_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testPlugin2Settings, x => x.CategoryNumber, model.CategoryNumber_OverrideForStore, storeScope, false);

            //now clear settings cache
            await _settingService.ClearCacheAsync();

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

            return await Configure();
        }

        #endregion
    }
}