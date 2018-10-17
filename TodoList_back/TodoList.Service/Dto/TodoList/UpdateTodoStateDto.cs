using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Service.Dto.TodoList
{
    public class UpdateTodoStateDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 状态Id
        /// </summary>
        public long StateId { get; set; }
    }
}
