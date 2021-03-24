using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSMiniProgram.Controllers
{
    /// <summary>
    /// 备注:这个Controller应该要继承自 ControlerBase类而不是Controller类
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {

        private IUserService _userService;
        private IMapper _mapper;
        //private readonly AppSettings _appSettings;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
