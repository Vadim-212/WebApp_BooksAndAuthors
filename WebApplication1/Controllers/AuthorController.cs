using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using DL.Entity;

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
                //var expensiveBooks = db.Books.OrderByDescending(x => x.Price).Join(db.Authors, b => b.AuthorId, a => a.Id, (a, b) => new Authors { Id = b.Id, FirstName = b.FirstName, LastName = b.LastName }).ToList();
                var expensiveBooks = db.Books.OrderByDescending(x => x.Price).Select(b => b.AuthorId).Take(5).ToList();
                //expensiveBooks.ForEach(x => db.Authors.Where(y => y.Id == x));
                //ViewBag.AuthorsList = expensiveBooks;
                List<Authors> a = new List<Authors>();
                //foreach(var item in expensiveBooks)
                //{
                //    a.Add(db.Authors.Where(x => x.Id == item).FirstOrDefault());
                //}
                expensiveBooks.ForEach(x => { a.Add(db.Authors.Where(y => y.Id == x).FirstOrDefault()); });
                ViewBag.AuthorsList = a;

                List<HelpClass> o = new List<HelpClass>();
                //var expensiveBooks2 = db.Database.SqlQuery<object>("SELECT TOP 5 a.Id, SUM(Price) as PricesSum FROM Books b JOIN Authors a ON a.Id = b.AuthorId GROUP BY a.Id ORDER BY PricesSum DESC",new SqlParameter());
                //expensiveBooks2.ForEach(x => { o.Add(new { Id=x.Id,Name=db.Authors.Where(y=>y.Id==x.Id).FirstOrDefault(),PricesSum=x.PricesSum }) })
                var eb = (from b in db.Books
                          join au in db.Authors on b.AuthorId equals au.Id
                          select new { b.Price, au.Id, }).GroupBy(p => p.Id).ToList();
                //eb.ForEach(x => { o.Add(new HelpClass() { Id = x.Key,Price=x}) })

                int n = 1;
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

        public ActionResult CreateEdit(int id)
        {
            if (id == -1)
                return View();
            Authors author;
            using (Library db = new Library())
            {
                author = db.Authors.Where(x => x.Id == id).FirstOrDefault();
            }
            return View(author);
        }
        [HttpPost]
        public ActionResult CreateEdit(Authors author)
        {
            using (Library db = new Library())
            {
                try
                {
                    var a = db.Authors.Where(x => x.Id == author.Id).First();
                    db.Entry(author).State = System.Data.Entity.EntityState.Modified;
                }
                catch
                {
                    db.Authors.Add(author);
                }
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

        public ActionResult TopAuthorsPartialView()
        {
            return PartialView();
        }
    }
}