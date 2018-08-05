using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Repository.Entity;

namespace TodoList.Repository
{
    public class TodoListDbContext : DbContext
    {


        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
        { }
        /// <summary>
        /// 待办事项
        /// </summary>
        public DbSet<Todo> Todos { set; get; }
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> Users { set; get; }


    }
}
