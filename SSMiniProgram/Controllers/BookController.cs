using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace SSMiniProgram.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookServices bService;
        public BookController(IBookServices s)
        {
            bService = s;
        }


        
        [HttpGet(template: "hot_list")]
        public async Task<IActionResult> Get()
        {
            return await Task.Run(() => Ok(bService.GetBookList())); //return Ok(jService.getJournal());
        }




        /// <summary>
        /// 获取书籍的详细内容
        /// 前端用这种方式获取到 https://localhost:5001/Book/2/detail
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        [HttpGet(template: "{bid}/detail", Name = "GetDetail")]
        public async Task<IActionResult> GetDetail(int bid)
        {
            //return await Task.Run(() => Ok(bService.GetBookList())); //return Ok(jService.getJournal());
            return await Task.Run(()=>Ok(bService.GetBook(bid)));
        }

        [HttpGet(template: "favor/count",Name = "getMyBookCount")]
        public async Task<IActionResult> GetMyBookCount()
        {
            return await Task.Run(()=>Ok(100));
        }

       [HttpGet(template: "{bid}/short_comment",Name = "GetComments")]
        public async Task<IActionResult> GetComments(int bid)
        {
           // return await Task.Run(() => Ok("Leeeeeeeeeeeeeeeeeee"));
           return await Task.Run(()=>Ok(bService.GetComments(bid)));
        }

        [HttpGet(template:"{bid}/favor",Name = "GetLikeStatus")]
        public async Task<IActionResult> GetLikeStatus()
        {
            return await Task.Run(() => Ok(true));
        }
    }
}