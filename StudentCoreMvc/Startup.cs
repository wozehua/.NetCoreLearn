using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StudentCoreMvc
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //这边自定义服务的注册有三种方式
            //1：services.AddSingleton<接口,实现类>  单例模式 意思就是在整个Web 生命周期里只有一个实例
            services.AddSingleton<IWelcomeServices, WelcomeService>();
            //AddTransient 就是每次方法或者类请求 都会生成一个新的注册实例
            //services.AddTransient<IWelcomeServices, WelcomeService>();
            //AddScoped 每次HTTP请求 会生成一个注册实例 ，如果是同一个请求
            //每个类或者实例请求的是同一个注册实例
            //services.AddScoped<IWelcomeServices, WelcomeService>();


        }

        /// <summary>
        /// 配置了Http请求的管道
        /// 当http请求到达后 这边的Configure 配置的信息
        /// 将决定了我们怎么响应httpD的请求。
        /// 我们这边可以在新增参数，是因为Asp.Net Core 解析这边的参数
        /// 然后依赖注入到我们的程序当中
        /// 像 IApplicationBuilder,IHostingEnvironment 等都是微软自带的这种方法 是已经注册完了
        /// 但是我们自己写的服务 IWelcomeServices 是没有注册的
        /// 所以我们要在上面的方法 ConfigureServieces 中注册服务 （这个方法就是用来注册服务的）
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="welcomeSevices"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IWelcomeServices welcomeSevices)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                //IConfiguration configuration
                //var welcome=configuration["Welcome"];
                var welcome = welcomeSevices.GetMessage();
                await context.Response.WriteAsync(welcome);
            });
        }
    }
}
