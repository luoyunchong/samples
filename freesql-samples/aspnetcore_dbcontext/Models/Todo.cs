using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

namespace aspnetcore_dbcontext.Models;

public class Todo
{
    public long Id { get; set; }
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