using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vic.Core.Application.IServices;
using Vic.Core.Application.Dtos;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Vic.Core.Utils;

namespace Ruiec.WebUI.Components
{
    [Authorize(AuthenticationSchemes = ConstDefine.AuthenticationScheme)]
    [ViewComponent(Name = "NavigationTop")]
    public class NavigationTopViewComponent : ViewComponent
    {
        private readonly IMenuAppService _menuAppService;
        private readonly IUserAppService _userAppService;
        public NavigationTopViewComponent(IMenuAppService menuAppService, IUserAppService userAppService)
        {
            _menuAppService = menuAppService;
            _userAppService = userAppService;
        }

        public IViewComponentResult Invoke()
        {
            var userId = base.UserClaimsPrincipal.FindFirst(ClaimTypes.Sid).Value;
            Guid uid = new Guid(userId);
            var user = _userAppService.Get(uid);
            ViewBag.AdminName = user.UserName;
            var menus = _menuAppService.GetMenusByUser(uid, true);
            return View(menus);
        }
    }
}
