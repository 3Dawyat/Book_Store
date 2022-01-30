using Book_Store.DB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.BL
{
    public interface IDepServices
    {
        public List<TbDepartment> GetShowDepartments();
        public List<TbDepartment> GetAllShowDepartments();
        public List<TbDepartment> GetRecycleDepartments();
        public Task<bool> ActionOfRecycle(int id, string show);
        public bool DeleteDep(int id);
        public bool AddDep(TbDepartment dep);
        public TbDepartment GetDepById(int? id);
        public TbDepartment GetHome();
        public Task<TbDepartment> EditDep(TbDepartment dep);
    }
}
