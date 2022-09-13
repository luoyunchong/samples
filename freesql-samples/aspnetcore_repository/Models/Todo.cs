using FreeSql.DataAnnotations;
using IGeekFan.FreeKit.Extras.AuditEntity;

namespace aspnetcore_repository.Models;

[Table(Name = "repo_todo")]
public class Todo : FullAuditEntity<int, Guid>
{    
    /// <summary>
    /// ��������
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// �Ƿ����
    /// </summary>
    public bool IsDone { get; set; }
    /// <summary>
    /// ����ʱ��
    /// </summary>
    public DateTime? NotifictionTime { get; set; }
    /// <summary>
    /// �������ʱ��
    /// </summary>
    public DateTime? DoneTime { get; set; }
}