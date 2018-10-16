using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Service;
using TodoList.Service.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.Web.Api
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
       
        /// <summary>
        /// 获取书籍列表
        /// </summary>
        /// <param name="bookService"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBookList")]
        public IEnumerable<BookDto> GetBookList([FromServices]IBookService bookService)
        {
            return bookService.GetBookList();
        }
        /// <summary>
        /// 获取书籍详情
        /// </summary>
        /// <param name="bookService"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBookDetail")]
        public BookDto GetBookDetail([FromServices]IBookService bookService,[FromQuery]long id)
        {
            return bookService.GetBookDetail(id);
        }
        
        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="bookService"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddBook")]
        public bool AddBook([FromServices]IBookService bookService, [FromBody]CreateBookDto book)
        {
           return  bookService.AddBook(book);
        }
        /// <summary>
        /// 更新书籍
        /// </summary>
        /// <param name="bookService"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateBook")]
        public bool UpdateBook([FromServices]IBookService bookService, [FromBody]UpdateBookDto book)
        {
            return bookService.UpdateBook(book);
        }
        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="bookService"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteBook")]
        public bool DeleteBook([FromServices]IBookService bookService, long id)
        {
            return bookService.DeleteBook(id);
        }
       
       
    }
}
