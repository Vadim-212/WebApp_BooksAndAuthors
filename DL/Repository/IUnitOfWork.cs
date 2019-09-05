using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Entity;

namespace DL.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Authors> Authors { get; }
        IRepository<Books> Books { get; }
        IRepository<Users> Users { get; }
        IRepository<UsersBooks> UsersBooks { get; }
        void Save();
    }
}
