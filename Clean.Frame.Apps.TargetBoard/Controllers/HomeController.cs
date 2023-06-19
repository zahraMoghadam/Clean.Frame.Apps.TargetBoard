using AutoMapper;
using Clean.Frame.Apps.TargetBoard.Core.Services;
using Clean.Frame.Apps.TargetBoard.Models;
using Clean.Frame.Apps.TargetBoard.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Globalization;

namespace Clean.Frame.Apps.TargetBoard.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMainBoardService _mainBoardService;
        private readonly ITargetService _targetService;
        private readonly IAspectService _aspectService;
        private readonly IMapper _mapper;


        public HomeController(ILogger<HomeController> logger, IMainBoardService mainBoardService, ITargetService targetService, IAspectService aspectService, IMapper mapper)
        {
            _logger = logger;
            _mainBoardService = mainBoardService;
            _targetService = targetService;
            _aspectService = aspectService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            PersianCalendar date = new PersianCalendar();
            DateTime startDate = new DateTime(2021, 1, 01);
            DateTime endDate = new DateTime(2021, 12, 20);
            var units = new List<SelectListItem>();

            var list = Enumerable.Range(0, 12)
                .Select(a => startDate.AddMonths(a))
                .OrderByDescending(a => a.Month)
               .TakeWhile(a => a <= endDate);

            var diff = list.OrderBy(x => date.GetMonth(x))
              .Select(a => new SelectListItem { Text = PersianCalendarExtension.GetMonthName(date.GetMonth(a)), Value = date.GetMonth(a).ToString() });
            var currentMonth = date.GetMonth(DateTime.Now);
            var currentYear = date.GetYear(DateTime.Now);

            var months = diff.Select(item =>
                                        new SelectListItem
                                        {
                                            Value = item.Value.ToString(),
                                            Selected = item.Value==currentMonth.ToString()?true:false,
                                            Text = item.Text.ToString()
                                        }).ToList();

            var years = Enumerable.Range(1400, 11).ToList().Select(item =>
                                        new SelectListItem
                                        {
                                            Value = item.ToString(),
                                            Selected = item == currentYear ? true : false,
                                            Text = item.ToString()
                                        }).ToList();

            var home = new HomeViewModel
            {
                Years = years,
                Months = months
            };
            return View(home);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get-all-mainboard-home")]
        public async Task<JsonResult> GetMainBoardHome(int? month, int? year)
        {
            PersianCalendar date = new PersianCalendar();
            var currentDateTime = DateTime.Now;
            var currentMonth = 0;
            var currentYear = 0;
            currentMonth = month is null ? date.GetMonth(currentDateTime) : month.Value;
            currentYear = year is null ? date.GetYear(currentDateTime) : year.Value;


            var mainboardServiceData = await _mainBoardService.GetAllByDateAsync(currentYear, currentMonth);

            if (!mainboardServiceData.Any())
            {
                return Json("[]");
            }
            var mainboardData = mainboardServiceData.FirstOrDefault();
            var data = _mapper.Map<MainBoardJsonViewModel>(mainboardData);
            foreach (var item in data.Aspects)
            {
                item.Targets.ToList().ForEach(c => c.Messages = c.Message?.Split(',').ToList());
            };

            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            data.MonthNames = PersianCalendarExtension.PersianMonthNames().ToList();
            var json = JsonConvert.SerializeObject(data, settings);

            return Json(json);
        }
    }
}