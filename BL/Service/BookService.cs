using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BuisnessModel;
using DL.Repository;
using DL.Entity;

namespace BL.Service
{
    public class BookService : IBookService
    {
        IUnitOfWork Db { get; set; }

        public BookService(IUnitOfWork uow)
        {
            Db = uow;
        }
        public void CreateOrUpdate(BookBM book)
        {
            if (book.Id == 0)
            {
                Books newBook = new Books() { AuthorId = book.AuthorId, Title = book.Title, Pages = book.Pages, Price = book.Price };
                Db.Books.Create(newBook);
            }
            else
            {
                Books newBook = AutoMapper<BookBM, Books>.Map(book);
                Db.Books.Update(newBook);
            }
        }

        public void DeleteBook(int id)
        {
            Db.Books.Delete(id);
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public BookBM GetBook(int id)
        {
            if (id != 0)
            {
                return AutoMapper<Books, BookBM>.Map(Db.Books.Get, (int)id);
            }
            return new BookBM();
        }

        public IEnumerable<BookBM> GetBooks()
        {
            return AutoMapper<IEnumerable<Books>, List<BookBM>>.Map(Db.Books.GetAll);
        }

        public void Save()
        {
            Db.Save();
        }
    }
}
