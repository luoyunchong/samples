using System;
using FreeSql.DataAnnotations;

namespace winforms_netframework.Models
{
    [Table(Name = "winform_todo")]
    public class Todo
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }
        [Column(StringLength = 500)]
        public string Message { get; set; }
        public bool IsDone { get; set; }
        public DateTime? NotifictionTime { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}