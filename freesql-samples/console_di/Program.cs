
using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using (var host = AppStartup())
{
    App app = host.Services.GetRequiredService<App>();
    await app.RunAsync(args);
}

static IHost AppStartup()
{
    var host = Host.CreateDefaultBuilder() // Initialising the Host 
                .ConfigureServices((context, services) =>
                {
                    // Adding the DI container for configuration
                    ConfigureServices(context, services);
                    services.AddTransient<App>(); // Add transiant mean give me an instance each it is being requested
                })
                .Build(); // Build the Host

    return host;
}

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    var configuration = context.Configuration;

    Func<IServiceProvider, IFreeSql> fsql = r =>
    {
        IFreeSql fsql = new FreeSqlBuilder()
                   .UseConnectionString(FreeSql.DataType.Sqlite, configuration.GetConnectionString("Sqlite"))
                    // .UseConnectionString(FreeSql.DataType.MySql, configuration["ConnectionStrings:MySql"])
                    //.UseConnectionString(FreeSql.DataType.SqlServer, configuration["ConnectionStrings:SqlServer"])
                    .UseNameConvert(FreeSql.Internal.NameConvertType.ToUpper)
                    .UseAutoSyncStructure(true)
                    .UseNoneCommandParameter(true)
                    .UseGenerateCommandParameterWithLambda(true)
                    //.UseLazyLoading(false)
                    .UseMonitorCommand(
                        cmd => Console.WriteLine("\r\n线程" + Thread.CurrentThread.ManagedThreadId + ": " + cmd.CommandText)
                        )
                    .Build();
        fsql.Aop.ConfigEntityProperty += (s, e) =>
        {
            if (e.Property.PropertyType == typeof(decimal) || e.Property.PropertyType == typeof(decimal?))
            {
                e.ModifyResult.Precision = 18;
                e.ModifyResult.Scale = 6;
                e.ModifyResult.DbType = "decimal";
            }
        };
        return fsql;
    };
    services.AddSingleton(fsql);
    services.AddFreeRepository();
    services.AddScoped<UnitOfWorkManager>();
}