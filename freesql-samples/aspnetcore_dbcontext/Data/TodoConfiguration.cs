using aspnetcore_dbcontext.Models;
using FreeSql.Extensions.EfCoreFluentApi;

namespace aspnetcore_dbcontext.Data;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EfCoreTableFluent<Todo> eb)
    {
        eb.ToTable("dbcontext_todo");
        eb.HasKey(u => u.Id).Help().Property(u => u.Id).IsIdentity(true);
        eb.Property(u => u.Message).HasMaxLength(500);
    }
}