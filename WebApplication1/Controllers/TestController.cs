using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        //accessed by typing https://mywebsite.com/Test/Index
        public IActionResult Index()
        {
            //....
            
            return View();
        }


        public IActionResult Process(string id)
        {
            //in asp.net there are various built-in objects which
            //allow you to access both from server side and also
            //from the pages
            //e.g. TempData, ViewBag, ViewData, Context
            //e.g. We will eventually pass Model instances to the Views
            TempData["msg"] = "Hello, the id input is " + id;
            return View();
        }


        public IActionResult GetData()
        {
            return Json("{\"name\": \"ryan\"}");
        }

        public IActionResult GetFile(string path)
        {
            return File(new byte[] { }, "application/pdf");
        }
    }
}
