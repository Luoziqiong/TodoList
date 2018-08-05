using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Service.Dto;

namespace TodoList.Service
{
    public interface IUserService
    {
        bool Register(UserDto user);
        long Login(UserDto user);
        bool UpdatePassword(UpdateUserDto user);
    }
}
