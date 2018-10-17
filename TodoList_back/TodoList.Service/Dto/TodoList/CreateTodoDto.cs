using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Service.Dto.TodoList
{
    public class CreateTodoDto
    {
        /// <summary>
        /// 具体内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime FinishDate { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
    }
}
