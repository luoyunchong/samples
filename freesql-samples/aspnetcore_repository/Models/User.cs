using IGeekFan.FreeKit.Extras.AuditEntity;

namespace aspnetcore_repository.Models;

public class User : Entity<Guid>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}