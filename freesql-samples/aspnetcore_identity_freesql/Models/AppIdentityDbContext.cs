using FreeSql;
using IGeekFan.AspNetCore.Identity.FreeSql;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace aspnetcore_identity_freesql.Models;

public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public AppIdentityDbContext(IOptions<IdentityOptions> identityOptions, IFreeSql fsql, DbContextOptions options)
        : base(identityOptions.Value, fsql, options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        //这里直接指定一个静态的 IFreeSql 对象即可，切勿重新 Build()
    }

    protected override void OnModelCreating(ICodeFirst codefirst)
    {
        base.OnModelCreating(codefirst);
        //批量
        //codefirst.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
        codefirst.ApplyConfiguration(new AppUserConfiguration());
        codefirst.ApplyConfiguration(new AppRoleConfiguration());
    }
}