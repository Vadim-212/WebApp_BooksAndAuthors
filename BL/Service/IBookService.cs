using BL.BuisnessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface IBookService
    {
        void CreateOrUpdate(BookBM book);
        BookBM GetBook(int id);
        IEnumerable<BookBM> GetBooks();
        void DeleteBook(int id);
        void Dispose();
    }
}
