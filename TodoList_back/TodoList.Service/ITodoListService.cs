using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Repository.Entity;
using TodoList.Service.Dto;

namespace TodoList.Service
{
    public interface ITodoListService
    {
        IEnumerable<TodoListDto> GetTodoListByPriority(long userId, int priority);
        bool AddTodoList(CreateTodoListDto todo);
        bool DeleteTodoList(long id);
        bool EditTodoList(UpdateTodoListDto todo);
        bool ChangeTodoState(UpdateTodoListDto todo);
    }
}
