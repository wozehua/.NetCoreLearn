using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using StudentCoreMvc.Model;
using StudentCoreMvc.Services;

namespace StudentCoreMvc
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        /// <summary>
        /// IConfiguration 在PrograM.cs 中已经注册过了可以直接用
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //使用mvc 要注册这个AddMVC();
            services.AddMvc();
            //这边自定义服务的注册有三种方式
            //1：services.AddSingleton<接口,实现类>  单例模式 意思就是在整个Web 生命周期里只有一个实例
            services.AddSingleton<IWelcomeServices, WelcomeService>();
            //services.AddScoped<IRepository<Student>, InMemoryRepository>();
            services.AddSingleton<IRepository<Student>, InMemoryRepository>();

            //一下两个都是获取ConnetionString:DefaultConnetion 下的DefaultConnetion 信息
            //var connectionName = _configuration["ConnetionString:DefaultConnetion"];
            var connectionName = _configuration.GetConnectionString("DefaultConnetion");
            services.AddDbContext<DbContext>(options => options.UseSqlServer(connectionName));

            //AddTransient 就是每次方法或者类请求 都会生成一个新的注册实例
            //services.AddTransient<IWelcomeServices, WelcomeService>();
            //AddScoped 每次HTTP请求 会生成一个注册实例 ，如果是同一个请求每个类或者实例请求的是同一个注册实例
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
        /// <param name="welcomeSevices">自己实现的中间件</param>
        /// <param name="logger">日志中间件</param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IWelcomeServices welcomeSevices, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region 注释代码
            //app.Use(next =>
            //{
            //    logger.LogInformation("app Use........");
            //    return async httpContext =>
            //    {
            //        logger.LogInformation("Async HttpContext....");
            //        if (httpContext.Request.Path.StartsWithSegments("/first"))
            //        {
            //            logger.LogInformation("first!!!!");
            //            await httpContext.Response.WriteAsync("first !!!");
            //        }
            //        else
            //        {
            //            logger.LogInformation(" next httpContext");
            //            await next(httpContext);
            //        }
            //    };
            //});
            //app.UseWelcomePage("/welcome"); 
            #endregion
            //我们在真实的项目中通常不适用这个方法
            //因为这个run 通常是一个简单的方法
            //在真实的项目中我们通常使用app.use
            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            //app.UseFileServer() 包含了UseDefaultFiles() UseStaticFiles() 这两个功能 还有其他功能
            //所以我们用UseFileServer() 就可以了 还有其他功能如 目录浏览
            //这个默认浏览首页
            //app.UseFileServer();
            //app.UseStaticFiles();
            //也可以用这种方法来指定路径 但是我们通常是使用默认的app.UseStaticFiles()下默认的wwwroot
            //todo 这边怎么引用到wwwroot
            //https://docs.microsoft.com/zh-cn/aspnet/core/client-side/using-gulp?view=aspnetcore-2.2
            app.UseStaticFiles(new StaticFileOptions()
            {
                //要应用其他路径的话 这个RequestPath 是一定要写。
                RequestPath = "/node_modules",
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
            });
            //app.UseMvcWithDefaultRoute();
            //MapRoute 可以用来判断分支走向
            app.UseMvc(routebuild => { routebuild.MapRoute("Default", "{Controller=Home}/{Action=Index}/{Id?}"); });
            app.Run(async (context) =>
            {
                //throw new Exception("erro");
                //IConfiguration configuration
                //var welcome=configuration["Welcome"];
                var welcome = welcomeSevices.GetMessage();
                await context.Response.WriteAsync(welcome);
            });
        }
    }
}
