using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Catalog;
using Nop.Services.Logging;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.TestPlugin2.Components
{
    [ViewComponentAttribute(Name = "WidgetsTestPlugin2")]
    public class WidgetsTestPlugin2ViewComponent : NopViewComponent
    {
        
        private readonly TestPlugin2Settings _testPlugin2Settings;
        private readonly ILogger _logger;
        private readonly IProductService _productService;
        private readonly IList<int> _categoryID;
        private readonly ICategoryService _categoryService;



        public WidgetsTestPlugin2ViewComponent(TestPlugin2Settings testPlugin2Settings, ILogger logger, IProductService productService, IList<int> categoryID, ICategoryService categoryService)
        {
            _testPlugin2Settings = testPlugin2Settings;
            _logger = logger;
            _productService = productService;
            _categoryID = categoryID;
            _categoryService = categoryService;
        }


        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var html = "";
            _categoryID.Add(_testPlugin2Settings.CategoryNumber);
            var nbrOfProducts = await _productService.GetNumberOfProductsInCategoryAsync(_categoryID);
            var selectedCategory = await _categoryService.GetCategoryByIdAsync(_testPlugin2Settings.CategoryNumber);
            string categoryName;
            if (selectedCategory != null)
            {
                categoryName = selectedCategory.Name;
            } else
            {
                categoryName = "Selected category is NOT valid in widget settings.";
            }

            try
            {
                html = $"<h1>{_testPlugin2Settings.WidgetDisplayName}</h1><br><h2>{categoryName}({nbrOfProducts})</h2>";
            }
            catch (Exception ex)
            {
                await _logger.InsertLogAsync(Core.Domain.Logging.LogLevel.Error, "Could NOT load category highlight widget (TestPlugin2)...", ex.ToString());
            }
            return View("~/Plugins/Widgets.TestPlugin2/Views/PublicInfo.cshtml", html);
        }
    }
}
