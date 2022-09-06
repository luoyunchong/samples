using aspnetcore_dbcontext.Models;
using FreeSql;

namespace aspnetcore_dbcontext.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(IFreeSql fsql, DbContextOptions options) : base(fsql, options)
        {
        }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ICodeFirst codefirst)
        {
            base.OnModelCreating(codefirst);
        }
    }
}
