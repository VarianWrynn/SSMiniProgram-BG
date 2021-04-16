using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            /*To be able to use the connection string anywhere in the code, 
             * it's necessary to create a singleton of its model inside the
             * Startup class.*/
            ConnectionStrings con = new ConnectionStrings();
            Configuration.Bind("ConnectionStrings", con);
            services.AddSingleton(con);

            /*services.AddScoped<ILeeTestRepository, LeeTestRepository>();
            services.AddScoped<IJournalRepository, JournalRepository>();
            services.AddScoped<IJournal_Member_LikesRepository, Journal_Member_LikesRepository>();
            services.AddScoped<IMemberInfoRepository, MemberInfoRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBook_Member_Like_Repository, Book_Member_Like_Repository>();
            services.AddScoped<IBook_CommentsRepository, Book_CommentsRepository>();*/

            /*All you have to do to use this generic repository in ASP.NET Core is to add it in 
             * Startup.ConfigureServices as a scoped service. With this, you will be able to 
             * inject a repository of a specific entity at any time.
             * https://blog.zhaytam.com/2019/03/14/generic-repository-pattern-csharp/*/
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            /*Create a service for the IUserRepository and inject the MySQL connection string 
             * (from the appsettings.json) into DBContext:*/
            services.AddDbContext<DBContext>(o => o.UseMySql(Configuration.GetConnectionString("MySQL")));


            //Service
            services.AddScoped<IJournalServices, JournalServices>();
            services.AddScoped<IBookServices, BookServices>();

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
