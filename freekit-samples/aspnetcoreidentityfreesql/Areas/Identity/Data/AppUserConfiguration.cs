using FreeSql.Extensions.EfCoreFluentApi;

namespace aspnetcoreidentityfreesql.Areas.Identity.Data;
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EfCoreTableFluent<AppUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(50);
        builder.Property(x => x.LastName).HasMaxLength(50);
    }
}
