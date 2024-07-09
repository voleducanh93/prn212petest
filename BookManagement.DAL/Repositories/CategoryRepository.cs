using BookManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DAL.Repositories
{
    public class CategoryRepository
    {
        //class này chính là Cabinet chỉ chơi với Category
        //nó cần nhờ đến DbContext
        //đừng quên: GUI -> Service -> REPO (HERE!) -> DBCONTEXT -> TABLE
        //NHỚ NEW CONTEXT MỖI LẦN DÙNG

        private BookManagementDbContext _context;

        public List<BookCategory> GetAll()
        {
            _context = new();
            return _context.BookCategories.ToList(); //ta k join vì chỉ cần trong nội bộ table Category (3 cột id, genre, desc)
        }
    }
}
