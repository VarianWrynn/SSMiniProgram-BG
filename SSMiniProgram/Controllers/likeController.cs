using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.POCOs;
using Services;

namespace SSMiniProgram.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class likeController : ControllerBase
    {
        private readonly IJournalServices jService;

        public likeController(IJournalServices s)
        {
            jService = s;
        }



        //[HttpPost(template: "like", Name = "Like")] Like大小写敏感，坑爹呢 2020-4-17
        //[HttpPost(template: "like")]
        [HttpPost]
        // public async Task<IActionResult> doLike([FromBody]int like_id) //用[FromHeader]可以进的来，但是值为0
        public async Task<IActionResult> doLike([FromBody] Journal_Member_Likes model)
        {
            // 因为 like 请求没有设置回调函数，所以这里只能用member_id + journal_id来请求更新与新增 2020-4-19 18:36:34
            return await Task.Run(() =>
            {
                model.Member_Id = 1;
                
                jService.Add(model);
                return Ok();
            });
        }


        [HttpPost(template: "cancel")]
        public async Task<IActionResult> cancelLike([FromBody]Journal_Member_Likes model)
        {

            return await Task.Run(() =>
            {
                jService.Remove(model);
                return Ok();
            });
        }
    }
}