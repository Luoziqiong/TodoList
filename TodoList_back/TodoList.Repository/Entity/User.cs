using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TodoList.Repository.Entity
{
    /// <summary>
    /// 用户实体
    /// </summary>
    [Table("lzq_user")]
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public long Id { set; get; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required,MaxLength(45)]
        public string Username { set; get; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [Required,MaxLength(30)]
        public string Password { set; get; }
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
