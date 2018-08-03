using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Repository.Entity;
using TodoList.Service.Dto;

namespace TodoList.Service
{
    public interface ITodoListService
    {
        IEnumerable<TodoListDto> GetTodoList(long userId);
        bool AddTodoList(long userId, string content,int priority);
        bool DeleteTodoList(long id);
        bool EditTodoList(int id, string content, int priority);
    }
}
