using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoList.Repository;
using TodoList.Repository.Entity;
using TodoList.Service.Dto;

namespace TodoList.Service.Impl
{
    public class BookService:BaseService<BookDto>,IBookService
    {
        private readonly TodoListDbContext todoListDbContext;
        public BookService(TodoListDbContext todoListDbContext)
        {
            this.todoListDbContext = todoListDbContext;
        }
        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool AddBook(CreateBookDto book)
        {
            todoListDbContext.BookEnities.Add(new BookEntity()
            {
                Name = book.Name,
                Price = book.Price,
                PublishDate = book.PublishDate,
                Description = book.Description,
                Publisher = book.Publisher
            });
            return todoListDbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 获取书籍列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookDto> GetBookList()
        {
            var bookList = todoListDbContext.BookEnities.Select(b => new BookDto()
            {
                Id = b.Id,
                Name = b.Name,
                Publisher = b.Publisher,
                PublishDate = b.PublishDate,
                Description = b.Description,
                Price = b.Price

            }).ToList();

            return bookList;
        }
        /// <summary>
        /// 根据书籍Id获取书籍详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BookDto GetBookDetail(long id)
        {
            var book = todoListDbContext.BookEnities.Where(o => o.Id == id).Select(o => new BookDto()
            {
                Id = o.Id,
                Name = o.Name,
                Description = o.Description,
                Publisher = o.Publisher,
                PublishDate = o.PublishDate,
                Price = o.Price
            }).FirstOrDefault();
            return book;
        }
        /// <summary>
        /// 修改书籍信息
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool UpdateBook(UpdateBookDto book)
        {
            var entity = todoListDbContext.BookEnities.Where(o => o.Id == book.Id).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception();
            }
            entity.Price = book.Price;
            entity.Description = book.Description;
            return todoListDbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteBook(long id)
        {
            var entity = todoListDbContext.BookEnities.Where(o => o.Id == id).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception();
            }
            todoListDbContext.BookEnities.Remove(entity);
            return todoListDbContext.SaveChanges() > 0;
        }
    }
}
