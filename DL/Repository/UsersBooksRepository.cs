using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using DL.Entity;

namespace DL.Repository
{
    public class UsersBooksRepository : IRepository<UsersBooks>
    {
        private Library db;

        public UsersBooksRepository(Library db)
        {
            this.db = db;
        }
        public void Create(UsersBooks item)
        {
            db.UsersBooks.Add(item);
        }

        public void Delete(int id)
        {
            UsersBooks userBooks = db.UsersBooks.Find(id);
            if (userBooks != null)
                db.UsersBooks.Remove(userBooks);
        }

        public IEnumerable<UsersBooks> Find(Func<UsersBooks, bool> predicate)
        {
            IEnumerable<UsersBooks> usersBooks = db.UsersBooks.Where(predicate);
            return usersBooks;
        }

        public UsersBooks Get(int id)
        {
            UsersBooks userBooks = db.UsersBooks.Find(id);
            return userBooks;
        }

        public IEnumerable<UsersBooks> GetAll()
        {
            return db.UsersBooks.ToList();
        }

        public void Update(UsersBooks item)
        {
            //db.Entry(item).State = EntityState.Modified;
            db.UsersBooks.Where(x => x.Id == item.Id).FirstOrDefault().BookId = item.BookId;
            db.UsersBooks.Where(x => x.Id == item.Id).FirstOrDefault().UserId = item.UserId;
            db.UsersBooks.Where(x => x.Id == item.Id).FirstOrDefault().IssueDate = item.IssueDate;
            db.UsersBooks.Where(x => x.Id == item.Id).FirstOrDefault().Time = item.Time;
            db.UsersBooks.Where(x => x.Id == item.Id).FirstOrDefault().ReturnDate = item.ReturnDate;
        }
    }
}
