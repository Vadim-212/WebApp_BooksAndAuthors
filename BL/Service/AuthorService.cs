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
    public class AuthorService : IAuthorService
    {
        IUnitOfWork Db { get; set; }

        public AuthorService(IUnitOfWork uow)
        {
            Db = uow;
        }
        public void CreateOrUpdate(AuthorBM author)
        {
            if (author.Id == 0)
            {
                Authors newAuthor = new Authors() { FirstName = author.FirstName, LastName = author.LastName };
                Db.Authors.Create(newAuthor);
            }
            else
            {
                Authors newAuthor = AutoMapper<AuthorBM, Authors>.Map(author);
                Db.Authors.Update(newAuthor);
            }
        }

        public void DeleteAuthor(int id)
        {
            Db.Authors.Delete(id);
        }

        public void Save()
        {
            Db.Save();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public AuthorBM GetAuthor(int id)
        {
            if(id != 0)
            {
                return AutoMapper<Authors, AuthorBM>.Map(Db.Authors.Get, (int)id);
            }
            return new AuthorBM();
        }

        public IEnumerable<AuthorBM> GetAuthors()
        {
            return AutoMapper< IEnumerable<Authors>, List < AuthorBM >>.Map(Db.Authors.GetAll);
        }
    }
}
