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
    public class UserService : IUserService
    {
        IUnitOfWork Db { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Db = uow;
        }
        public void CreateOrUpdate(UserBM user)
        {
            if (user.Id == 0)
            {

                Users duser = new Users() { Name = user.Name, Email = user.Email };
                Db.Users.Create(duser);
            }
            else
            {
                Users editUser = AutoMapper<UserBM, Users>.Map(user);
                Db.Users.Update(editUser);
            }
            Db.Save();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public UserBM GetUser(int id)
        {
            if (id != 0)
            {
                return AutoMapper<Users, UserBM>.Map(Db.Users.Get, (int)id);
            }
            return new UserBM();
        }

        public List<UsersBooksBM> GetReturnBooks(int id)
        {
            List<UsersBooks> ub = Db.UsersBooks.Find(i => i.UserId == id).ToList();
            List<UsersBooksBM> bub = new List<UsersBooksBM>();

            foreach (var item in ub)
            {
                Books book = Db.Books.Get(item.BookId);
                Authors author = Db.Authors.Get(book.AuthorId);
                UsersBooksBM userBook = new UsersBooksBM() { Id = item.Id, AuthorId = author.Id, AuthorName = author.FirstName, BookId = book.Id, BookName = book.Title, UserId = item.UserId, UserName = Db.Users.Get(item.UserId).Name, IssueDate = item.IssueDate };
                bub.Add(userBook);
            }

            return bub;
        }

        public IEnumerable<UserBM> GetUsers()
        {
            return AutoMapper<IEnumerable<Users>, List<UserBM>>.Map(Db.Users.GetAll);
        }

        public void DeleteUser(int id)
        {
            Db.Users.Delete(id);
            Db.Save();
        }
    }
}
