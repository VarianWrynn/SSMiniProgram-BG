using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SSMiniProgram.Controllers
{
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
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

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
