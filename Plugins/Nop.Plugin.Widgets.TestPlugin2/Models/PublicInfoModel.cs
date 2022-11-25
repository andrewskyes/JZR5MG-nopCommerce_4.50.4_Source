using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.TestPlugin2.Models
{
    public record PublicInfoModel : BaseNopModel
    {
        public string HeaderText { get; set; }
        public string CountText { get; set; }
        public string CategoryText { get; set; }
    }
}