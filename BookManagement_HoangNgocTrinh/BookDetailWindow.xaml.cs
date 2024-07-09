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
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(BookCategoryIdComboBox.SelectedValue.ToString());
            //ta new 1 sách trống trơn, set các prop từ màn hình nhập
            Book x = new Book();
            x.BookId = int.Parse(BookIdTextBox.Text);
            x.BookName = BookNameTextBox.Text;
            x.Description = DescriptionTextBox.Text;
            x.PublicationDate = DateTime.Now;
            x.Quantity = int.Parse(QuantityTextBox.Text);
            x.Price = double.Parse(PriceTextBox.Text);
            x.Author = AuthorTextBox.Text;
            x.BookCategoryId = int.Parse(BookCategoryIdComboBox.SelectedIndex.ToString());

            //quan trọng & hồi hộp 
            _service.CreateBook(x);
            //về màn hình chính F5 grid

            this.Close(); //nhấn nút save, save xong, thì phải tắt cửa sổ
            //thừa kế hàm Close() từ class Cha Window
        }
    }
}
