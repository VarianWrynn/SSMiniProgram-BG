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


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.Run(() => Ok(bService.getBookList())); //return Ok(jService.getJournal());
        }
    }
}