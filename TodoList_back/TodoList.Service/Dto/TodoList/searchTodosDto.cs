using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Service.Dto.TodoList
{
    public class SearchTodosDto
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 状态Id
        /// </summary>
        public long StateId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
    }
}
