using System.Reflection;
using aspnetcore_repository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using IGeekFan.FreeKit.Extras.Dependency;
using IGeekFan.FreeKit.Extras.FreeSql;
using IGeekFan.FreeKit.Extras.Security;

var builder = WebApplication.CreateBuilder(args);
var c = builder.Configuration;
// Add services to the container.

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((webBuilder, containerBuilder) =>
    {
        Assembly[] currentAssemblies = { typeof(Program).Assembly };
        containerBuilder.RegisterModule(new UnitOfWorkModule(currentAssemblies));
        containerBuilder.RegisterModule(new FreeKitModule(currentAssemblies));
    });

builder.Services.AddControllers(options =>
{
    options.Filters.AddService(typeof(UnitOfWorkActionFilter));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o=> o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "aspnetcore_repository.xml"), true));

builder.Services.AddFreeSql(c);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();//认证

app.UseAuthorization();
#region 自定义中间件，给ICurrentUserAccessor中的CurrentUser赋值
app.UseCurrentUserAccessor();
#endregion
app.MapControllers();

app.Run();
