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
