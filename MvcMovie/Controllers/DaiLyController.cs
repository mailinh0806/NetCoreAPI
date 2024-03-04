using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
namespace MvcMovie.Controllers;
public class DaiLyController : Controller
{
    public IActionResult Index(){
        return View();
    }
    [HttpPost]
    public IActionResult Index(DaiLy dl){
        string Result= "hello" + "-" + dl.MaDaiLy + "-" + dl.TenDaiLy + "-" + dl.NguoiDaiDien + "-" + dl.DiaChi + "-" + dl.DienThoai + "-" + dl.MaHTPP;
        ViewBag.thongbao = Result;
        return View();
    }
}