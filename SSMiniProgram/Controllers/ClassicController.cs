using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interface;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace SSMiniProgram.Controllers
{
    [Route("[controller]")]
    public class ClassicController : Controller
    {
       private readonly IJournalServices jService;
        public ClassicController(IJournalServices s)
        {
            jService = s;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.Run(() => Ok(jService.getJournal()));//return Ok(jService.getJournal());
        }


        /// <summary>
        /// 前端用这种方式获取到 https://localhost:5001/Classic/2/Previous
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [HttpGet(template: "{index}/previous", Name = "previous")]
        // public JournalDTO Get(int index)
        public async Task<IActionResult> Previous(int index)
        {

            return await Task.Run(() => Ok(jService.getJournal(index - 1)));
        }

        [HttpGet(template: "{index}/next", Name = "next")]
        // public JournalDTO Get(int index)
        public async Task<IActionResult> Next(int index)
        {

            return await Task.Run(() => Ok(jService.getJournal(index + 1)));
        }

        /// <summary>
        /// 获取点赞的数量，用于单独给翻页的时候用，避免缓存带来的数据不同步问题
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(template: "{type}/{id}/favor", Name = "favor")]
        public int favor(int type, int id)
        {

            return 0;
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