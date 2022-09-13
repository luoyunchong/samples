using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

namespace aspnetcore_dbcontext.Models;

public class Todo
{
    public long Id { get; set; }
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