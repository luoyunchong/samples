using System.Diagnostics;
using aspnetcore_identity_freesql.Models;
using FreeSql;
using FreeSql.Internal;
using IGeekFan.AspNetCore.Identity.FreeSql;
using Microsoft.AspNetCore.Identity;

namespace aspnetcore_identity_freesql
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFreeSqlIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            Func<IServiceProvider, IFreeSql> fsql = r =>
            {
                IFreeSql fsql = new FreeSqlBuilder()
                        .UseConnectionString(DataType.MySql, configuration["ConnectionStrings:MySql"])
                        //.UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
                        .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
                        .UseMappingPriority(MappingPriorityType.FluentApi, MappingPriorityType.Attribute, MappingPriorityType.Aop)
                        .UseMonitorCommand(cmd =>
                        {
                            Trace.WriteLine(cmd.CommandText + ";");
                        })
                        .Build();
                //软删除
                fsql.GlobalFilter.Apply<ISoftDelete>("IsDeleted", a => a.IsDeleted == false);
                return fsql;
            };

            services.AddSingleton<IFreeSql>(fsql);
            services.AddFreeRepository();
            services.AddScoped<UnitOfWorkManager>();

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var orm = scope.ServiceProvider.GetRequiredService<IFreeSql>();

                //只有实例化了AppIdentityDbContext，才能正常调用OnModelCreating，不然直接使用仓储，无法调用DbContext中的OnModelCreating方法，配置的TodoConfiguration 就会没有生效
                services.AddFreeDbContext<AppIdentityDbContext>(options => options
                            .UseFreeSql(orm)
                            .UseOptions(new DbContextOptions()
                            {
                                EnableCascadeSave = true
                            }));
            }

            services.AddIdentityCore<AppUser>(o =>
            {
                o.SignIn.RequireConfirmedEmail = false;
                o.Stores.MaxLengthForKeys = 128;
            })
            .AddRoles<AppRole>()
            .AddSignInManager()
            .AddFreeSqlStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            // 如果是MVC 另外安装包 <PackageReference Include="Microsoft.AspNetCore.Identity.UI" Version="6.0.7" /> 使用AddDefaultIdentity方法
            // services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<AppRole>()
            //        .AddFreeSqlStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            return services;
        }

        ///<summary>
        /// 自定义Scope 的Serivce 执行 DbContext中的OnModelCreating
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>

        public static IServiceProvider RunScopeService(this IServiceProvider serviceProvider)
        {
            //在项目启动时，从容器中获取IFreeSql实例，并执行一些操作：同步表，种子数据,FluentAPI等
            using (IServiceScope serviceScope = serviceProvider.CreateScope())
            {
                var freeSql = serviceScope.ServiceProvider.GetRequiredService<IFreeSql>();
                using var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

                freeSql.CodeFirst.SyncStructure(
                    typeof(AppUser),
                    typeof(AppRole),
                    typeof(IdentityUserLogin<Guid>),
                    typeof(IdentityUserRole<Guid>),
                    typeof(IdentityUserClaim<Guid>),
                    typeof(IdentityRoleClaim<Guid>),
                    typeof(IdentityUserToken<Guid>)
                );
            }
            return serviceProvider;
        }
    }
}