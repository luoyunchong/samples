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
                        .UseAutoSyncStructure(true) //�Զ�ͬ��ʵ��ṹ�����ݿ⣬FreeSql����ɨ����򼯣�ֻ��CRUDʱ�Ż����ɱ�
                        .UseMappingPriority(MappingPriorityType.FluentApi, MappingPriorityType.Attribute, MappingPriorityType.Aop)
                        .UseMonitorCommand(cmd =>
                        {
                            Trace.WriteLine(cmd.CommandText + ";");
                        })
                        .Build();
                //��ɾ��
                fsql.GlobalFilter.Apply<ISoftDelete>("IsDeleted", a => a.IsDeleted == false);
                return fsql;
            };

            services.AddSingleton<IFreeSql>(fsql);
            services.AddFreeRepository();
            services.AddScoped<UnitOfWorkManager>();

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var orm = scope.ServiceProvider.GetRequiredService<IFreeSql>();

                //ֻ��ʵ������AppIdentityDbContext��������������OnModelCreating����Ȼֱ��ʹ�òִ����޷�����DbContext�е�OnModelCreating���������õ�TodoConfiguration �ͻ�û����Ч
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

            // �����MVC ���ⰲװ�� <PackageReference Include="Microsoft.AspNetCore.Identity.UI" Version="6.0.7" /> ʹ��AddDefaultIdentity����
            // services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<AppRole>()
            //        .AddFreeSqlStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            return services;
        }

        ///<summary>
        /// �Զ���Scope ��Serivce ִ�� DbContext�е�OnModelCreating
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>

        public static IServiceProvider RunScopeService(this IServiceProvider serviceProvider)
        {
            //����Ŀ����ʱ���������л�ȡIFreeSqlʵ������ִ��һЩ������ͬ������������,FluentAPI��
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