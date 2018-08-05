using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoList.Repository;
using TodoList.Repository.Entity;
using TodoList.Service.Dto;

namespace TodoList.Service.Impl
{
    public class UserService:BaseService<User>,IUserService
    {
        private readonly TodoListDbContext todoListDbContext;
        public UserService(TodoListDbContext todoListDbContext)
        {
            this.todoListDbContext = todoListDbContext;
        }
        //用户注册
        public bool Register(UserDto user)
        {
            todoListDbContext.Users.Add(new User()
            {
                Username = user.Username,
                Password = user.Password
              
            });
            return todoListDbContext.SaveChanges() > 0; 

        }

        public long Login(UserDto user)
        {
            var u = todoListDbContext.Users.Where(o => o.Username == user.Username && o.Password == user.Username).FirstOrDefault();
            if (u == null)
            {
                throw new Exception();
            }
            return u.Id;
        }
        public bool UpdatePassword(UpdateUserDto user)
        {
            var u = todoListDbContext.Users.Where(o => o.Id == user.Id && o.Password == user.OldPassword).FirstOrDefault();
            if (u == null)
            {
                throw new Exception();
            }

            u.Password = user.NewPassword;
            return todoListDbContext.SaveChanges() > 0 ;
        }
    }
}
