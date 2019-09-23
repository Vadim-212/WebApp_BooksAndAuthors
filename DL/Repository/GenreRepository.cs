using DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Repository
{
    public class GenreRepository: IRepository<Genre>
    {
        private Library db;

        public GenreRepository(Library db)
        {
            this.db = db;
        }

        public void Create(Genre item)
        {
            db.Genre.Add(item);
        }

        public void Delete(int id)
        {
            Genre genre = db.Genre.Find(id);
            if (genre != null)
                db.Genre.Remove(genre);
        }

        public IEnumerable<Genre> Find(Func<Genre, bool> predicate)
        {
            IEnumerable<Genre> genres = db.Genre.Where(predicate);
            return genres;
        }

        public Genre Get(int id)
        {
            Genre genre = db.Genre.Find(id);
            return genre;
        }

        public IEnumerable<Genre> GetAll()
        {
            return db.Genre.ToList();
        }

        public void Update(Genre item)
        {
            //db.Entry(item).State = EntityState.Modified;
            db.Genre.Where(x => x.Id == item.Id).FirstOrDefault().Name = item.Name;
        }
    }
}
