using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoList.Repository.Entity;
using TodoList.Service;
using TodoList.Service.Dto.TodoList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.Web.Api
{
    /// <summary>
    /// 代办事项api
    /// </summary>
    [Route("api/[controller]")]
    public class TodoListController : Controller
    {
        /// <summary>
        /// 获取待办事项列表
        /// </summary>
        /// <param name="todoListService"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        // POST: api/<controller>
        [HttpPost]
        [Route("GetTodoList")]
        public IEnumerable<TodoDto> GetTodoList([FromServices]ITodoListService todoListService, [FromBody]SearchTodosDto condition)
        {
            return todoListService.GetTodoList(condition);
        }
        /// <summary>
        /// 添加待办事项
        /// </summary>
        /// <param name="todoListService"></param>
        /// <param name="todo"></param>
        /// <returns></returns>
        // POST api/<controller>
        [HttpPost]
        [Route("AddTodoList")]
        public bool AddTodoList([FromServices]ITodoListService todoListService, [FromBody]CreateTodoDto todo)
        {
            return todoListService.AddTodoList(todo);
        }
        /// <summary>
        /// 更新待办事项内容
        /// </summary>
        /// <param name="todoListService"></param>
        /// <param name="todo"></param>
        /// <returns></returns>
        // POST api/<controller>
        [HttpPost]
        [Route("EditTodoList")]
        public bool EditTodoList([FromServices]ITodoListService todoListService, [FromBody]UpdateTodoDto todo)
        {
            return todoListService.EditTodoList(todo);
        }
        /// <summary>
        /// 更新待办事项状态
        /// </summary>
        /// <param name="todoListService"></param>
        /// <param name="todo"></param>
        /// <returns></returns>
        // POST api/<controller>
        [HttpPost]
        [Route("UpdateTodoListState")]
        public bool UpdateTodoListState([FromServices]ITodoListService todoListService, [FromBody]UpdateTodoStateDto todo)
        {
            return todoListService.UpdateTodoListState(todo);
        }
        /// <summary>
        /// 删除待办事项
        /// </summary>
        /// <param name="todoListService"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteTodoList")]
        public bool DeleteTodoList([FromServices]ITodoListService todoListService, [FromQuery]long id)
        {
            return todoListService.DeleteTodoList(id);
        }
    }
}
