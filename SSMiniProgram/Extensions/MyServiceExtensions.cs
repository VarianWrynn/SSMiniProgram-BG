using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interface;
using DAL.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace SSMiniProgram.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="options">泛型委托，委托类型是自定义的构建器类</param>
        public static void AddMyService(this IServiceCollection serviceCollection, Action<MyServiceBuilder> options)
        {
           //创建构建器

           var builder = new MyServiceBuilder(serviceCollection);

            //调用委托，传递对象进来

            //options是泛型委托，其需要传入一个强类型MyServiceBuilder的实例,无返回类型（void)
            options(builder);
        }
    }
}
