﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SSMiniProgram
{
    /// <summary>
    /// https://www.bilibili.com/video/BV1k7411A7p6
    /// .NET Core专题[Asp.NetCore启动流程和主机(配置)] -哔哩哔哩
    /// </summary>
    public class Program
    {
        //这里其实可以看出.NET Core就是一个应用控制台，
        public static void Main(string[] args)
        {
            /*
             - CreateHostBuilder里面只是把配置以委托的都读取存放到List里面，没有执行；
             - 真正开始执行是在Build()方法中执行；所以在我们配置完之前，都不会被执行，即：我们所有的配置都被【延后】了；
             -------------------------------------------
             -一个主机构建器只能构建一个主机，再次构建会直接Throw Exception,可以参考阅读源码；
             -  Run()就是让主机跑起来；
             - Run()是以阻塞的方式运行主机。（如果不以阻塞的方式运行，因为当前应用是个控制台应用程序，如果不阻塞，则一执行就没了）
             */
            CreateHostBuilder(args).Build().Run();
        }

        //在这里会创建主机（host),并且把.NET Core应用绑在主机里面；
        //首先是创建默认主机构建器(构建器主要目的就是配置，等配置都写好了之后 就在Main方法里面Build().Run();
        //Host类是在.NET Core的扩展包里，即: .NET Extension, 该拓展包是开源的；

        //功能性的组件都在.NET Extension里面， 而.NET Core里面都是核心的API；


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            //构建器主机里面包含了两部分，一部分是构建主机配置，另一个是构建应用配置；
            //{.NET Core是一个主机对象，包含了当前应用所需的所有资源；}
            //同时还会创建默认的服务容器（UserDefaultServiceProvider)
            Host.CreateDefaultBuilder(args)
                //.NET Core 有两种Host，一种是泛型（通用）Host，另一种是Web主机
                //Web主机是泛型主机的拓展，提供了额外Web功能比如支持HTTP，集成Kestrel,内置了IIS集成等；
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    /*
                     * - 从 前缀为"ASPNETCORE"加载到WEB配置主机
                     * - 讲Kestrel设置为Web服务器并对其进行默认配置 （/也支持IIS集成，不过与Kestrel是二选一关系）
                     * - 在这个委托里面也可以进行自定义配置
                     * - Web主机不建议用硬编码，以为不灵活，可以用配置文件/命令行；
                     * -----------------------------------------------------------------------------------------
                     * - 小结：这里都属于【组件配置】，不属于主机，但是由主机调动；这些组件配置都有拓展类提供的配置方法；
                     */

                    //这里举一个例子，对Kestrel配置进行设置，设置其请求的最大量是1024*3；（默认是28.6 M);
                    //这里配置完，如果用反向代理Ngix，则也要到Ngix上进行修改请求的Body最大值
                    //webBuilder.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = 1024 * 1024 * 1024);

                    //设置内置Log组件的日志级别；
                   // webBuilder.ConfigureLogging(l=>l.SetMinimumLevel(LogLevel.Debug));


                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:6000");//改变端口号
                });
    }

    /*什么是主机?
     - 主机负责应用的启动和生命周期的管理
     - 负责 配置服务器
     - 负责 请求处理管道
     - 默认设置日志记录
     - 依赖关系和注入的配置
     - 这个主机不是虚拟主机，简单来说就是一个封装了应用资源的【对象】，
     - 这个资源是属于.NET Core的一个类，类名就叫 Host(中文名叫 主机)

     */
}
