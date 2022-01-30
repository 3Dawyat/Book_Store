using System;
using System.Collections.Generic;

#nullable disable

namespace Book_Store.DB_Models
{
    public partial class TbBook
    {
        public TbBook()
        {
           AddDate =  TimeZoneInfo.ConvertTime(DateTime.UtcNow,TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time"));
        }
        public int BokId { get; set; }
        public string BokName { get; set; }
        public string BokAuther { get; set; }
        public string BokImage { get; set; }
        public string BokFile { get; set; }
        public string Show { get; set; }
        public int? BokRate { get; set; }
        public int? DepId { get; set; }
        public DateTime? AddDate { get; set; }
        public virtual TbDepartment Dep { get; set; }
    }
}
