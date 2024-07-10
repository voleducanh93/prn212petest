using BookManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DAL.Repositories
{
    public class BookRepository
    {
        //class này đóng vai BookCabinet đã học
        //nó có các hàm CUD table Book
        //nhưng mình k cần viết câu SQL, mà chỉ đi gọi Đại sứ DB chính là object từ class DBContext
        //				dbContext cx là 1 dạng Cabinet but chơi thẳng với dtb đó bên trong nó có chuỗi kết nối CSDL
        // class BookRepo chính là Cabinet but chỉ chơi 1 table Book 
        // cài cho nguyên lí S trong SQLID
        //class này phải khai báo biến DbContext 
        //và có các hàm CRUD table Book
        //vì chơi trực tiếp table -> thuộc level, layer DAL Data Access Layer


        private BookManagementDbContext _context; //k new, lúc nào thao tác table mới new
                // 	----- List<Book> _arr = new();
                //			 _arr.Add(new Book(){})


        //CRUD method với table Book
        //tên hàm ngắn gon, mang ý nghĩa của CRUD table
        public List<Book> GetAll()
        {
            _context = new BookManagementDbContext();
            //return _context.Books.ToList(); //Book là DbSet, to hơn List, convert về List
            // ~ SELECT * FROM Book - lazy loading 0 nghĩa là
            //select từ cuốn sách trong table, new Book() và add vào List<Book> Books
            //NHƯNG LƯỜI K JOIN, K CÓ NEW CATEGORY BÊN TRONG CUỐN SÁCH, ĐỂ ĐẢM BẢO PERFORMANCE
            //NẾU TA MUỐN LẤY LUN INFO CATE TA PHẢI CHỦ ĐỘNG BÁO JOIN
            //TOÁN TỬ INCLUDE<BIẾN TRỎ ĐẾN TABLE CẦN JOIN>
            //                BIẾN NÀY GỌI LÀ: NAVIGATION PATH - ĐƯỜNG GIÚP EM SANG TABLE KHÁC

            return _context.Books.Include("BookCategory").ToList();
            //đã join sang table BookCategory qua biến BookCategory { get; set; }
        }

        //hàm tạo mới cuốn sách
        public void Create(Book x)
        {
            //new DbContext() rồi mới dùng
            _context = new();
            _context.Books.Add(x);
            _context.SaveChanges();
        }//đã xong insert sách mới, new Book từ GUI đưa xuống, vì từ GUI ta có màn hình nhập mới cuốn sách, pass từ Service
        //TƯƠNG TỰ TA CÓ HÀM Update(Book x), Delete(Book x), Delete(int BookId)
        //remove 1 object từ List, hoặc remove theo PK where Id

        public void Update(Book x)
        {
            _context = new();
            _context.Books.Update(x);
            _context.SaveChanges();

        }
        public void Delete(Book x)
        {
            _context = new();
            _context.Books.Remove(x);
            _context.SaveChanges();
        }
    }
}
//MÔ HÌNH CHẠY SỬ DỤNG CLASS
//GUI WPF -> SERVICE (BLL) -> REPO (DAL) -> DBCONTEXT --> TABLE
//GUI WPF <- SERVICE (BLL) <- REPO (DAL) <- DBCONTEXT <-- TABLE
