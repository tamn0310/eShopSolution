using eShopSolution.Api.Application;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSplution.AdminWeb.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}