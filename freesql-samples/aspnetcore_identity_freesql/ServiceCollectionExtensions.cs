using System.Diagnostics;
using aspnetcore_identity_freesql.Models;
using FreeSql;
using FreeSql.Internal;
using IGeekFan.AspNetCore.Identity.FreeSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

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

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies(o => { });
            //.AddJwtBearer(IdentityConstants.ApplicationScheme);
            
            services.AddIdentityCore<AppUser>(o =>
            {
                o.SignIn.RequireConfirmedEmail = false;
                o.Stores.MaxLengthForKeys = 128;
            })
            .AddRoles<AppRole>()
            .AddSignInManager()
            .AddFreeSqlStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            //
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

        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration c)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "aspnetcore_identity_freesql - HTTP API",
                    Version = "v1",
                    Description = "The aspnetcore_identity_freesql Microservice HTTP API. This is a Data-Driven/CRUD microservice sample"
                });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "aspnetcore_identity_freesql.xml"), true);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    Name = "Authorization", //jwt默认的参数名称
                    In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });

            return services;
        }
    }
}