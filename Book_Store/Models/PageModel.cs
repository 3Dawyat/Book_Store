using Book_Store.DB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models
{
    public class PageModel
    {
        public List<TbBook> Books { get; set; }
        public List<TbDepartment> Departments { get; set; }
        public TbDepartment Department { get; set; }

    }
}
