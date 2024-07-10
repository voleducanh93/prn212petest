using BookManagement.DAL.Models;
using BookManagement.DAL.Repositories;

namespace BookManagement.BLL.Services
{

    //GUI WPF -> THẰNG NÀY SERVICE -> REPO -> DBCONTEXT -> TABLE
    //class này lại cũng là 1 dạng Cabinet, nhưng chỉ chơi với table
    //Book, chơi trong RAM, tính toán gì đó đưa trở lại GUI, hoặc lấy từ GUI đem xuống Repo -> từ đó xuống DbContext rồi xuống table
    //nó sẽ bị gọi bởi GUI
    //nhưng nó lại đi gọi Repo, cần khai báo biến Repo
    
    public class BookService
    {
        private BookRepository _repo = new();

        // các hàm CUD Book trong RAM, đều nhờ cậy từ Repo
        //đặt tên hàm gần với đời thực

        public List<Book> GetAllBooks()
        {
            return _repo.GetAll(); //phần trang, sort trước khi trả về
        }

        //hàm CreateBook(nhận new Book(){} từ GUI và đẩy xuống Repo)
        //hàm Repo vừa viết xong
        public void CreateBook(Book x)
        {
            _repo.Create(x); //hàm vừa biết xong. K cần new Repo()
        }

        public void UpdateBooK(Book x)
        {
            _repo.Update(x);
        }

        public void DeleteBook(Book x)
        {
            _repo.Delete(x);
        }

    }
}
