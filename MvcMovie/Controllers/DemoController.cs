using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
   public class DemoController : Controller
   {
    public IActionResult Index()
        {
            return View();
        }
    [HttpPost]
    public IActionResult Index(Person ps){
        string strResult = "Xin Chào" + ps.PersonID + "-" + ps.FullName;
        ViewBag.info = strResult;
        return View(); 
    }
   }
    
}