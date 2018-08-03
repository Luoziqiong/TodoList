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
            var sqlString = $"select * from  lzq_todolist as t inner join lzq_state as s on t.StateId = s.Id where s.UserId = @UserId";
            var todolist = todoListDbContext.Set<TodoListDto>().FromSql(sqlString,new SqlParameter("@UserId", userId)).ToList();
            return todolist;
        }
        /// <summary>
        /// 删除待办事项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteTodoList(long id)
        {
            var todo = todoListDbContext.Todos.Where(o => o.Id == id).FirstOrDefault();
            if
            todoListDbContext.Remove()
        }
        /// <summary>
        /// 添加代办事项
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="content"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public bool AddTodoList(long userId,string content,int priority)
        {
            List<Todo> list = new List<Todo>();
            // 数据库连接字符串
            var connStr = "server=192.168.50.208;port=3306;Initial Catalog=training;user id=training;password=123456;ConnectionReset=false;SslMode = none;";
            // connection
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"INSERT INTO `lzq_todolist` (`content`, `userId`,`priority`) VALUES (@content, @userId,@priority)";
                cmd.Parameters.Add(new MySqlParameter("@content", content));
                cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                cmd.Parameters.Add(new MySqlParameter("@priority", priority));
                cmd.ExecuteNonQuery();
               
            }
            return true;
        }
        /// <summary>
        /// 编辑代办事项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public bool EditTodoList(int id, string content, int priority)
        {
            List<Todo> list = new List<Todo>();
            // 数据库连接字符串
            var connStr = "server=192.168.50.208;port=3306;Initial Catalog=training;user id=training;password=123456;ConnectionReset=false;SslMode = none;";
            // connection
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"update lzq_todolist set content = @content,priority= @priority where id = @Id";
                cmd.Parameters.Add(new MySqlParameter("@content", content));
                cmd.Parameters.Add(new MySqlParameter("@Id", id));
                cmd.Parameters.Add(new MySqlParameter("@priority", priority));
                cmd.ExecuteNonQuery();

            }
            return true;
        }
    }
}
