using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class BooksJsonRepository : IBooksRepository
    {

        public BooksJsonRepository(string path)
        {
            jsonFile = path;
            if(System.IO.File.Exists(jsonFile)== false)
            {
                System.IO.File.Create(jsonFile).Close();
            }
        }

        private string jsonFile = "";
        public void AddBook(Book book)
        {
            //1. get books
            var existentBooks = GetBooks().ToList();
            //2. append book
            if (existentBooks.SingleOrDefault(x => x.Isbn == book.Isbn) == null)
                existentBooks.Add(book);
            else throw new Exception("Book isbn already exists");

            //3. serialize
            string jsonBooks = JsonSerializer.Serialize(existentBooks);
            //4. save to file
            System.IO.File.WriteAllText(jsonFile, jsonBooks);
        }

        public void DeleteBook(string isbn)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Book> GetBooks()
        {
            string myContents = System.IO.File.ReadAllText(jsonFile);
            //deserialization: converting json string => object

            if (string.IsNullOrEmpty(myContents) == true)
            {
                return new List<Book>().AsQueryable();
            }

            try
            {
                List<Book> books =
                JsonSerializer.Deserialize<List<Book>>(myContents);
                return books.AsQueryable();
            }
            catch (Exception ex) 
            {
                return new List<Book>().AsQueryable();
            }
        }

        public IQueryable<Book> GetBooks(string keyword)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
