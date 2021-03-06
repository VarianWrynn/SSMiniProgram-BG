﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CommonLib;

//在System.Web.Http空间下也存在一个[Route("[controller]")]属性
//https://jasonwatmore.com/post/2019/10/14/aspnet-core-3-simple-api-for-authentication-registration-and-user-management
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace SSMiniProgram.Controllers
{
    //[Authorize] No authenticationScheme was specified, and there was no DefaultChallengeScheme found
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 那段时光已悄然离开，而我的心亦不复存在
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            LoggerManager.Info("This is a test");

            var rng = new Random();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),

                TemperatureC = rng.Next(-20, 55),

                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }



        /*以下这种形式会报错：AmbiguousMatchException: The request matched multiple endpoints. 
         * Matches: SSMiniProgram.Controllers.WeatherForecastController.Get (SSMiniProgram)*/
        //[HttpGet(Name = "GetQrCode")]
        [HttpGet(template: "GetQrCode")] //url+ WeatherForecast/GetQrCode
        public FileResult GetQrCode(string ftpPath)

        {
            FtpWebRequest reqFTP;
            try
            {
                // 根据uri创建FtpWebRequest对象   
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpPath));

                // 指定执行什么命令  
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;

                // 指定数据传输类型  
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = false;

                // ftp用户名和密码  
                //reqFTP.Credentials = new NetworkCredential();
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                // 把下载的文件写入流
                Stream ftpStream = response.GetResponseStream();

                // 缓冲大小设置为2kb  
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                MemoryStream mStream = new MemoryStream();
                //每次读文件流的2kb
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    //把内容从文件流写入   
                    //outputStream.Write(buffer, 0, readCount);
                    mStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                //关闭两个流和ftp连接
                ftpStream.Close();
                mStream.Close();
                response.Close();
                return File(mStream.ToArray(), "image/jpg");
            }
            catch (Exception ex)
            {
                byte[] file = new byte[0];
                return File(file, "image/jpg");
            }

        }
    }
}
