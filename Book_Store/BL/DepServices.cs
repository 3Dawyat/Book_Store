using Book_Store.DB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.BL
{
    public class DepServices : IDepServices
    {
        public LibraryContext Context { get; }
        public DepServices(LibraryContext _Context)
        {
            Context = _Context;
        }
        public bool DeleteDep(int id)
        {
            try
            {
                TbDepartment dep = Context.TbDepartments.Where(a => a.DepId == id).FirstOrDefault();
                Context.Remove(dep);
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<TbDepartment> EditDep(TbDepartment dep)
        {
            try
            {
                using (var db = new LibraryContext())
                {
                    db.TbDepartments.Update(dep);
                    int affected = await db.SaveChangesAsync();
                    if (affected == 1)
                    {
                        return dep;
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        public bool AddDep(TbDepartment dep)
        {
            try
            {
                Context.Add<TbDepartment>(dep);
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<TbDepartment> GetShowDepartments()
        {
            return Context.TbDepartments.Where(a => a.DepId != 7 && a.Show != "False").ToList();
        }
        public List<TbDepartment> GetRecycleDepartments()
        {
            return Context.TbDepartments.Where(a => a.DepId != 7 && a.Show == "False").ToList();
        }
        public TbDepartment GetDepById(int? id)
        {
            return Context.TbDepartments.Where(a => a.DepId == id).FirstOrDefault();
        }
        public async Task<bool> ActionOfRecycle(int id, string show)
        {
            TbDepartment dep = GetDepById(id);
            dep.Show = show;
            try
            {
                using (var db = new LibraryContext())
                {
                    db.TbDepartments.Attach(dep).Property(x => x.Show).IsModified = true;
                    int affected = await db.SaveChangesAsync();
                    if (affected == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public TbDepartment GetHome()
        {

            return Context.TbDepartments.Where(a => a.DepId == 7).FirstOrDefault();
        }
        public List<TbDepartment> GetAllShowDepartments()
        {

            return Context.TbDepartments.Where(a => a.Show != "False").ToList();
        }
    }
}
