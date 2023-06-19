using Microsoft.AspNetCore.Mvc;
using Clean.Frame.Apps.TargetBoard.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Core.Services;
using AutoMapper;
using Clean.Frame.Apps.TargetBoard.Services.Services.Dtos;
using System.Net;

namespace Clean.Frame.Apps.TargetBoard.Controllers
{
    public class TargetController : Controller
    {
        private readonly ILogger<TargetController> _logger;
        private readonly ITargetService _targetService;
        private readonly IMapper _mapper;

        public TargetController(ILogger<TargetController> logger, ITargetService targetService, IMapper mapper)
        {
            _logger = logger;
            _targetService = targetService;
            _mapper = mapper;
        }
        public IActionResult Index(int? aspectId)
        {
            if (aspectId.HasValue)
            {
                return View(aspectId.Value);
            }
            return View();
        }

        [AllowAnonymous]
        [Route("get-target-by-id")]
        public async Task<PartialViewResult> GetTargetById(int id, int? aspectId)
        {
            var data = await _targetService.GetByIdAsync(id);

            if (data is null)
            {
                data = new TargetDto();
                data.AspectId = aspectId.HasValue ? aspectId.Value : 0;
            }
            data.Messages =!string.IsNullOrEmpty(data.Message)? data.Message.Split(','):new string[0];
            return PartialView("_add_target", data);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("get_all_target_grid_data")]
        public async Task<IActionResult> GetAllAsync(int? aspectId)
        {
            var data = new List<TargetDto>();

            if (!aspectId.HasValue)
            {
                var res = await _targetService.GetAllAsync();
                var _model = _mapper.Map<List<TargetDto>>(res);
                data = _model.ToList();
            }
            else
            {
                var result = await _targetService.GetAllByAspectIdAsync(aspectId.Value);
                data = result.ToList();
            }
            data.ToList().ForEach(x => x.Message.TrimStart(',').TrimEnd(','));
            data.ForEach(x => x.Message.Where(x => !x.Equals(",")).Select(x => x));
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
        [Route("target-save")]
        public async Task<IActionResult> SaveTargetAsync(TargetDto request)
       {
          
            string message;
            ModelState.Remove(nameof(TargetDto.Id));
            ModelState.Remove(nameof(TargetDto.Messages));
            ModelState.Remove("Id");
             request.Messages=   request.Messages is null ? new string[0] : request.Messages;
            message =(request.Messages.Length>0 )? String.Join(',', request.Messages):"";
            if (ModelState.IsValid)
            {
                if (request.Id == 0)
                {
                   
                    try
                    {
                        Target model = _mapper.Map<Target>(request);
                        model.Message = message;
                        await _targetService.AddAsync(model);
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
                    Target model = _mapper.Map<Target>(request);
                    model.Message = message;
                     _targetService.Update(model);
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
        [Route("target-remove")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id > 0)
            {
                await _targetService.RemoveAsync(id);
                return StatusCode((int)HttpStatusCode.OK, new { result = "success", message = "رکورد مورد نظر با موفقیت حذف شد" });
            }

            return StatusCode((int)HttpStatusCode.BadRequest, new {result="failed", message = "اطلاعات معتبر نیست" });

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
