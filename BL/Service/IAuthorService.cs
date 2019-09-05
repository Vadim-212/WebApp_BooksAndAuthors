using BL.BuisnessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface IAuthorService
    {
        void CreateOrUpdate(AuthorBM author);
        AuthorBM GetAuthor(int id);
        IEnumerable<AuthorBM> GetAuthors();
        void DeleteAuthor(int id);
        void Dispose();
    }
}
