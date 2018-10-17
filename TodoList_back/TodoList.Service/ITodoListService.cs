using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Repository.Entity;
using TodoList.Service.Dto;
using TodoList.Service.Dto.TodoList;

namespace TodoList.Service
{
    public interface ITodoListService
    {
        IEnumerable<TodoDto> GetTodoList(SearchTodosDto condition);
        bool AddTodoList(CreateTodoDto todo);
        bool DeleteTodoList(long id);
        bool EditTodoList(UpdateTodoDto todo);
        bool UpdateTodoListState(UpdateTodoStateDto todo);
    }
}
