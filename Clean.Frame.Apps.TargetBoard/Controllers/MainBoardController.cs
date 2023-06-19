using Microsoft.AspNetCore.Mvc;
using Clean.Frame.Apps.TargetBoard.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using Clean.Frame.Apps.TargetBoard.Services.Services.Units;
using Clean.Frame.Apps.TargetBoard.Utility;
using Clean.Frame.Apps.TargetBoard.Services.Services;
using Clean.Frame.Apps.TargetBoard.Services.Services.Dtos;
using System.Net;

namespace Clean.Frame.Apps.TargetBoard.Controllers
{
    public class MainBoardController : Controller
    {
        private readonly ILogger<MainBoardController> _logger;
        private readonly IMainBoardService _mainBoardService;
        private readonly IMapper _mapper;
        private readonly IUnitService _unitService;
        private readonly IAspectService _aspectService;

        public MainBoardController(ILogger<MainBoardController> logger, IMainBoardService mainBoardService, IMapper mapper, IUnitService unitService, IAspectService aspectService)
        {
            _logger = logger;
            _mainBoardService = mainBoardService;
            _mapper = mapper;
            _unitService = unitService;
            _aspectService = aspectService;
        }
        public IActionResult Index()
        {

            return View();
        }

        [Route("get-mainboard-by-id")]
        [HttpPost]
        public async Task<PartialViewResult> GetByIdAsync(int id)
        {
            MainBoardDto data = new MainBoardDto();
            data = await _mainBoardService.GetByIdAsync(id);
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


            var months = diff.Select(item =>
                                        new SelectListItem
                                        {
                                            Value = item.Value.ToString(),
                                            Text = item.Text.ToString()
                                        }).ToList();

            var years = Enumerable.Range(1400, 11).ToList().Select(item =>
                                        new SelectListItem
                                        {
                                            Value = item.ToString(),
                                            Text = item.ToString()
                                        }).ToList();

            var unitData = await _unitService.GetAllAsync();
            if (unitData is not null)
            {
                units = unitData.ToList().Select(item =>
                                        new SelectListItem
                                        {
                                            Value = item.Id.ToString(),
                                            Text = item.Name.ToString()
                                        }).ToList();
            }

            if (data is null)
            {
                data = new MainBoardDto
                {
                    Years = years,
                    Months = months,
                    Units = units
                };
            }
            data.Years = years;
            data.Months = months;
            data.Units = units;

            return PartialView("_add_MainBoard", data);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("get-aspect-by-main-board-id")]
        public async Task<PartialViewResult> getAspectByMainBordId(int id)
        {

            if (id is not 0)
            {
                return PartialView("_add_aspect_in_mainBoard", id);
            }
            else
            { return PartialView("_add_aspect_in_mainBoard"); }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("get-all-mainboard")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _mainBoardService.GetAllByUnitAsync();

            var newModel = data.Select(x => new MainBoardDto
            {
                MonthName = PersianCalendarExtension.GetMonthName(x.Month),
                UnitName = x.UnitName,
                UnitId = x.UnitId,
                Year = x.Year,
                Id = x.Id
            }).ToList();
            if (newModel is not null)
            {
                return Ok(newModel);
            }
            else
            {
                return NotFound(newModel);

            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("mainBoard-save")]
        public async Task<IActionResult> SaveAsync(MainBoardDto request)
        {
            var message = "";
            ModelState.Remove(nameof(MainBoardDto.Id));
            //ModelState.Remove(nameof(MainBoardDto.Unit));
            ModelState.Remove(nameof(MainBoardDto.Months));
            ModelState.Remove(nameof(MainBoardDto.Aspects));
            ModelState.Remove(nameof(MainBoardDto.UnitName));
            ModelState.Remove(nameof(MainBoardDto.Years));
            ModelState.Remove(nameof(MainBoardDto.MonthName));
            ModelState.Remove(nameof(MainBoardDto.MonthNames));
            ModelState.Remove(nameof(MainBoardDto.Units));
            if (ModelState.IsValid)
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    if (request.Id == 0)
                    {
                        try
                        {
                            var model = _mapper.Map<MainBoard>(request);
                            await _mainBoardService.AddAsync(model);
                            message = " داده با موفقیت ذخیره شد ";
                            _logger.LogInformation(message);
                            return Json(new { result = "success", message });
                        }
                        catch
                        {
                            message = "خطا در انجام عملیات";
                            return StatusCode((int)HttpStatusCode.InternalServerError, new { result = "failed", message });
                        }
                    }
                    try
                    {
                        var model = _mapper.Map<MainBoard>(request);
                        _mainBoardService.Update(model);
                        message = " داده با موفقیت بروز رسانی شد ";
                        _logger.LogInformation(message);
                        return StatusCode((int)HttpStatusCode.OK, new { result = "success", message = "داده با موفقیت بروزرسانی شد" });
                    }
                    catch
                    {
                        message = "خطا در انجام عملیات";
                        return StatusCode((int)HttpStatusCode.InternalServerError, new { result = "failed", message });
                    }
                }

                message = "لطفا داده ها را بررسی نمایید.";
                _logger.LogInformation(message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { result = "failed", message });
            }
            message = "لطفا داده ها را بررسی نمایید.";
            _logger.LogInformation(message);
            return StatusCode((int)HttpStatusCode.InternalServerError, new { result = "failed", message });
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("mainBoard-remove")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id > 0)
            {
                var hasRelation = await _mainBoardService.CheckHasMainBoardAspectsForDelete(id);
                if (hasRelation)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { result = "failed", message = "متاسفانه قادر به حذف نیستید، اطلاعات این رکورد جای دیگر استفاده می شود." });
                }

                var notfount = await _mainBoardService.GetByIdAsync(id);
                if (notfount is null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { result = "failed", message = "رکورد یافت نشد" });
                }
                await _mainBoardService.RemoveAsync(id);
                return StatusCode((int)HttpStatusCode.OK, new { result = "success", message = "رکورد مورد نظر با موفقیت حذف شد" });
            }

            return StatusCode((int)HttpStatusCode.BadRequest, new { result = "failed", message = "اطلاعات معتبر نیست" });


        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("get_aspect_by_mainboard")]
        public async Task<PartialViewResult> LoadAspect(int? mainBoardId)
        {
            if (mainBoardId.HasValue)
            {
                return PartialView("_aspect", mainBoardId.Value);
            }
            else
            {
                return PartialView("_aspect");
            }
        }
        [Route("get_target_by_aspect")]
        public async Task<PartialViewResult> LoadTarget(int? aspectId)
        {
            if (aspectId.HasValue)
            {
                return PartialView("_target", aspectId.Value);
            }
            return PartialView("_target");
        }
    }
}
