using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Service.Dto
{
    public class TodoListDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int Priority { get; set; }
        public int State { get; set; }
        public long UserId { get; set; }

    }
}
