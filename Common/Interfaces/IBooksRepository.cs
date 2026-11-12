using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Interfaces
{
    public interface IBooksRepository
    {
        IQueryable<Book> GetBooks();
        IQueryable<Book> GetBooks(string keyword);

        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(string isbn);


    }
}
