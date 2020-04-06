using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace SSMiniProgram.Controllers
{
    [Route("[controller]")]
    public class ClassicController : Controller
    {
        //public IActionResult Index()
        //{

        //}


        [HttpGet]
        public Lates Get()
        {
            return new Lates
            {
                content = "浩瀚群星，璀璨如你",
                fav_nums = 100,
                id = 1,
                image = "https://www.tmclee.com/Lee/images/img_bg_1.jpg",
                index = 7,
                like_status = 0,
                pubdate = "2020-02-22",
                title = "群星璀璨",
                type = 100
            };

        }
    }
}