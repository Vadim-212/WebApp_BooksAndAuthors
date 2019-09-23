using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using DL.Entity;

namespace DL.Repository
{
    public class BookRepository : IRepository<Books>
    {
        private Library db;

        public BookRepository(Library db)
        {
            this.db = db;
        }
        public void Create(Books item)
        {
            db.Books.Add(item);
        }

        public void Delete(int id)
        {
            Books book = db.Books.Find(id);
            if (book != null)
                db.Books.Remove(book);
        }

        public IEnumerable<Books> Find(Func<Books, bool> predicate)
        {
            IEnumerable<Books> books = db.Books.Where(predicate);
            return books;
        }

        public Books Get(int id)
        {
            Books book = db.Books.Find(id);
            return book;
        }

        public IEnumerable<Books> GetAll()
        {
            return db.Books.ToList();
        }

        public void Update(Books item)
        {
            //db.Entry(item).State = EntityState.Modified;
            db.Books.Where(x => x.Id == item.Id).FirstOrDefault().AuthorId = item.AuthorId;
            db.Books.Where(x => x.Id == item.Id).FirstOrDefault().Title = item.Title;
            db.Books.Where(x => x.Id == item.Id).FirstOrDefault().Pages = item.Pages;
            db.Books.Where(x => x.Id == item.Id).FirstOrDefault().Price = item.Price;
        }
    }
}
