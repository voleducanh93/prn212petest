using BookManagement.BLL.Services;
using BookManagement.DAL.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookManagement_HoangNgocTrinh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BookService _service = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadBookButton_Click(object sender, RoutedEventArgs e)
        {
            BookListDataGrid.ItemsSource = _service.GetAllBooks();
        }

        private void BookListDataGird_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();//remove app khỏi ram
        }

        private void BookMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //đây là cái hàm sẽ tự đc gọi khi cửa sở này mở lên - EVENT XUẤT HIỆN THÌ GỌI HÀM GÌ - LẬP TRÌNH SỰ KIỆN - ONCLICK() TRONG XỬ LÍ NÚT BẤM BÊN HTML
            //ta sẽ fill vào data grid. Tuy nhiên ta sẽ tách câu lệnh fill vào grid 1 hàm riêng cho code dễ đọc
            //vì việc fill vào grid sẽ xuất hiện nhiều lần:
            //1. mở màn hình lên, fill grid trước tên
            //2. nhấn nút tạo mới cuốn sách, mở màn hình tạo mới, tạo mới xong trở lại màn hình ta phải F5 LẠI GRID THÌ MỚI CÓ CUỐN SÁCH MỚI DC THÊM
            //3. SỬA 1 CUỐN SÁCH, F5 LẠI GRID
            //4. XÓA 1 CUỐN SÁCH, F5 LẠI GRID
            //HÀM NÀY DC DÙNG Ở NHIỀU NƠI, GỌI LÀ HÀM HELPER, PRIVATE LÀ ĐỦ
            LoadDataToGrid();
        }

        //hàm helper
        private void LoadDataToGrid()
        {
            BookListDataGrid.ItemsSource = null; //xóa lưới vì đề phòng có data trước đó
            BookListDataGrid.ItemsSource = _service.GetAllBooks();   //tải lại data
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BookDetailWindow detail = new();
            //render
            detail.ShowDialog();
            //f5 grid
            LoadDataToGrid();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            //lấy cuốn sách user chọn từ cái lưới để đẩy sang màn hình details
            //không lấy từ table, phí do toàn bộ sách đã bỏ vào ram đang nằm trong Grid
            // Book selectd = (Book)BookListDataGrid.SelectedItem;
            //này sẽ bị exception nếu ép không được, ví dụ không chọn 1 dòng nào, thì sao mà ép kiểu,
            //bắt exception, hoặc dùng chiêu khác
            Book selected = BookListDataGrid.SelectedItem as Book; //as là toàn tử mới, ép thử về trái thành vế phải, 
                                                                    //ráng ép selectedItem thành Book
                                                                    //nếu ép không thành công, thì gán null thay vì tung ra Exception
                                                                    
            if (selected == null) 
            {
                //chửi nếu mún edit mà không chọn sahcs để edit
                MessageBox.Show("Please select a Book before editing", "Choose one", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //xử lí có cuốn sách update
            //MessageBox.Show(selected.BookName);
            //tìm cách đẩy, chuyể selected sang bên màn hình detail
            //2 chàng 1 nàng, ko đi xuống Db lấy lại, có trong ram rồi thì cùng trỏ
            BookDetailWindow detail = new();
            //chốt quan trọng, khác phần create, ta phải đẩy cuốn sách selected sang bên kia

            detail.EditedBook = selected;

            detail.ShowDialog(); //render show tên
            //f5 lại Grid
            LoadDataToGrid();
                    
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Book selected = BookListDataGrid.SelectedItem as Book;
            if (selected == null)
            {
                //chửi nếu mún xóa mà không chọn sách để xóa
                MessageBox.Show("Please select a Book before editing", "Choose one", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //thiếu đoạn code are you sure, bị trừ 1 điểm
            MessageBoxResult answer = MessageBox.Show("Do you really want to delete the selected book?", "Deleted confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
                return;

            //sure Nếu xóa rồi !!!!
            _service.DeleteBook(selected);
            //f5 lai grid
        }
    }
}