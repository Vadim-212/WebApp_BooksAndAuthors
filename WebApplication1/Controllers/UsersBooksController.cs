using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity;

namespace WebApplication1.Controllers
{
    public class UsersBooksController : Controller
    {
        // GET: UsersBooks
        public ActionResult Index()
        {
            List<UsersWithBooks> usersWithBooks = new List<UsersWithBooks>();
            using (Library db = new Library())
            {
                var usersBooks = db.UsersBooks.ToList();
                foreach(var item in usersBooks)
                {
                    usersWithBooks.Add(new UsersWithBooks() { Id = item.Id, User = db.Users.Where(x => x.Id == item.UserId).FirstOrDefault().Name, UserId = db.Users.Where(x => x.Id == item.UserId).FirstOrDefault().Id, Book = db.Authors.Where(x => x.Id == db.Books.Where(y=>y.Id==item.BookId).FirstOrDefault().AuthorId).FirstOrDefault().FirstName + " " + db.Authors.Where(x => x.Id == db.Books.Where(y => y.Id == item.BookId).FirstOrDefault().AuthorId).FirstOrDefault().LastName + " \"" + db.Books.Where(x=>x.Id==item.BookId).FirstOrDefault().Title + "\"", BookId = db.Books.Where(x => x.Id == item.BookId).FirstOrDefault().Id });
                }
            }
            return View(usersWithBooks);
        }

        public ActionResult Create()
        {
            using (Library db = new Library())
            {
                List<SelectListItem> sliUsers = new List<SelectListItem>();
                foreach(var item in db.Users.ToList())
                {
                    sliUsers.Add(new SelectListItem()
                    {
                        Selected = false,
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });
                }
                sliUsers[0].Selected = true;
                ViewBag.usersList = sliUsers;

                List<SelectListItem> sliBooks = new List<SelectListItem>();
                foreach(var item in db.Books.ToList())
                {
                    sliBooks.Add(new SelectListItem()
                    {
                        Selected = false,
                        Text = db.Authors.Where(x => x.Id == item.AuthorId).FirstOrDefault().FirstName + " " + db.Authors.Where(x => x.Id == item.AuthorId).FirstOrDefault().LastName + " \"" + item.Title + "\"",
                        Value = item.Id.ToString()
                    });
                }
                sliBooks[0].Selected = true;
                ViewBag.booksList = sliBooks;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(UsersWithBooks usersWithBooks)
        {
            UsersBooks userBooks;
            using (Library db = new Library())
            {
                userBooks = new UsersBooks()
                {
                    UserId = int.Parse(Request.Form["usersList"]),
                    BookId = int.Parse(Request.Form["booksList"])
                };
                db.UsersBooks.Add(userBooks);
                db.SaveChanges();
            }
            return Redirect("Index");
        }

        public ActionResult Edit(int id)
        {
            UsersBooks ub;
            using (Library db = new Library())
            {
                ub = db.UsersBooks.Where(x => x.Id == id).FirstOrDefault();

                List<SelectListItem> sliUsers = new List<SelectListItem>();
                foreach (var item in db.Users.ToList())
                {
                    sliUsers.Add(new SelectListItem()
                    {
                        Selected = false,
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });
                }
                sliUsers.Where(x => x.Value == ub.UserId.ToString()).FirstOrDefault().Selected = true;
                ViewBag.usersList = sliUsers;

                List<SelectListItem> sliBooks = new List<SelectListItem>();
                foreach (var item in db.Books.ToList())
                {
                    sliBooks.Add(new SelectListItem()
                    {
                        Selected = false,
                        Text = db.Authors.Where(x => x.Id == item.AuthorId).FirstOrDefault().FirstName + " " + db.Authors.Where(x => x.Id == item.AuthorId).FirstOrDefault().LastName + " \"" + item.Title + "\"",
                        Value = item.Id.ToString()
                    });
                }
                sliBooks.Where(x => x.Value == ub.BookId.ToString()).FirstOrDefault().Selected = true;
                ViewBag.booksList = sliBooks;
            }
            return View(ub);
        }

        [HttpPost]
        public ActionResult Edit(UsersBooks usersBooks)
        {
            UsersWithBooks uwb;
            using (Library db = new Library())
            {
                db.UsersBooks.Where(x => x.Id == usersBooks.Id).FirstOrDefault().UserId = int.Parse(Request.Form["usersList"]);
                db.UsersBooks.Where(x => x.Id == usersBooks.Id).FirstOrDefault().BookId = int.Parse(Request.Form["booksList"]);
                db.SaveChanges();
            }
            return Redirect("~/UsersBooks/Index");
        }

        public ActionResult Delete(int id)
        {
            using (Library db = new Library())
            {
                db.UsersBooks.Remove(db.UsersBooks.Where(x => x.Id == id).FirstOrDefault());
                db.SaveChanges();
            }
            return Redirect("~/UsersBooks/Index");
        }
    }
}