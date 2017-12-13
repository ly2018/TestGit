using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vic.Core.Application.IServices;
using Vic.Core.Utils;
using Vic.Core.Application.Dtos;

namespace Ruiec.WebUI.Components
{
    [Authorize(AuthenticationSchemes = ConstDefine.AuthenticationScheme)]
    [ViewComponent(Name = "NavigationLeft")]
    public class NavigationLeftViewComponent : ViewComponent
    {
        private readonly IMenuAppService _menuAppService;
        private readonly IUserAppService _userAppService;
        public NavigationLeftViewComponent(IMenuAppService menuAppService, IUserAppService userAppService)
        {
            _menuAppService = menuAppService;
            _userAppService = userAppService;
        }

        public IViewComponentResult Invoke()
        {
            var userId = base.UserClaimsPrincipal.FindFirst(ClaimTypes.Sid).Value;
            var menus = _menuAppService.GetMenusByUser(new Guid(userId), false);
            return View(menus);
        }
    }
}
