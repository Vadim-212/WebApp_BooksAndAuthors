using BL.BuisnessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface IGenreService
    {
        void CreateOrUpdate(GenreBM user);
        GenreBM GetGenre(int id);
        IEnumerable<GenreBM> GetGenres();
        void DeleteGenre(int id);
        void Save();
        void Dispose();
    }
}
