using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.POCOs;

namespace SSMiniProgram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeetestController : ControllerBase
    {
        private readonly ConnectionStrings con; 
        private readonly ILeeTestRepository repo;
        public LeetestController(ConnectionStrings c, ILeeTestRepository r)
        {
            con = c;
            repo = r;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] LeeTest vm)
        {
            return await Task.Run(() =>
            {
                var result = repo.List(w =>
                            (vm.Id == 0 || w.Id == vm.Id));//Id不传入这默认为0，触发条件永远为真，则返回所有数据
                            //&&
                            //(vm.Name == null || w.Name.ToUpper().Equals(vm.Name.ToUpper())));
                return Ok(result);
                //using (var c = new MySqlConnection(con.MySQL))
                //{
                //    var sql = @"SELECT * FROM user
                //                WHERE (@id = 0 OR id = @id)
                //                AND (@name IS NULL OR UPPER(name) = UPPER(@name))";
                //    var query = c.Query<User>(sql, vm, commandTimeout: 30);
                //    return Ok(query);
                //}
            });
        }
    }
}