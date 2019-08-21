using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity;

namespace WebApplication1.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            List<Authors> authors;
            using (Library db = new Library())
            {
                authors = db.Authors.ToList();
            }
            return View(authors);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Authors author)
        {
            using (Library db = new Library())
            {
                db.Authors.Add(author);
                db.SaveChanges();
            }
            return Redirect("Index");
        }

        public ActionResult Edit(int id)
        {
            Authors author;
            using (Library db = new Library())
            {
                author = db.Authors.Where(x => x.Id == id).FirstOrDefault();

            }
            return View(author);
        }

        [HttpPost]
        public ActionResult Edit(Authors author)
        {
            using (Library db = new Library())
            {
                db.Authors.Where(x => x.Id == author.Id).FirstOrDefault().FirstName = author.FirstName;
                db.Authors.Where(x => x.Id == author.Id).FirstOrDefault().LastName = author.LastName;
                db.SaveChanges();
            }
            return Redirect("~/Author/Index");
        }

        public ActionResult Delete(int id)
        {
            using (Library db = new Library())
            {
                db.Authors.Remove(db.Authors.Where(x => x.Id == id).FirstOrDefault());
                db.SaveChanges();
            }
            return Redirect("~/Author/Index");
        }
    }
}