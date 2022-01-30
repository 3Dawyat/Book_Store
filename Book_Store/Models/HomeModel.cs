using Book_Store.DB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models
{
    public class HomeModel
    {
        public List<TbBook> LastAdded { get; set; }
        public TbDepartment Home{ get; set; }
    }
}
