using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TodoList.Repository;
using TodoList.Repository.Entity;
using TodoList.Service.Dto;
using Microsoft.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using TodoList.Service.Dto.TodoList;

namespace TodoList.Service.Impl
{
    public class TodoListService : BaseService<Todo>, ITodoListService
    {
        private readonly TodoListDbContext todoListDbContext;
        public TodoListService(TodoListDbContext todoListDbContext)
        {
            this.todoListDbContext = todoListDbContext;
        }
        /// <summary>
        /// 获取待办事项列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<TodoDto> GetTodoList(SearchTodosDto condition)
        {
            var todolist = new List<TodoDto>();
            if (condition.Priority == 0 && condition.StateId == 0)
            {
                if (condition.BeginTime != condition.EndTime)
                {
                    todolist = todoListDbContext.Todos.Where(o => o.StartDate >= condition.BeginTime
                    && o.StartDate <= condition.EndTime && o.UserId == condition.UserId).Select(o => new TodoDto()
                    {
                        Id = o.Id,
                        Content = o.Content,
                        StartDate = o.StartDate,
                        FinishDate = o.FinishDate,
                        Priority = o.Priority,
                        StateId = o.StateId,
                    }).ToList();
                }
                else
                {
                    todolist = todoListDbContext.Todos.Where(o => o.StartDate >= condition.BeginTime
                     && o.UserId == condition.UserId).Select(o => new TodoDto()
                     {
                         Id = o.Id,
                         Content = o.Content,
                         StartDate = o.StartDate,
                         FinishDate = o.FinishDate,
                         Priority = o.Priority,
                         StateId = o.StateId,
                     }).ToList();
                }


            }
            else if (condition.Priority == 0)
            {
                if (condition.BeginTime != condition.EndTime)
                {
                    todolist = todoListDbContext.Todos.Where(o => o.StartDate >= condition.BeginTime && o.StartDate <= condition.EndTime && o.UserId == condition.UserId && o.StateId == condition.StateId)
                    .Select(o => new TodoDto()
                    {
                        Id = o.Id,
                        Content = o.Content,
                        StartDate = o.StartDate,
                        FinishDate = o.FinishDate,
                        Priority = o.Priority,
                        StateId = o.StateId,
                    }).ToList();
                }
                else
                {
                    todolist = todoListDbContext.Todos.Where(o => o.StartDate >= condition.BeginTime && o.UserId == condition.UserId && o.StateId == condition.StateId)
                    .Select(o => new TodoDto()
                    {
                        Id = o.Id,
                        Content = o.Content,
                        StartDate = o.StartDate,
                        FinishDate = o.FinishDate,
                        Priority = o.Priority,
                        StateId = o.StateId,
                    }).ToList();
                }

            }
            else if (condition.StateId == 0)
            {
                if (condition.BeginTime != condition.EndTime)
                {
                    todolist = todoListDbContext.Todos.Where(o => o.StartDate >= condition.BeginTime && o.StartDate <= condition.EndTime && o.UserId == condition.UserId && o.Priority == condition.Priority)
                .Select(o => new TodoDto()
                {
                    Id = o.Id,
                    Content = o.Content,
                    StartDate = o.StartDate,
                    FinishDate = o.FinishDate,
                    Priority = o.Priority,
                    StateId = o.StateId,
                }).ToList();
                }
                else
                {
                    todolist = todoListDbContext.Todos.Where(o => o.StartDate >= condition.BeginTime
                && o.UserId == condition.UserId && o.Priority == condition.Priority)
                    .Select(o => new TodoDto()
                    {
                        Id = o.Id,
                        Content = o.Content,
                        StartDate = o.StartDate,
                        FinishDate = o.FinishDate,
                        Priority = o.Priority,
                        StateId = o.StateId,
                    }).ToList();
                }

            }
            else
            {
                if (condition.BeginTime != condition.EndTime)
                {
                    todolist = todoListDbContext.Todos.Where(o => o.StartDate >= condition.BeginTime
                    && o.StartDate <= condition.EndTime
                    && o.UserId == condition.UserId && o.StateId == condition.StateId && o.Priority == condition.Priority)
                        .Select(o => new TodoDto()
                        {
                            Id = o.Id,
                            Content = o.Content,
                            StartDate = o.StartDate,
                            FinishDate = o.FinishDate,
                            Priority = o.Priority,
                            StateId = o.StateId,
                        }).ToList();
                }
                else
                {
                    todolist = todoListDbContext.Todos.Where(o => o.StartDate >= condition.BeginTime
                            && o.UserId == condition.UserId && o.StateId == condition.StateId && o.Priority == condition.Priority)
                                .Select(o => new TodoDto()
                                {
                                    Id = o.Id,
                                    Content = o.Content,
                                    StartDate = o.StartDate,
                                    FinishDate = o.FinishDate,
                                    Priority = o.Priority,
                                    StateId = o.StateId,
                                }).ToList();
                }
            }
            return todolist;
        }
        /// <summary>
        /// 删除待办事项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteTodoList(long id)
        {
            var entity = todoListDbContext.Todos.Where(o => o.Id == id).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception();
            }
            todoListDbContext.Todos.Remove(entity);
            return todoListDbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 添加代办事项
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public bool AddTodoList(CreateTodoDto todo)
        {
            todoListDbContext.Todos.Add(new Todo
            {
                Content = todo.Content,
                Priority = todo.Priority,
                UserId = todo.UserId,
                StartDate = todo.StartDate,
                FinishDate = todo.FinishDate
            });
            return todoListDbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 编辑代办事项
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public bool EditTodoList(UpdateTodoDto todo)
        {
            var entity = todoListDbContext.Todos.Where(o => o.Id == todo.Id).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception();
            }
            entity.Content = todo.Content;
            entity.Priority = todo.Priority;
            entity.StartDate = todo.StartDate;
            entity.FinishDate = todo.FinishDate;
            return todoListDbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 改变待办事项状态
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public bool UpdateTodoListState(UpdateTodoStateDto todo)
        {
            var entity = todoListDbContext.Todos.Where(o => o.Id == todo.Id).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception();
            }
            entity.StateId = todo.StateId;
            return todoListDbContext.SaveChanges() > 0;
        }
    }
}
