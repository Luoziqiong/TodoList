using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TodoList.Repository.Entity
{
    /// <summary>
    /// 状态实体
    /// </summary>
    [Table("lzq_state")]
    public class State
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public long Id { set; get; }
        /// <summary>
        /// 状态描述
        /// </summary>
        [Required,MaxLength(10)]
        public string state { set; get; }
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
