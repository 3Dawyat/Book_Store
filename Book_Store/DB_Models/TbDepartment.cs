using System;
using System.Collections.Generic;

#nullable disable

namespace Book_Store.DB_Models
{
    public partial class TbDepartment
    {
        public TbDepartment()
        {
            TbBooks = new HashSet<TbBook>();
        }

        public int DepId { get; set; }
        public string DepName { get; set; }
        public string Titel { get; set; }
        public string Description { get; set; }
        public string Saing { get; set; }
        public string Icon { get; set; }
        public string Show { get; set; }


        public virtual ICollection<TbBook> TbBooks { get; set; }
    }
}
