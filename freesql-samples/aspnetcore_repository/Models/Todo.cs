using FreeSql.DataAnnotations;
using IGeekFan.FreeKit.Extras.AuditEntity;

namespace aspnetcore_repository.Models;

[Table(Name = "repo_todo")]
public class Todo : FullAuditEntity<int, Guid>
{    
    /// <summary>
    /// 待办事项
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// 是否完成
    /// </summary>
    public bool IsDone { get; set; }
    /// <summary>
    /// 提醒时间
    /// </summary>
    public DateTime? NotifictionTime { get; set; }
    /// <summary>
    /// 任务完成时间
    /// </summary>
    public DateTime? DoneTime { get; set; }
}