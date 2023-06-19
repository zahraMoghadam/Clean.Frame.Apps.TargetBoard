using System.Diagnostics;
using System.Net;
using AutoMapper;
using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Core.Services;
using Clean.Frame.Apps.TargetBoard.Models;
using Clean.Frame.Apps.TargetBoard.Services.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Frame.Apps.TargetBoard.Controllers
{
    public class AspectController : Controller
    {
        private readonly ILogger<AspectController> _logger;
        private readonly IAspectService _aspectService;
        private readonly IMapper _mapper;

        public AspectController(ILogger<AspectController> logger, IAspectService aspectService, IMapper mapper)
        {
            _logger = logger;
            _aspectService = aspectService;
            _mapper = mapper;
        }
        public IActionResult Index(int? mainBoardId)
        {
            if (mainBoardId.HasValue)
            {
                return View("Index", mainBoardId.Value);
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("get-aspect-by-id")]
        public async Task<PartialViewResult> GetAspectById(int id, int? mainBoardId)
        {
            var data = await _aspectService.GetByIdAsync(id);
            var model = new AspectMainBoardDto();
            if (data is null)
            {
                data = new AspectDto();
                data.MainBoardId = mainBoardId.HasValue ? mainBoardId.Value : 0;
            }
            model = _mapper.Map<AspectMainBoardDto>(data);
            return PartialView("_add_aspect", model);
        }

        [AllowAnonymous]
        [Route("get_all_aspect_grid_data")]
        public async Task<IActionResult> GetAllAsync(int mainBoardId)
        {
            var data = new List<AspectDto>();
            if (mainBoardId is 0)
            {
                var result = await _aspectService.GetAllAsync();
                var _model = _mapper.Map<List<AspectDto>>(result);
                data = _model.ToList();
            }
            else
            {
                data = await _aspectService.GetByMainBoardIdWithoutRelationAsync(mainBoardId);
            }

            if (data is not null)
            {
                return Ok(data);
            }
            else
            {
                return NotFound(data);

            }
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("aspect-save")]
        public async Task<IActionResult> SaveAsync(AspectMainBoardDto request)
        {
            var message = "";
            ModelState.Remove(nameof(AspectDto.Id));
            ModelState.Remove(nameof(AspectDto.MainBoard));
            ModelState.Remove(nameof(AspectDto.Targets));
           // ModelState.Remove(nameof(AspectDto.Targets));

            if (ModelState.IsValid)
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    if (request.Id == 0)
                    {
                        try
                        {
                            var model = _mapper.Map<Aspect>(request);
                            await _aspectService.AddAsync(model);
                            message = " داده با موفقیت ذخیره شد ";
                            _logger.LogInformation(message);
                            return StatusCode((int)HttpStatusCode.OK, new { result = "ok", message });
                        }
                        catch
                        {
                            message = "خطا در انجام عملیات";
                            return StatusCode((int)HttpStatusCode.InternalServerError, new { result = "failed", message });
                        }
                    }
                    try
                    {
                        var model = _mapper.Map<Aspect>(request);
                        _aspectService.Update(model);
                        message = " داده با موفقیت بروز رسانی شد ";
                        _logger.LogInformation(message);
                        return StatusCode((int)HttpStatusCode.OK, new { result = "ok", message });
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
        [Route("aspect-remove")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id > 0)
            {
                var hasRelation = await _aspectService.CheckHasAspectRelationForDelete(id);
                if (hasRelation)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { result = "failed", message = "متاسفانه قادر به حذف نیستید، اطلاعات این رکورد جای دیگر استفاده می شود." });
                }

                var notfount = await _aspectService.GetByIdAsync(id);
                if (notfount is null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { result = "failed", message = "رکورد یافت نشد" });
                }
                await _aspectService.RemoveAsync(id);
                return StatusCode((int)HttpStatusCode.OK, new { result = "success", message = "رکورد مورد نظر با موفقیت حذف شد" });
            }

            return StatusCode((int)HttpStatusCode.BadRequest, new { result = "failed", message = "اطلاعات معتبر نیست" });


        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get-target-from-aspect")]
        public async Task<PartialViewResult> getTargetByAspectId(int id)
        {
            var aspectModel = _aspectService.GetByIdAsync(id);

            if (aspectModel is not null)
            {
                AspectViewModel _model = _mapper.Map<AspectViewModel>(aspectModel.Result);
                return PartialView("AddAspectTarget", _model);
            }
            else
            { return PartialView("AddAspectTarget"); }
        }
    }
}