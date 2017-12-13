using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vic.Core.Application.IServices;
using Vic.Core.Application.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.WebApiCompatShim;
using System.Web.Http;
using Vic.Core.Application.Dtos;

namespace Ruiec.WebApi.Controllers
{
    //[Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static ILogger<ValuesController> _log;
        private readonly IPlateSettingAppService _PlateSettingAppService;
        public ValuesController(ILogger<ValuesController> log, IPlateSettingAppService PlateSettingAppService)
        {
            _PlateSettingAppService = PlateSettingAppService;
            _log = log;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _log.LogError("日志测试2017");
            _PlateSettingAppService.Create(new PlateSettingDto()
            {
                CreateTime = DateTime.Now,
                CreatorID = Vic.Core.Utils.ConstDefine.SuperAdminID,
                Key = "API接口",
                Value = "API接口",
                Tag = "API接口",
                LastUpdateTime = DateTime.Now,
                LastUpdateUserID = Vic.Core.Utils.ConstDefine.SuperAdminID,
            });
            var list = _PlateSettingAppService.ListByReadOnlyDB();
            return list.Select(a => a.Key).AsEnumerable();
        }

        [HttpPost]
        public IActionResult Test([FromBody]PlateSettingDto model)
        {
            _PlateSettingAppService.Create(model);
            return Ok("");
        }
    }
}
