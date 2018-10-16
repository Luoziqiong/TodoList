using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Service.Dto.User;

namespace TodoList.Service
{
    public interface IUserService
    {
        int Register(UserDto user);
        long Login(UserDto user);
    }
}
