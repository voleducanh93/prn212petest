using System;
using System.Collections.Generic;

namespace BookManagement.DAL.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string BookName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime PublicationDate { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public string Author { get; set; } = null!;

    public int BookCategoryId { get; set; }

    public virtual BookCategory BookCategory { get; set; } = null!;
    //vietsub:                  biến object trỏ vào table/class BookCategory bên tay trái
    //             Class/Table
    //             Category (chứa cate id mà cuốn sách thuộc về
    //                              chứa cả GenreType, Desc_
    //                            biến này dc gọi là Navigation Path
    //                            nhờ biến này giúp ta join bằng móc sang table Category/class Cate
    //đưa tên biến vào trong câu Join khi sờ table Book, sờ List<Book> Books trong DbContext
}
