using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Service.Dto.User
{
    public class UserDto
    {
        /// <summary>
        /// 用户名
        /// </summary>   
        public string Username { set; get; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { set; get; }
    }
}
