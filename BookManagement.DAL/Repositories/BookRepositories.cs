using BookManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.DAL.Repositories
{
    //class này đóng vai BookCabinet đã học
    //nó có các hàm CRUD table Book
    //nhưng mình không cần viết câu SQL, mà chỉ đi gọi đại sứ DB chính là class object từ class DbContext
    //DbContext.cs cũng là 1 dạng Cabinet nhưng chơi thẳng với database do bên trong nó có chuỗi kết nối CSDL
    //class BookRepo chính là Cabinet nhưng chỉ chơi 1 table 
    //cài cho nguyên lí S trong SOLID
    //class này phải khai báo biến DbContext 
    //và có các hàm CRUD table Book
    //vì chơi trực tiếp table nên nó mới thuộc cái level, layer DAL data access layer
    public class BookRepositories
    {
        private BookManagementDbContext _context; //ko new, lúc nào thao tác mới new
        // ---list<Book> _arr = new();
        //               _arr.Add(new Book(){})

        //CRUD method với table Book
        //tên hàm ngắn gọn, mang ý nghĩa của CRUD table
        public List<Book> GetAll()
        {
            _context = new BookManagementDbContext();
            /*return _context.Books.ToList();*/ //Books là DbSet, to hơn List, convert về List
            // Select*from Book lazy loading 
            //select từng cuốn sách trong table, new Book() và add vào List<Book> Books
            //nhưng lười không join, không có new category bên trong cuốn sách, để đảm bảo performance
            //nếu ta muốn lấy info cate ta phải chủ động báo join
            //toán tử include<biến trỏ đến table cần join>
            //                  biến này gọi là: navigation path - đường giúp em sang table khác

            return _context.Books.Include("BookCategory").ToList();
        }    

        //hàm tạo mới cuốn sách 
        public void Create(Book x)
        {
            //new DbContext() rồi mới dùng 
            _context = new();
            _context.Books.Add(x);
            _context.SaveChanges();
        }//đã xong insert cuốn sách . new Book từ GUI đưa xuống, vì từ GUI ta có màn hình nhập mới cuốn sách
        //GUI ta có màn hình nhập mới cuốn sách, pass từ Service
        //
        //TƯƠNG TỰ TA CÓ HÀM Update(Book x), Delete(Book x), Delete(int bookId)
        //remove 1 object từ List, hoặc remove theo PK where Id
        //
    }
}
//Mô hình sử dụng class
//GUI WPF --> SERVICE (BLL) --> REPO (DAL) --> DBCONTEXT --> TABLE
//GUI WPF --> SERVICE (BLL) <-- REPO (DAL) <-- DBCONTEXT <-- TABLE