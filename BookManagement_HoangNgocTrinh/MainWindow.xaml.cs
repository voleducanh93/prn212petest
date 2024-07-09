using BookManagement.BLL.Services;
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
    }
}