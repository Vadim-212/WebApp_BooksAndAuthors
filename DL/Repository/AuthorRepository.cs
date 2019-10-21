using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Entity;
using System.Data.Entity;

namespace DL.Repository
{
    public class AuthorRepository: IRepository<Authors>
    {
        private Library db;

        public AuthorRepository(Library db)
        {
            this.db = db;
        }

        public void Create(Authors item)
        {
            db.Authors.Add(item);
        }

        public void Delete(int id)
        {
            Authors author = db.Authors.Find(id);
            if (author != null)
                db.Authors.Remove(author);
        }

        public IEnumerable<Authors> Find(Func<Authors, bool> predicate)
        {
            IEnumerable<Authors> authors = db.Authors.Where(predicate);
            return authors;
        }

        public Authors Get(int id)
        {
            Authors author = db.Authors.Find(id);
            return author;
        }

        public IEnumerable<Authors> GetAll()
        {
            List<Authors> authors = db.Authors.OrderBy(x => x.FirstName).ToList();
            return authors;
        }

        public void Update(Authors item)
        {
            //db.Entry(item).State = EntityState.Modified;
            db.Authors.Where(x => x.Id == item.Id).FirstOrDefault().FirstName = item.FirstName;
            db.Authors.Where(x => x.Id == item.Id).FirstOrDefault().LastName = item.LastName;
        }
    }
}
