using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TodoList.Repository.Entity
{
    /// <summary>
    /// 代办事项实体
    /// </summary>
    [Table("lzq_todolist")]
    public class Todo
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 具体内容
        /// </summary>
        [Required,MaxLength(255)]
        public string Content { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Required,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime StartDate{ get; set; }
        /// <summary>
        /// 截止时间
        /// </summary>
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime FinishDate { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        [Required, DefaultValue(0)]
        public int Priority { get; set; }
        /// <summary>
        /// 状态Id
        /// </summary>
        [Required, DefaultValue(0)]
        public long StateId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public long UserId { get; set; }
        /// <summary>
        /// 数据创建时间
        /// </summary>
        [Required,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DbCreateDate { get; set; }
        /// <summary>
        /// 数据更新时间
        /// </summary>
        [Required,DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DbUpdateDate { get; set; }
    }
}
