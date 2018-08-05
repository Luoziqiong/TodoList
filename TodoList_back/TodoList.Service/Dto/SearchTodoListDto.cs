using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Service.Dto
{
    public class SearchTodoListDto
    {
        public long UserId { get; set; }
        public int Priority { get; set; }
    }
}
