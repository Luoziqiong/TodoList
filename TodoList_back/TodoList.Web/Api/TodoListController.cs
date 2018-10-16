using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Repository.Entity;
using TodoList.Service;
using TodoList.Service.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.Web.Api
{
    [Route("api/[controller]")]
    public class TodoListController : Controller
    {
        /// <summary>
        /// 获取待办事项列表
        /// </summary>
        /// <param name="todoListService"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/<controller>
        [HttpGet]
        [Route("GetTodoList")]
        public IEnumerable<TodoListDto> GetTodoList([FromServices]ITodoListService todoListService, [FromQuery]long id)
        {
            return todoListService.GetTodoList(id);
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
        public bool AddTodoList([FromServices]ITodoListService todoListService, [FromBody]Todo todo)
        {
            return todoListService.AddTodoList(todo.UserId, todo.Content,todo.Priority);
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
        public bool EditTodoList([FromServices]ITodoListService todoListService, [FromBody]Todo todo)
        {
            return todoListService.EditTodoList(todo.Id, todo.Content,todo.Priority);
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
