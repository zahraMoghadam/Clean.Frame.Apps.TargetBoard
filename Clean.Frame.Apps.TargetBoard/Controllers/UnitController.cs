using Microsoft.AspNetCore.Mvc;
using Clean.Frame.Apps.TargetBoard.Models;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Clean.Frame.Apps.TargetBoard.Core.Entities;
using AutoMapper;
using Clean.Frame.Apps.TargetBoard.Services.Services.Dtos;
using Clean.Frame.Apps.TargetBoard.Services.Services;

namespace Clean.Frame.Apps.TargetBoard.Controllers
{
    public class UnitController : Controller
    {
        private readonly ILogger<UnitController> _logger;
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;

        public UnitController(ILogger<UnitController> logger, IUnitService unitService, IMapper mapper)
        {
            _logger = logger;
            _unitService = unitService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> GetUnitAsync(int? id)
        {
            if (id is > 0)
            {
                var data = await _unitService.GetByIdAsync(id.Value);
                return PartialView("AddUnit", data);
            }

            return PartialView("AddUnit");
        }


        [HttpPost]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _unitService.GetAllAsync();
            return Ok(data);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("unit-save")]
        public async Task<IActionResult> SaveAsync(UnitDto request)
        {
            string message;
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                if (request.Id == 0)
                {
                    try
                    {
                        Unit model = _mapper.Map<Unit>(request);
                        await _unitService.AddAsync(model);
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
                    Unit model = _mapper.Map<Unit>(request);
                    _unitService.Update(model);
                    message = " داده با موفقیت بروز رسانی شد ";
                    _logger.LogInformation(message);
                    return Json(new { result = "success", message });
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

        [AllowAnonymous]
        [HttpDelete]
        [Route("unit-remove")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id > 0)
            {
                var hasRelation = await _unitService.CheckHasUnitRelationForDelete(id);
                if (hasRelation)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { result = "failed", message = "متاسفانه قادر به حذف نیستید، اطلاعات این رکورد جای دیگر استفاده می شود." });
                }
                await _unitService.RemoveAsync(id);
                return StatusCode((int)HttpStatusCode.OK, new { result = "success", message = "رکورد با موفقیت حذف شد" });
            }

            return StatusCode((int)HttpStatusCode.BadRequest, new { result = "failed",message = "اطلاعات معتبر نیست" });

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
