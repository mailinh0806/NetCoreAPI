using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.Models.Process;
using OfficeOpenXml;
using X.PagedList;

namespace MvcMovie.Controllers
{
    public class PersonController : Controller
   {
    private readonly ApplicationDbcontext _context;
    private ExcelProcess _excelProcess = new ExcelProcess();
    public PersonController(ApplicationDbcontext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index(int? page, int? PageSize)
        {
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="3", Text= "3"},
                new SelectListItem() { Value="5", Text= "5"},
                new SelectListItem() { Value="10", Text= "10"},
                new SelectListItem() { Value="15", Text= "15"},
                new SelectListItem() { Value="25", Text= "25"},
                new SelectListItem() { Value="50", Text= "50"},
            };
            int pagesize = (PageSize ?? 3);
            ViewBag.psize = pagesize;
            var model = _context.Person.ToList().ToPagedList(page ?? 1, pagesize);
            return View(model);
        }
    
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PersonID,FullName,Address")] Person person)
    {
        if (ModelState.IsValid)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(person);
    }
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null || _context.Person == null)
        {
            return NotFound();
        }
        var person = await _context.Person.FindAsync(id);
        if (person == null)
        { 
            return NotFound(); 
        }
        return View(person);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("PersonID,FullName,Address")] Person person)
    {
        if (id != person.PersonID)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(person);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(person.PersonID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(person);

    }
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null || _context.Person == null)
        {
            return NotFound();
        }
        var person = await _context.Person.FirstOrDefaultAsync(m => m.PersonID == id);
        if (person == null)
        {
            return NotFound();
        }
        return View(person);
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (_context.Person == null)
        {
            return Problem("Entity set 'ApplicationBbContext.Person'  is null.");
        }
        var person = await _context.Person.FindAsync(id);
        if (person != null)
        {
            _context.Person.Remove(person);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }


    // public IActionResult Create()
    // {
    //     return View();
    // }
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Create([Bind("PersonID,FullName,Address")] Person person)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         _context.Add(person);
    //         await _context.SaveChangesAsync();
    //         return RedirectToAction(nameof(Index));
    //     }
    //     return View(person);
    // }

    public async Task<IActionResult> Upload()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file != null)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                ModelState.AddModelError("", "Please choose excel file to upload");
            }
            else
            {
                
                var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                var fileLocation = new FileInfo(filePath).ToString();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    var dt = _excelProcess.ExcelToDataTable(fileLocation);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var ps = new Person();
                        ps.PersonID = dt.Rows[i][0].ToString();
                        ps.FullName = dt.Rows[i][1].ToString();
                        ps.Address = dt.Rows[i][2].ToString();
                        _context.Add(ps);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                    
                }
            }
        }
                        return RedirectToAction(nameof(Index));
        // return View(ps2);
    }
    public ActionResult Download()
    {
        var fileName = "YourFileName" + ".xls";
        using(ExcelPackage excelPackage = new ExcelPackage())
        {
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
            worksheet.Cells["A1"].Value = "PersonID";
            worksheet.Cells["B1"].Value = "FullName";
            worksheet.Cells["C1"].Value = "Address";
            var personList = _context.Person.ToList();
            worksheet.Cells["A2"].LoadFromCollection(personList);
            var stream = new MemoryStream(excelPackage.GetAsByteArray());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" , fileName);
        }
    }
    private bool PersonExists(string id)
    {
        return ( _context.Person?.Any(e => e.PersonID == id)).GetValueOrDefault();
    }
    }
    
}