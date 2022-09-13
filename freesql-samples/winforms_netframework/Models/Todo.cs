using System;
using FreeSql.DataAnnotations;

namespace winforms_netframework.Models
{
    [Table(Name = "winform_todo")]
    public class Todo
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [Column(StringLength = 500)]
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
        /// ����ʱ��
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// �������ʱ��
        /// </summary>
        public DateTime? DoneTime { get; set; }
    }
}