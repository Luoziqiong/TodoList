using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Service
{
    public interface IUserService
    {
        int Register(string name, string password);
        long Login(string name, string password);
    }
}
