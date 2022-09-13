using aspnetcore_dbcontext.Data;
using aspnetcore_dbcontext.Models;
using FreeSql;
using FreeSql.Internal;

namespace aspnetcore_dbcontext
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFreeSql(this IServiceCollection services, IConfiguration c)
        {

            Func<IServiceProvider, IFreeSql> fsql = r =>
            {
                IFreeSql fsql = new FreeSqlBuilder()
                    .UseConnectionString(DataType.Sqlite,c.GetConnectionString("Sqlite"))
                    //.UseConnectionString(DataType.MySql,c.GetConnectionString("MySql"))
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

            //在项目启动时，从容器中获取IFreeSql实例，并执行一些操作：同步表，种子数据,FluentAPI等
            using IServiceScope serviceScope = services.BuildServiceProvider().CreateScope();
            var free = serviceScope.ServiceProvider.GetRequiredService<IFreeSql>();

            free.SetDbContextOptions(opt => {
                opt.OnEntityChange = report => {
                    Console.WriteLine(report);
                };
            });

            free.CodeFirst.ApplyConfigurationsFromAssembly(typeof(TodoConfiguration).Assembly);
            free.CodeFirst.SyncStructure(typeof(Todo));
            services.AddFreeDbContext<TodoDbContext>(options => options.UseFreeSql(free));

            return services;
        }
    }
}