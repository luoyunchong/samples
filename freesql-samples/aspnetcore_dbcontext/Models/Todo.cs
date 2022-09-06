using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

namespace aspnetcore_dbcontext.Models;

public class Todo
{
    [Column(IsPrimary = true, IsIdentity = true)]
    public long Id { get; set; }
    [StringLength(500)]
    public string Message { get; set; }
    public bool IsDone { get; set; }
    public DateTime? NotifictionTime { get; set; }
}