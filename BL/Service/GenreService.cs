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
    public class GenreService: IGenreService
    {
        IUnitOfWork Db { get; set; }

        public GenreService(IUnitOfWork uow)
        {
            Db = uow;
        }
        public void CreateOrUpdate(GenreBM genre)
        {
            if (genre.Id == 0)
            {

                Genre dgenre = new Genre() { Name = genre.Name };
                Db.Genre.Create(dgenre);
            }
            else
            {
                Genre editGenre = AutoMapper<GenreBM, Genre>.Map(genre);
                Db.Genre.Update(editGenre);
            }
            Db.Save();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public GenreBM GetGenre(int id)
        {
            if (id != 0)
            {
                return AutoMapper<Genre, GenreBM>.Map(Db.Genre.Get, (int)id);
            }
            return new GenreBM();
        }

        public IEnumerable<GenreBM> GetGenres()
        {
            return AutoMapper<IEnumerable<Genre>, List<GenreBM>>.Map(Db.Genre.GetAll);
        }

        public void DeleteGenre(int id)
        {
            Db.Genre.Delete(id);
            Db.Save();
        }
        public void Save()
        {
            Db.Save();
        }
    }
}
