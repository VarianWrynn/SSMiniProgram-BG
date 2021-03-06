﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
/*
 * 我们主动获取服务（New Class)叫 主动，正转；
 * IOC这是被动获取服务，所以叫【反转】反转的是服务的控制权；控制权交给了IOC，不再由你自己主动、任意、随时的创建了。
 * IOC在主机构建器里面就已经产生了。
 */
using Microsoft.Extensions.DependencyInjection;//.NET Core的依赖注入框架，我们的IOC实现就在这里面
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using System.Reflection;
using System.IO;
using DAL.Interface;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Model;
using Services;
using SSMiniProgram.Extensions;

#if DEBUG
#else
[assembly:ApiController]
#endif
//The above setup will automatically propagate the [ApiController] feature to 
//all controllers discovered in this assembly.
// https://www.strathweb.com/2019/01/enabling-apicontroller-globally-in-asp-net-core-2-2/
// https://www.cnblogs.com/aqgy12138/p/13419027.html

namespace SSMiniProgram
{
    /// <summary>
    /// Startup类以及包含的两个方法是约定的，.NET Core启动的时候会检查，其中Config方法是必须的，ConfigureServices是可选的；
    /// 
    /// Startup有个接口是IStartup,但是在多环境下要基于约定而不是基于接口2021-4-22
    /// </summary>
    public class Startup
    {
        //这个是 ASP.NET Core Web应用启动类

        public IConfigurationRoot Configuration { get; }
        //public Startup(IConfiguration configuration)
        public Startup(Microsoft.Extensions.Hosting.IHostEnvironment env)
        {
            var appSettings = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

            Configuration = appSettings;
        }

       // public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            //该方法是 配置服务的,可选的，主要作用是【以依赖注入的方式将服务(服务其实就是一个Class)添加到服务容器（IOC)】(IOC是.NET Core的核心概念)...
            // 正式因为我们用到了依赖注入的手段，所以才参数控制反转这个结果。可以把IOC容器看做是工厂模式的一种升华
            /*IOC容器的作用：
             - 注册类型(把服务的一些类，注册到容器里面去，有点 登记簿 的概念）
             - 解析实例:当我们把某个类（比如A class)注册到容器之后，如何获得这个A的instance呢，传统上是new A();但是在这里是【直接通过容器】来请求这个instance. 
             - 整个应用都在主机里，整个应用都可以用到这个容器，获取已注册的任何类型的实例

             - IOC容器本身也是一个对象，只是这个对象里面存储的都是 已注册类型；
             - DI框架是属于.NET 拓展类型，功能不多比较简单，普通的需求已经够用了
             - 更多的一些注册类的功能与生命周期，需要第三方的IOC容器。（你需要依赖管理的时候，就可以用IOC）
             - ASP.NET Core依赖注入是最基础，都得依赖注入进来，我们才可以使用；
             - IOC也可以实现单利模式，管理单利对象。
             - 主机创建完成之后，容器里就已经为我们默认注册了一些服务（比如主机环境变量，整个应用配置，以及自定义注册服务）

             */



            /*服务的什么周期：
            请求一个instance是尤其生存周期的，包含三种：
            1.瞬时：A类控制器依赖B类服务（瞬时的），每一次请求都是一个新的instance;
            2.作用域:每一次有客户请求时，类只会实例化一次，在这个请求中，后面的操作都会应用这个B类的Instance;
            3.单例：整个应用什么周期，只要是向服务容器请求instance,第一次才会创建，后面都是直接使用该instance.*/

            /*To be able to use the connection string anywhere in the code, 
             * it's necessary to create a singleton of its model inside the
             * Startup class.*/
            ConnectionStrings con = new ConnectionStrings();
            Configuration.Bind("ConnectionStrings", con);
            services.AddSingleton(con);


            /*
             *  - 如果我有100多个服务，难道我要写100次的服务注册吗？
                - 如果是内置的确实需要些100次；
                - 如果是第三方的IOC，可以批量注册，【通过反射】，我们想怎么注册就这么注册 
                - 属性注入：内置的不行，第三方的可以
                - 2021-5-3 22:59:26
                 
                - - 如果你写了一个第三方组件，该组件需要依赖很多其他的服务，难道你也要让开发人员自己来注入这些依赖且选择生命周期吗?不，你需要创建一个服务拓展方法，【这个是约定】。
             */
            services.AddScoped<ILeeTestRepository, LeeTestRepository>();
            services.AddScoped<IJournalRepository, JournalRepository>();
            services.AddScoped<IJournal_Member_LikesRepository, Journal_Member_LikesRepository>();
            services.AddScoped<IMemberInfoRepository, MemberInfoRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBook_Member_Like_Repository, Book_Member_Like_Repository>();
            services.AddScoped<IBook_CommentsRepository, Book_CommentsRepository>();

            /*All you have to do to use this generic repository in ASP.NET Core is to add it in 
             * Startup.ConfigureServices as a scoped service. With this, you will be able to 
             * inject a repository of a specific entity at any time.
             * https://blog.zhaytam.com/2019/03/14/generic-repository-pattern-csharp/*/
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IBook_DetailRepository, Book_DetailRepository>();

            /*Create a service for the IUserRepository and inject the MySQL connection string 
             * (from the appsettings.json) into DBContext:*/
            services.AddDbContext<DBContext>(o => o.UseMySql(Configuration.GetConnectionString("MySQL")));


            //Service
            //services.AddScoped<IJournalServices, JournalServices>();
            //services.AddScoped<IBookServices, BookServices>();
            /*按约定封装 两个服务注册方法，同时提供了一个配置委托*/
            services.AddMyService(options=>options.UserMyController());

            //添加对控制器和API相关功能的支持，但是不支持View和页面（即：Web API的模板默认使用的内置控件）
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lee's API", Version = "v1" });
            //});

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Lee Wang",
                        Email = "yuna1111@163.com",
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //配置管道的。如果你想在管道里面使用某个中间件，那么首先需要把该服务先注入进来
        //- 管道是ASP.NET Core Web应用的核心，我们开发Web应用，其实就是在写管道控件(Controller也属于管道控件的一种）
        //- 路由、认证、会话、缓存等等都是通过管道来实现的
        /*
         * 实际上ASP.NET Core应用一般都是使用某个框架来开发，比如MVC、WebAPI等；这些框架都是建立在某个特殊的中间件之上。
         * 因此我们可以通过编写中间件来拓展请求管道，在ASP.NET Core上创建我们自己的Web框架。
         */
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lee~ API V1");
                //c.RoutePrefix = string.Empty; To serve the Swagger UI at the app's root
                //(http://localhost:<port>/), set the RoutePrefix property to an empty string:
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
