using BookManagement.BLL.Services;
using BookManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookManagement_HoangNgocTrinh
{
    /// <summary>
    /// Interaction logic for BookDetailWindow.xaml
    /// </summary>
    public partial class BookDetailWindow : Window
    {

        //tui cần 2 service
        private BookService _service = new();   //lưu sách
        private CategoryService _categoryService = new();   //danh sách Cate

        //ta bổ sung thêm 1 prop viết theo style full cũng được hoặc là style 
        //auto-generate cũng được
        public Book EditedBook { get; set; } = null; 
        //                                      món mới; ~_edited = null;
        //int yob = 2004; -> set() tên biến kèm dấu bằng thì là set
        //cw(yob) sout(yob) tên biến mình ên thi là Get()


        public BookDetailWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //đổ vào combobox 5 dòng Category - chọn Self Help nhưng cần số 5 FK
            BookCategoryIdComboBox.ItemsSource = _categoryService.GetAllCategories();

            BookCategoryIdComboBox.DisplayMemberPath = "BookGenreType"; //cột mún showw
            BookCategoryIdComboBox.SelectedValuePath = "BookCategoryId"; //cần value vào FK của table Book



            BookCategoryIdComboBox.SelectedValue = 1;



            //kieemr tra xem manf hinhf nayf owr mode naof : new/create hay edit/update
            //biến/prop EditedBook chính là flag cờ dùng check status/mode của màn hình này. Nếu nó ==null -> tạo mới sách
            //                                                                               nếu nó != null ->ai đó bên ngoài đẩy cho nó cuốn sách (bên Main đấy) thì là => edit mode
            BookModeLabel.Content = "Create new book";

            if (EditedBook != null)
            {
                //Đổi label BookMode
                BookModeLabel.Content = "Chỉnh sửa thông tin sách";
                

                //để data vào các text, lịch, dropdown
                //Lưu ý: Disable ô BOOK ID, KO cho Edit PK - Toang hết tất cả FK Nếu có - hoặc chơi trò testcase
                BookIdTextBox.Text = EditedBook.BookId.ToString();
                //chuỗi  = số mã sách là kiểu số cần convert về chuỗi 
                BookIdTextBox.IsEnabled = false; //cấm edit ID cuốn sách cũ

                BookNameTextBox.Text = EditedBook.BookName;
             
                DescriptionTextBox.Text = EditedBook.Description;
                PublicationDateDatePicker.Text = EditedBook.PublicationDate.ToString();
                //đổi ngày tháng về chuỗi
                QuantityTextBox.Text = EditedBook.Quantity.ToString();
                PriceTextBox.Text = EditedBook.Price.ToString();
                AuthorTextBox.Text = EditedBook.Author;
                //jump đến đúng cate cuốn sách đang có 
                BookCategoryIdComboBox.SelectedValue = EditedBook.BookCategoryId;
                //thực ra cái comboBox này đã đỏ sẵn 5 loại sách đoạn code trước if
                //chỉ cần chờ ai đó select, ta chủ động select ứng với cate của cuốn sách đang edit;

            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //chửi trước khi save
            //có thể tách riêng hàm validate()
            //if(validate() thì gọi đoạn code dưới
            //làm biếng thì viết ngay đây
            int quantity = int.Parse(QuantityTextBox.Text);
            if(quantity <= 0 || quantity > 1000)
            {
                //chửi và thoát
                MessageBox.Show("The quantity must be between 1...1000");
                return;
            }    


            //MessageBox.Show(BookCategoryIdComboBox.SelectedValue.ToString());
            //ta new 1 sách trống trơn, set các prop từ màn hình nhập
            Book x = new Book();
            x.BookId = int.Parse(BookIdTextBox.Text); 
            x.BookName = BookNameTextBox.Text;
            x.Description = DescriptionTextBox.Text;
            x.PublicationDate = (DateTime)PublicationDateDatePicker.SelectedDate;
            x.Quantity = int.Parse(QuantityTextBox.Text);
            x.Price = double.Parse(PriceTextBox.Text);
            x.Author = AuthorTextBox.Text;
            x.BookCategoryId = int.Parse(BookCategoryIdComboBox.SelectedIndex.ToString());
            //Kiểm tra mode của màn hình để gọi hàm Update() Create() tương ứng
            if(EditedBook == null)
         

            //quan trọng & hồi hộp 
            _service.CreateBook(x);
            //về màn hình chính F5 grid
            _service.UpdateBooK(x);

            this.Close(); //nhấn nút save, save xong, thì phải tắt cửa sổ
            //thừa kế hàm Close() từ class Cha Window
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //this.close();
            Close(); //gọi hàm của class cha window
        }
    }
}
