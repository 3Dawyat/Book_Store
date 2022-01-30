using Book_Store.DB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.BL
{
    public interface IBookServices
    {
        public List<TbBook> GetShowBooks();
        public List<TbBook> GetRecycleBooks();
        public List<TbBook> GetAllBooksByDep(int page);
        public bool AddBook(TbBook book);
        public TbBook GetById(int? id);
        public Task<bool> DeleteBook(int id);
        public Task<TbBook> EditBook(TbBook book);
        public Task<bool> ActionOfRecycle(int id, string show);
    }
}
