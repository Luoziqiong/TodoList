using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Repository.Entity;
using TodoList.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.Web.Api
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        // GET: api/<controller>
        [HttpGet]
        [Route("Login")]
        public long Login([FromServices]IUserService userService, [FromQuery]User user)
        {
            return userService.Login(user.Username, user.Password);
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST api/<controller>
        [HttpPost]
        [Route("Register")]
        public int Register([FromServices]IUserService userService, [FromBody]User user)
        {
            return userService.Register(user.Username, user.Password);
        }
    }
}
