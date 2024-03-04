using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
namespace MvcMovie.Controllers;
public class HeThongPhanPhoiController : Controller
{
    public IActionResult Index(){
        return View();
    }
    [HttpPost]
    public IActionResult Index(HeThongPhanPhoi htpp){
        string Result= "hello" + "-" + htpp.MaHTPP + "-" + htpp.TenHTPP;
        ViewBag.thongbao = Result;
        return View();
    }
}