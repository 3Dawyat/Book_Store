using Book_Store.BL;
using Book_Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
namespace Book_Store.Controllers
{
    public class HomeController : Controller
    {
        IBookServices IBook;
        IDepServices IDep;
        IFileServices IFile;
        public HomeController(IBookServices IBookServ, IDepServices IDepp, IFileServices IFileServ)
        {
            IBook = IBookServ;
            IFile = IFileServ;
            IDep = IDepp;
        }
        public IActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.LastAdded = IBook.GetShowBooks();
            model.Home = IDep.GetHome();
            ViewBag.Title = model.Home.DepName;
            ViewBag.Departments = IDep.GetShowDepartments();
            return View(model);
        }
        public IActionResult Page(int id)
        {
            try
            {
                PageModel model = new PageModel();
                model.Books = IBook.GetAllBooksByDep(id);
                model.Departments = IDep.GetShowDepartments();
                model.Department = IDep.GetDepById(id);
                if (model.Department != null)
                {
                    ViewBag.Title = model.Department.DepName;
                    ViewBag.Departments = IDep.GetShowDepartments();
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch 
            {
                return RedirectToAction("Index");
            }
        }
        public IActionResult Download(string FileName)
        {
            if (FileName != null)
            {
                var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pdf")).Root + $@"\{FileName}";
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                return File(fileBytes,"application/force-download",FileName);
            }
            else
            return Redirect("Index");
        }
    }
}
