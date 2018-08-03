using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Service.Dto;

namespace TodoList.Service
{
    public interface IBookService
    {
        bool AddBook(CreateBookDto book);
        IEnumerable<BookDto> GetBookList();
        BookDto GetBookDetail(long id);
        bool UpdateBook(UpdateBookDto book);
        bool DeleteBook(long id);
    }
}
