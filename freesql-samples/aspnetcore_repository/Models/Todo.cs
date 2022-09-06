using IGeekFan.FreeKit.Extras.AuditEntity;

namespace aspnetcore_repository.Models;

public class Todo : FullAuditEntity<int, Guid>
{
    public string Message { get; set; }
    public bool IsDone { get; set; }
    public DateTime? NotifictionTime { get; set; }
}