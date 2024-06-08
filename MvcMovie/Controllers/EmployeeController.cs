using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
namespace MvcMovie.Controllers;
public class EmployeeController : Controller
{
    public IActionResult Index(){
        return View();
    }
    [HttpPost]
    public IActionResult Index(Employee emp){
        string Result= "hello" + "-" + emp.EmployeeID + "-" + emp.Age;
        ViewBag.thongbao = Result;
        return View();
    }

}

