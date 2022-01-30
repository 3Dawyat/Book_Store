using Book_Store.BL;
using Book_Store.DB_Models;
using Book_Store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Book_Store.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
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
        public IActionResult Dashbord()
        {
            DashbordModel model = new DashbordModel();
            model.Books = IBook.GetShowBooks();
            model.Departments = IDep.GetAllShowDepartments();
            return View(model);
        }
        public IActionResult Recycle()
        {
            DashbordModel model = new DashbordModel();
            model.Books = IBook.GetRecycleBooks();
            model.Departments = IDep.GetRecycleDepartments();
            ViewBag.Books = model.Books;
            ViewBag.Dep = model.Departments;
            return View(model);
        }
        public IActionResult FormBook(int? id)
        {
            ViewBag.Departments = IDep.GetShowDepartments();
            if (id != null)
            {
                return View(IBook.GetById(id));
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult SaveDepartment(TbDepartment dep)
        {
            if (dep.DepId == 0)
            {
                IDep.AddDep(dep);
            }
            else
            {
                IDep.EditDep(dep);
            }
            return RedirectToAction("Dashbord");
        }
        [HttpPost]
        public IActionResult SaveBook(TbBook book, IFormFile img, IFormFile file)
        {
            if (book.BokId == 0)
            {
                if (file != null)
                {
                    book.BokFile = IFile.AddFile(file, book.BokName);
                }
                if (img != null)
                {
                    book.BokImage = IFile.AddFile(img, Convert.ToString(Guid.NewGuid()));
                }
                IBook.AddBook(book);
            }
            else
            {
                TbBook oldbook = IBook.GetById(book.BokId);
                book.BokFile = oldbook.BokFile;
                book.BokImage = oldbook.BokImage;
                if (file != null)
                {
                    IFile.DeleteFile(book.BokFile);
                    book.BokFile = IFile.AddFile(file, book.BokFile);
                }
                if (img != null)
                {
                    IFile.DeleteFile(book.BokImage);
                    book.BokImage = IFile.AddFile(img, Convert.ToString(Guid.NewGuid()));
                }
                IBook.EditBook(book);
            }
            return RedirectToAction("Dashbord");
        }
        public IActionResult FormDep(int? id)
        {
            if (id != null)
            {
                return View(IDep.GetDepById(id));
            }
            else
            {
                return View();
            }
        }
        public IActionResult DeleteDep(int Id)
        {
            IDep.DeleteDep(Id);
            return RedirectToAction("Recycle");
        }
        public IActionResult MoveDepToRecycle(int id)
        {
            IDep.ActionOfRecycle(id, "False");
            return RedirectToAction("Dashbord");
        }
        public IActionResult ShowDep(int id)
        {
            IDep.ActionOfRecycle(id, "True");
            return RedirectToAction("Dashbord");
        }
        public IActionResult MoveBookToRecycle(int id)
        {
            IBook.ActionOfRecycle(id, "False");
            return RedirectToAction("Dashbord");
        }
        public IActionResult ShowBook(int id)
        {
            IBook.ActionOfRecycle(id, "True");
            return RedirectToAction("Dashbord");
        }
        public IActionResult DeleteBook(int Id)
        {
            TbBook oldBook = IBook.GetById(Id);
            IFile.DeleteFile(oldBook.BokImage);
            IFile.DeleteFile(oldBook.BokFile);
            IBook.DeleteBook(Id);
            return RedirectToAction("Recycle");
        }
    }
}
