using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Service.Dto
{
    public class UpdateTodoListDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int Priority { get; set; }
        public int State { get; set; }
       
    }
}
