using FreeSql;
using FreeSql.Internal;
using IGeekFan.FreeKit.Extras.FreeSql;

namespace aspnetcore_repository
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFreeSql(this IServiceCollection services, IConfiguration c)
        {
            services.Configure<UnitOfWorkDefaultOptions>(r =>
            {
                r.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;
                r.Propagation = Propagation.Required;
            });

            Func<IServiceProvider, IFreeSql> fsql = r =>
            {
                IFreeSql fsql = new FreeSqlBuilder()
                    .UseConnectionString(c)
#if DEBUG
                    .UseAutoSyncStructure(true)
#endif
                    .UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
                    .UseGenerateCommandParameterWithLambda(true)//默认false,针对 lambda 表达式解析,设置成true时方便查看SQL
                    .UseNoneCommandParameter(true) //默认true,针对insert/update/delete是否参数化
                    .UseMonitorCommand(
                        cmd => Console.WriteLine("\r\n线程" + Thread.CurrentThread.ManagedThreadId + ": " + cmd.CommandText)
                    ).Build();
                return fsql;
            };

            services.AddSingleton(fsql);

            services.AddFreeKitCore();

            return services;
        }
    }
}