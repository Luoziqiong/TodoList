using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TodoList.Repository.Entity
{
    /// <summary>
    /// 书籍实体
    /// </summary>
    [Table("book_luoziqiong")]
    public class BookEntity
    {
        ///<summary>
        ///Id
        ///</summary>
        [Key]
        public long Id { set; get; }

        ///<summary>
        ///名称
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { set; get; }
        ///<summary>
        ///价格
        /// </summary>
        [Required]
        public decimal Price { set; get; }
        ///<summary>
        ///简介
        /// </summary>
        [Required, MaxLength(200)]
        public string Description { set; get; }
        ///<summary>
        ///出版社
        /// </summary>
        [Required, MaxLength(30)]
        public string Publisher { set; get; }
        ///<summary>
        ///出版日期
        /// </summary>
        public DateTime PublishDate { set; get; }
    }
}
