using FreeSql.Extensions.EfCoreFluentApi;

namespace aspnetcore_identity_freesql.Models;

public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EfCoreTableFluent<AppRole> builder)
    {
    }
}