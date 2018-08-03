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
        ///<summary>
        ///书籍
        /// </summary>
        public DbSet<BookEntity>  BookEnities{ set; get; }
        /// <summary>
        /// 待办事项
        /// </summary>
        public DbSet<Todo> Todos { set; get; }
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> Users { set; get; }
        /// <summary>
        /// 状态
        /// </summary>
        public DbSet<State> States { set; get; }



    }
}
