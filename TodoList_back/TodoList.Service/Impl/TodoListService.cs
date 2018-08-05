using System;
using System.Collections.Generic;
using System.Linq;
using TodoList.Repository;
using TodoList.Repository.Entity;
using TodoList.Service.Dto;

namespace TodoList.Service.Impl
{
    public class TodoListService:BaseService<Todo>,ITodoListService
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
        public IEnumerable<TodoListDto> GetTodoList(long userId)
        {
            //var sqlString = $"select * from  lzq_todolist as t inner join lzq_state as s on t.StateId = s.Id where s.UserId = @UserId";
            //var todolist = todoListDbContext.Set<TodoListDto>().FromSql(sqlString,new SqlParameter("@UserId", userId)).ToList();
            var todolist = todoListDbContext.Todos.Where(o => o.UserId == userId).Select(o => new TodoListDto()
            {
               Id = o.Id,
               Content = o.Content,
               StartDate = o.StartDate,
               State  = o.State,
               FinishDate = o.FinishDate,
               Priority = o.Priority

            }).ToList();
            return todolist;
        }
        /// <summary>
        /// 根据优先级获取待办事项列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<TodoListDto> GetTodoListByPriority(long userId,int priority)
        {
            if (priority == 0)
            {
                return GetTodoList(userId);
            }
            else
            {
                var todolist = todoListDbContext.Todos.Where(o => o.UserId == userId && o.Priority == priority).Select(o => new TodoListDto()
                {
                    Id = o.Id,
                    Content = o.Content,
                    StartDate = o.StartDate,
                    State = o.State,
                    FinishDate = o.FinishDate,
                    Priority = o.Priority

                }).ToList();
                return todolist;
            }
           
        }
        /// <summary>
        /// 删除待办事项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteTodoList(long id)
        {
            var todo = todoListDbContext.Todos.Where(o => o.Id == id).FirstOrDefault();
            if (todo == null)
            {
                throw new Exception();
            }
            todoListDbContext.Todos.Remove(todo);
            return todoListDbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 添加代办事项
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="content"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public bool AddTodoList(CreateTodoListDto todo)
        {
            todoListDbContext.Todos.Add(new Todo()
            {
                Content = todo.Content,
                Priority = todo.Priority,
                StartDate = todo.StartDate,
                FinishDate = todo.FinishDate,
                UserId = todo.UserId
            });
            return todoListDbContext.SaveChanges() > 0;

        }
        /// <summary>
        /// 编辑代办事项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public bool EditTodoList(UpdateTodoListDto todo)
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
        /// 改变代办事项的状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public bool ChangeTodoState(UpdateTodoListDto todo)
        {
            var entity = todoListDbContext.Todos.Where(o => o.Id == todo.Id).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception();
            }
            entity.State = todo.State;
            return todoListDbContext.SaveChanges() > 0;
        }
    }
}
