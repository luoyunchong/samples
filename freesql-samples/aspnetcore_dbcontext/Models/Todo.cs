using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

namespace aspnetcore_dbcontext.Models;

public class Todo
{
    public long Id { get; set; }
    public string Message { get; set; }
    public bool IsDone { get; set; }
    public DateTime? NotifictionTime { get; set; }
}