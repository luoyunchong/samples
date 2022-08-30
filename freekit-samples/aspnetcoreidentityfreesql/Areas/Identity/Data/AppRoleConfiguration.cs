using FreeSql.Extensions.EfCoreFluentApi;

namespace aspnetcoreidentityfreesql.Areas.Identity.Data
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EfCoreTableFluent<AppRole> model)
        {
        }
    }
}
