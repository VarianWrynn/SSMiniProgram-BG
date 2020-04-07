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
        public Lates Get(int id)
        {
            if (id > 0)
            {
                return new Lates
                {
                    content = "You are my Shining Star",
                    fav_nums = 118,
                    id = 2,
                    image = "https://www.tmclee.com/Lee/images/3.jpg",
                    index = 3,
                    like_status = 0,
                    pubdate = "2020-02-22",
                    title = "Shining Stars",
                    type = 100
                };

            }

            return new Lates
            {
                content = "浩瀚群星，璀璨如你",
                fav_nums = 100,
                id = 1,
                image = "https://www.tmclee.com/Lee/images/img_bg_1.jpg",
                index = 2,
                like_status = 0,
                pubdate = "2020-02-22",
                title = "Shining Stars",
                type = 100
            };

        }
    }
}