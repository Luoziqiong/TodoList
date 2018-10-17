using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoList.Repository;
using TodoList.Repository.Entity;
using TodoList.Service.Dto.User;

namespace TodoList.Service.Impl
{
    public class UserService:BaseService<User>,IUserService
    {
        private readonly TodoListDbContext todoListDbContext;
        public UserService(TodoListDbContext todoListDbContext)
        {
            this.todoListDbContext = todoListDbContext;
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>1 成功，0 插入出错，-1 已存在该用户</returns>
        public int Register(UserDto user)
        {
            //检验账号唯一性
            var entity = todoListDbContext.Users.Where(o => o.Username == user.Username && o.Password == user.Password).FirstOrDefault();
            if (entity == null)
            {
                //添加账号  
                using (var tran = todoListDbContext.Database.BeginTransaction())//开启事务
                {
                    try
                    {
                        var u = new User { Username = user.Username, Password = user.Password };
                        //添加用户
                        todoListDbContext.Users.Add(u);
                        todoListDbContext.SaveChanges();
                        //添加实例
                        todoListDbContext.Todos.Add(new Todo { Content = "示例",Priority=1, UserId = u.Id });
                        todoListDbContext.SaveChanges();
                        tran.Commit();
                        return 1;
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        return 0;
                        // TODO: Handle failure
                    }
                }
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>0 不存在，其他 用户id</returns>
        public long Login(UserDto user)
        {
            var entity = todoListDbContext.Users.Where(o => o.Username == user.Username && o.Password == user.Password).FirstOrDefault();
            if(entity == null)
            {
                return 0;
            }
            else
            {
                return entity.Id;
            }
        }
    }
}
