using System;
using System.Composition;
using System.Linq;
using System.Reflection;
using Kaiheila.OneBot.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Kaiheila.OneBot
{
    /// <summary>
    /// 应用程序的入口点。
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// 应用程序的入口点。
        /// </summary>
        /// <param name="args">应用程序初始化命令参数。</param>
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            _ = host.Services.GetService<LifecycleHost>();
            host.Run();
        }

        /// <summary>
        /// 创建泛型主机构建器。
        /// </summary>
        /// <param name="args">应用程序初始化命令参数。</param>
        /// <returns>泛型主机构建器。</returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // Register Services
                    foreach (Type type in AppDomain.CurrentDomain.GetAssemblies()
                        .Concat(Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                            .Select(Assembly.Load))
                        .SelectMany(x =>
                            x.GetTypes()
                                .Where(type =>
                                    Attribute.GetCustomAttribute(type, typeof(ExportAttribute)) is not null)))
                        services.AddSingleton(type);

                    // Register Database Service
                    //services.AddDbContextPool<CqDatabaseContext>(CreateCqDatabaseContextPool, 64);
                })
                .ConfigureLogging(builder => builder
                    .AddDebug()
                    .AddSimpleConsole(options =>
                    {
                        options.ColorBehavior = LoggerColorBehavior.Enabled;
                        options.SingleLine = true;
                        options.IncludeScopes = true;
                    })
                    .AddSystemdConsole());

        //private static void CreateCqDatabaseContextPool(DbContextOptionsBuilder options) =>
        //    options.UseSqlite(CqDatabaseContext.ConnectionString);
    }
}
