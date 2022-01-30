using Book_Store.DB_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.BL
{

    public class BookServices : IBookServices
    {
        public LibraryContext Context { get; }
        public BookServices(LibraryContext _Context)
        {
            Context = _Context;
        }
        public bool AddBook(TbBook book)
        {
            try
            {
                Context.TbBooks.Add(book);
                Context.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> ActionOfRecycle(int id, string show)
        {
            TbBook book = GetById(id);
            book.Show = show;
            try
            {
                using (var db = new LibraryContext())
                {
                    db.TbBooks.Attach(book).Property(x => x.Show).IsModified = true;
                    int affected = await db.SaveChangesAsync();
                    if (affected == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteBook(int id)
        {
            try
            {
                using (var db = new LibraryContext())
                {
                    TbBook book = db.TbBooks.Where(a => a.BokId == id).FirstOrDefault();
                    db.Remove(book);
                    int affected = await db.SaveChangesAsync();
                    if (affected == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<TbBook> EditBook(TbBook book)
        {
            try
            {
                using (var db = new LibraryContext())
                {
                    db.TbBooks.Update(book);
                    int affected = await db.SaveChangesAsync();
                    if (affected == 1)
                    {
                        return book;
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        public List<TbBook> GetAllBooksByDep(int page)
        {
            return Context.TbBooks.Where(a => a.DepId == page && a.Show != "False").OrderByDescending(a => a.AddDate).ToList();
        }

        public List<TbBook> GetShowBooks()
        {
            return Context.TbBooks.Where(a => a.Show != "False").Include(a => a.Dep).OrderByDescending(a => a.AddDate).ToList();
        }
        public List<TbBook> GetRecycleBooks()
        {
            return Context.TbBooks.Where(a => a.Show == "False").Include(a => a.Dep).ToList();
        }
        public TbBook GetById(int? id)
        {
            return Context.TbBooks.Where(a => a.BokId == id).FirstOrDefault();
        }


    }
}
