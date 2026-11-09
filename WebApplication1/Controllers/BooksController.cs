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
        private CategoriesRepository _categoriesRepository;
        public BooksController(BooksRepository booksRepository,
                                CategoriesRepository categoryRepository) {
            
            _categoriesRepository = categoryRepository;
            _booksRepository = booksRepository;
        }


        public IActionResult Details(string id)
        {
            var book = _booksRepository.GetBooks().SingleOrDefault(x => x.Isbn == id);
            if (book == null)
            {
                TempData["error"] = "Book doesn't exist";
                return RedirectToAction("Index"); //Books/Index
            }
            else
            {
                return View(book);
            }


        }


        public IActionResult Index(int lastIndexOf=0, string q="next")
        {
            var NoOfBooksInDb = _booksRepository.GetBooks().Count();

            IQueryable<Book> list;
            if (q != "next") //previous
            {
                if (lastIndexOf - 10 >= 0)
                {   //26 - 20 = 06 -> 16 (lastIndexOf)
                    int num = (lastIndexOf - (10 * 2) <0 ? 0: lastIndexOf - (10 * 2));
                    
                    list = _booksRepository.GetBooks().Skip(num).Take(10);
                    TempData["counter"] = (num + 9);
                }
                else
                {
                    list = _booksRepository.GetBooks().Skip(0).Take(10);
                    TempData["counter"] = 9;
                }
            }
            else //next
            {
                    list = _booksRepository.GetBooks().Skip(lastIndexOf).Take(10);
                TempData["counter"] = lastIndexOf + 9;
            }
            return View(list);
        }

        public IActionResult Search(string keyword)
        {
            var list = _booksRepository.GetBooks(keyword);
            return View("Index", list); //we want to redirect the user to the Index page instead
                                        //NOT the search page
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
          var categories = _categoriesRepository.GetCategories();
            //TempData["categories"] = categories;
            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        //IWebHostEnvironment is the framework service we need to obtain dynamically the absolute Path where
        //we will be saving our files; it is being injected only into this method using Method-Injection
        public IActionResult Create(Book b, IFormFile file, [FromServices]IWebHostEnvironment host)
        {
            try
            {
                if (_booksRepository.GetBooks().SingleOrDefault(x => x.Isbn == b.Isbn) == null)
                {
                    if (file != null)
                    {
                        //file is uploaded on the web server
                        //1. filename issue
                        string filename = b.Isbn + Path.GetExtension(file.FileName);

                        //2. where we are going to save the file on disk?
                        string absolutePath = host.WebRootPath + "\\images\\" + filename;

                        using (var fs = new FileStream(absolutePath, FileMode.CreateNew))
                        {
                            file.CopyTo(fs);
                        }

                        //3. saving the relative path into the db
                        //absolutePaths are used for saving the file on disk
                        //relativePaths are used to be able to render the image in an <img> control
                        b.Cover = "\\images\\" + filename;

                    }
                    //saving the generated path into the instance b to be saved into the db
                    _booksRepository.AddBook(b);
                    TempData["message"] = "Book was added successfully"; //ViewBag.Message = ....

                    ModelState.Clear();


                }else
                {
                    TempData["error"] = "Error: book already exists in the database";
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Book was not input, check your inputs";
            }

            var categories = _categoriesRepository.GetCategories();
            ViewBag.Categories = categories;

            return View(); //redirects the user to the same page of origin i.e. back to same page
                           //option 2: redirect the user to a different page or method
                           //RedirectToAction("Index");
        }




        [HttpGet]
        public IActionResult Edit(string id) 
        {
            var originalBook = _booksRepository.GetBooks().SingleOrDefault(x => x.Isbn == id);
            if(originalBook == null)
            {
                TempData["error"] = "Book doesn't exist";
                return RedirectToAction("Index");

            }
            var categories = _categoriesRepository.GetCategories();
            ViewBag.Categories = categories.ToList();
            return View(originalBook);
        
        }

        [HttpPost]
        public IActionResult Edit(Book b, IFormFile file, [FromServices] IWebHostEnvironment host)
        {
            try
            {
                if (_booksRepository.GetBooks().SingleOrDefault(x => x.Isbn == b.Isbn) != null)
                {
                    if (file != null)
                    {
                        //file is uploaded on the web server
                        //1. filename issue
                        string filename = b.Isbn + Path.GetExtension(file.FileName);

                        //2. where we are going to save the file on disk?
                        string absolutePath = host.WebRootPath + "\\images\\" + filename;

                        if (System.IO.File.Exists(absolutePath)) { System.IO.File.Delete(absolutePath); }


                        using (var fs = new FileStream(absolutePath, FileMode.CreateNew))
                        {
                            file.CopyTo(fs);
                        }

                        //3. saving the relative path into the db
                        //absolutePaths are used for saving the file on disk
                        //relativePaths are used to be able to render the image in an <img> control
                        b.Cover = "\\images\\" + filename;

                    }
                    //saving the generated path into the instance b to be saved into the db
                    _booksRepository.UpdateBook(b);
                    TempData["message"] = "Book was updated successfully"; //ViewBag.Message = ....

                    ModelState.Clear();


                }
                else
                {
                    TempData["error"] = "Error: book doesn't exist in the database";
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Book was not input, check your inputs";
            }

            
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(string id)
        {
            _booksRepository.DeleteBook(id);
            TempData["message"] = "Book deleted";
            return RedirectToAction("Index");
        }


    }
}
