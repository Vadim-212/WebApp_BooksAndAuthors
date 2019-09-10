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
    public class AuthorController : Controller
    {
        IAuthorService authorService;
        public AuthorController(IAuthorService serv)
        {
            authorService = serv;
        }
        public ActionResult Index()
        {
            return View(AutoMapper<IEnumerable<AuthorBM>, List<AuthorModel>>.Map(authorService.GetAuthors));
        }

        public ActionResult CreateEdit(int? id = 0)
        {
            AuthorModel author = AutoMapper<AuthorBM, AuthorModel>.Map(authorService.GetAuthor,(int)id);
            return View(author);

        }

        [HttpPost]
        public ActionResult CreateEdit(AuthorModel author)
        {
            AuthorBM oldAuthor = AutoMapper<AuthorModel, AuthorBM>.Map(author);
            authorService.CreateOrUpdate(oldAuthor);
            return RedirectToActionPermanent("Index", "Author");
        }

        public ActionResult Delete(int id)
        {
            authorService.DeleteAuthor(id);
            return RedirectToAction("Index", "Author");
        }
    }
}