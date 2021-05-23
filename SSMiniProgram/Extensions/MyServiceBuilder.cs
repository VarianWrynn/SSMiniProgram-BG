using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace SSMiniProgram.Extensions
{
    /// <summary>
    /// 服务构构建器，其作用就是提供配置，比如这里提供了UserMyController()的配置选项，
    /// 
    /// 也可以新增另一个比如UserSheController配置
    /// </summary>
    public class MyServiceBuilder
    {
        /// <summary>
        /// 服务的集合
        /// </summary>
        public IServiceCollection ServiceCollection { get; set; }

        public MyServiceBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public void UserMyController()
        {
            ServiceCollection.AddScoped<IJournalServices, JournalServices>();
            ServiceCollection.AddScoped<IBookServices, BookServices>();
        }
    }
}
