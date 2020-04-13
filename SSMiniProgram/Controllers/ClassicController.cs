using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interface;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace SSMiniProgram.Controllers
{
    [Route("[controller]")]
    public class ClassicController : Controller
    {

       
        private readonly IJournalRepository repo;
        public ClassicController(IJournalRepository r)
        {
            repo = r;
        }

        //[HttpGet]
        //public JournalDTO Get()
        //{
        //    return new JournalDTO
        //    {
        //        content = "浩瀚群星，璀璨如你",
        //        fav_nums = 100,
        //        id = 1,
        //        image = "https://www.tmclee.com/Lee/images/img_bg_1.jpg",
        //        index = 2,
        //        like_status = 0,
        //        pubdate = "2020-02-22",
        //        title = "Shining Stars",
        //        type = 100
        //    };

        //}

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.Run(() =>
            {
                var result = repo.List(w =>
                            (w.id == 1)).FirstOrDefault();//如果没有First()前端收到的JSON是数组形式[]
                return Ok(result);

            });
        }


        /// <summary>
        /// 前端用这种方式获取到 https://localhost:5001/Classic/2/Previous
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [HttpGet(template:"{index}/previous",Name = "GetByindex")]
        public JournalDTO GetByindex(int index)
        {

            return new JournalDTO
            {
                content = "相约头马，群星璀璨",
                fav_nums = 118,
                id = index,
                image = "https://www.tmclee.com/Lee/images/3.jpg",
                index = 3,
                like_status = 0,
                pubdate = "2020-02-22",
                title = "You are my Shining Star",
                type = 100
            };
        }

        /// <summary>
        /// https://localhost:5001/Classic/GetById2?id=2 这种方式可以取到
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById2")]
        public JournalDTO GetById2(int id)
        {

            return new JournalDTO
            {
                content = "Leeeeeeeeee",
                fav_nums = 118,
                id = id,
                image = "https://www.tmclee.com/Lee/images/3.jpg",
                index = 3,
                like_status = 0,
                pubdate = "2020-02-22",
                title = "Shining Stars",
                type = 100
            };
        }
    }
}