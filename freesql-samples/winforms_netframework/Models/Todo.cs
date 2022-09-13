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
        /// 待办事项
        /// </summary>
        [Column(StringLength = 500)]
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
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 任务完成时间
        /// </summary>
        public DateTime? DoneTime { get; set; }
    }
}