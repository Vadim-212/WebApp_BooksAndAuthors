using BL;
using BL.BuisnessModel;
using BL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GenreController : Controller
    {
        IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }
        // GET: Genre
        public ActionResult Index()
        {
            List<GenreModel> genres = AutoMapper<IEnumerable<GenreBM>, List<GenreModel>>.Map(genreService.GetGenres);

            return View(genres);
        }
        public ActionResult CreateEdit(int? id = 0)
        {
            GenreModel genre = AutoMapper<GenreBM, GenreModel>.Map(genreService.GetGenre, (int)id);
            return View(genre);
        }

        [HttpPost]
        public ActionResult CreateEdit(GenreModel model)
        {
            GenreBM genre = AutoMapper<GenreModel, GenreBM>.Map(model);
            genreService.CreateOrUpdate(genre);
            genreService.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            genreService.DeleteGenre(id);
            genreService.Save();
            return RedirectToAction("Index");
        }
    }
}