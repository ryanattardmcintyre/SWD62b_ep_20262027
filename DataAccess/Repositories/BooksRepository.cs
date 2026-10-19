using Common.Models;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class BooksRepository
    {
        //field which represents an eventual object of type LibraryDbContext
        //reason is: we need to execute queries on that object

        private LibraryDbContext _context;

        //Dependency Injection:
        //- a design pattern which promotes the centralization of instance creation which then allows
        //  the client classes such as BooksRepository to be injected with that same instance

        //- one way of passing the created instance into the client classes is CONSTRUCTOR INJECTION
        public BooksRepository(LibraryDbContext context) {

            _context = context;
        }

        //IQueryable - it defers the execution of the sql statement stored inside using LINQ
        //             ...until it meets a ToList() or we render the page
        public IQueryable<Book> GetBooks()
        {
            //var list = from b in _context.Books
            //           select b; return list;

            return _context.Books; //Select * From Books Where Title like '%@keyword%'
        }

        public IQueryable<Book> GetBooks(string keyword)
        {    //var list = from b in _context.Books
            //             where b.Title.Contains(keyword)
            //           select b;
            //           return list;

                   //1st call               //2nd call to get the same list
            return GetBooks().Where(b => b.Title.Contains(keyword));
        }

        public void AddBook(Book b)
        {
            _context.Books.Add(b); //it adds b to the object _context...which is an abstraction of the db
            _context.SaveChanges();
        }

    }
}
