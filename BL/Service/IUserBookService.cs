using BL.BuisnessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface IUserBookService
    {
        void CreateOrUpdate(UsersBooksBM userBook);
        UsersBooksBM GetUserBook(int id);
        IEnumerable<UsersBooksBM> GetUsersBooks();
        void DeleteUserBook(int id);
        bool CheckUser(int id);
        void Save();
        void Dispose();
    }
}
