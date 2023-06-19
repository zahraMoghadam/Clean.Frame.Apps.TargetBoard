using Clean.Frame.Apps.TargetBoard.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Frame.Apps.TargetBoard.Components
{
    public class Aspects : ViewComponent
    {
        private readonly IAspectService _aspectService;
        public Aspects(IAspectService aspectService)
        {
            _aspectService = aspectService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int mainboardId)
        {
            var data = await _aspectService.GetByMainBoardIdAsync(mainboardId);
            if (data is not null)
            {
                return View("Default",data);
            }
            else
            {
                return View("Default");
            }
        }
    }
}
