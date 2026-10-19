using Microsoft.AspNetCore.Mvc;
using Common.Models;
using DataAccess.Repositories;
namespace WebApplication1.Controllers
{

    /*  Pages (Adding a book, Querying books, etc)
     *   ^
     *  Controllers: services(to ADD a book, to query books => All, Search, Sort, ...)
     *   ^
     *  Repositories
     *   ^
     *   DB (Book model => Books)
     */ 




    public class BooksController : Controller
    {
        private BooksRepository _booksRepository;
        public BooksController(BooksRepository booksRepository) { 
        
            _booksRepository = booksRepository;
        }



        //Functionality 1: Adding a book
        //[Get] step 1: user gets an empty page where to fill in details
        // step 2: user types
        //[Post] step 3: user submits data 


        /// <summary>
        /// This method is called to open a page with blank fields to cater for step 1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book b)
        {
            _booksRepository.AddBook(b);
            return View(); //redirects the user to the same page of origin i.e. back to same page
                           //option 2: redirect the user to a different page or method
                                       //RedirectToAction("Index");
        }

    }
}
