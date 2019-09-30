using BL;
using BL.BuisnessModel;
using BL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Newtonsoft.Json;

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
            authorService.Save();
            return RedirectToActionPermanent("Index", "Author");
        }

        public ActionResult Delete(int id)
        {
            authorService.DeleteAuthor(id);
            authorService.Save();
            return RedirectToAction("Index", "Author");
        }
        public ActionResult GetAuthors()
        {
            List<AuthorModel> authors = AutoMapper<IEnumerable<AuthorBM>, List<AuthorModel>>.Map(authorService.GetAuthors);
            return Json(authors, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAuthor(int id)
        {
            AuthorModel author = AutoMapper<AuthorBM, AuthorModel>.Map(authorService.GetAuthor, id);
            return Json(author, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CreateEditAjax(string obj)
        {
            AuthorModel author = JsonConvert.DeserializeObject<AuthorModel>(obj);
            AuthorBM oldAuthor = AutoMapper<AuthorModel, AuthorBM>.Map(author);
            authorService.CreateOrUpdate(oldAuthor);
            authorService.Save();
            return RedirectToActionPermanent("Index", "Author");
        }
    }
}