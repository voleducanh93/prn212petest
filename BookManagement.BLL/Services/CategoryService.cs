using BookManagement.DAL.Models;
using BookManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.BLL.Services
{
    public class CategoryService
    {
        //thằng này gọi Repo để lo table
        //chính nó lại bị GUI gọi
        //GUI -> Service (here) -> Repo -> ...
        private CategoryRepository _repo = new(); //new lun, k sợ do trong repo tự new DbContext mỗi lần chơi Db

        public List<BookCategory> GetAllCategories()
        {
            return _repo.GetAll();
        }
        //nhớ là đồ Category vào GUI thì là teo đầu dê bán thịt heo
    }
}
