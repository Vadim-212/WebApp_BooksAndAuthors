using BL.BuisnessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface IUserService
    {
        void CreateOrUpdate(UserBM user);
        UserBM GetUser(int id);
        IEnumerable<UserBM> GetUsers();
        List<UsersBooksBM> GetReturnBooks(int id);
        void DeleteUser(int id);
        void Save();
        void Dispose();
    }
}
