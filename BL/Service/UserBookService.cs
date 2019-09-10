using BL.BuisnessModel;
using DL.Entity;
using DL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class UserBookService: IUserBookService
    {
            IUnitOfWork Database { get; set; }

            public UserBookService(IUnitOfWork uow)
            {
                Database = uow;
            }

            public void CreateOrUpdate(UsersBooksBM userBook)
            {
                if (userBook.Id == 0)
                {

                    UsersBooks duserBook = new UsersBooks() { BookId = userBook.BookId, UserId = userBook.UserId, IssueDate = userBook.IssueDate };
                    Database.UsersBooks.Create(duserBook);
                }
                else
                {
                    UsersBooks editUserBook = AutoMapper<UsersBooksBM, UsersBooks>.Map(userBook);
                    Database.UsersBooks.Update(editUserBook);
                }
                Database.Save();
            }

            public void Dispose()
            {
                Database.Dispose();
            }

            public UsersBooksBM GetUserBook(int id)
            {
                if (id != 0)
                {
                UsersBooksBM buserBook = AutoMapper<UsersBooks, UsersBooksBM>.Map(Database.UsersBooks.Get, (int)id);
                    buserBook.AuthorId = Database.Books.Get(buserBook.BookId).AuthorId;
                    buserBook.AuthorName = Database.Authors.Get(buserBook.AuthorId).FirstName;
                    buserBook.BookName = Database.Books.Get(buserBook.BookId).Title;
                    buserBook.UserName = Database.Users.Get(buserBook.UserId).Name;
                    return buserBook;
                }
                return new UsersBooksBM();
            }

            public IEnumerable<UsersBooksBM> GetUsersBooks()
            {
                List<UsersBooksBM> buserBook = AutoMapper<IEnumerable<UsersBooks>, List<UsersBooksBM>>.Map(Database.UsersBooks.GetAll);
                for (int i = 0; i < buserBook.Count; i++)
                {
                    buserBook[i] = GetUserBook(buserBook[i].Id);
                }
                return (IEnumerable<UsersBooksBM>)buserBook;
            }

            public bool CheckUser(int id)
            {
                UsersBooks usersBooks = Database.UsersBooks.Find(i => i.UserId == id && i.IssueDate <= DateTime.Now).FirstOrDefault();
                return (usersBooks == null) ? true : false;
            }

            public void DeleteUserBook(int id)
            {
                Database.UsersBooks.Delete(id);
                Database.Save();
            }
    }
}
