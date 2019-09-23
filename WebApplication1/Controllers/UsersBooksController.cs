using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                    usersWithBooks.Add(new UsersWithBooks()
                    {
                        Id = item.Id,
                        User = db.Users.Where(x => x.Id == item.UserId).FirstOrDefault().Name,
                        UserId = db.Users.Where(x => x.Id == item.UserId).FirstOrDefault().Id,
                        Book = db.Authors.Where(x => x.Id == db.Books.Where(y=>y.Id==item.BookId).FirstOrDefault().AuthorId).FirstOrDefault().FirstName + " " + db.Authors.Where(x => x.Id == db.Books.Where(y => y.Id == item.BookId).FirstOrDefault().AuthorId).FirstOrDefault().LastName + " \"" + db.Books.Where(x=>x.Id==item.BookId).FirstOrDefault().Title + "\"",
                        BookId = db.Books.Where(x => x.Id == item.BookId).FirstOrDefault().Id,
                        IssueDate = item.IssueDate,
                        Time = item.Time,
                        ReturnDate = item.ReturnDate
                    });
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
                    BookId = int.Parse(Request.Form["booksList"]),
                    IssueDate = usersWithBooks.IssueDate,
                    Time = usersWithBooks.Time,
                    ReturnDate = (DateTime.Compare(usersWithBooks.ReturnDate,new DateTime(1,1,1,0,0,0)) == 0) ? new DateTime(1900,1,1,0,0,0) : usersWithBooks.ReturnDate
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
            using (Library db = new Library())
            {
                db.UsersBooks.Where(x => x.Id == usersBooks.Id).FirstOrDefault().UserId = int.Parse(Request.Form["usersList"]);
                db.UsersBooks.Where(x => x.Id == usersBooks.Id).FirstOrDefault().BookId = int.Parse(Request.Form["booksList"]);
                db.UsersBooks.Where(x => x.Id == usersBooks.Id).FirstOrDefault().IssueDate = usersBooks.IssueDate;
                db.UsersBooks.Where(x => x.Id == usersBooks.Id).FirstOrDefault().Time = usersBooks.Time;
                db.UsersBooks.Where(x => x.Id == usersBooks.Id).FirstOrDefault().ReturnDate = (DateTime.Compare(usersBooks.ReturnDate, new DateTime(1, 1, 1, 0, 0, 0)) == 0) ? new DateTime(1900, 1, 1, 0, 0, 0) : usersBooks.ReturnDate;
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

        public ActionResult ReturnBook(int id)
        {
            using (Library db = new Library())
            {
                db.UsersBooks.Where(x => x.Id == id).FirstOrDefault().ReturnDate = DateTime.Now;
                db.SaveChanges();
            }
            return Redirect("~/UsersBooks/Index");
        }

        public ActionResult SendNotification(int id)
        {
            UsersBooks usersBooks;
            Users user;
            Books book;
            Authors author;
            using (Library db = new Library())
            {
                usersBooks = db.UsersBooks.Where(x => x.Id == id).FirstOrDefault();
                user = db.Users.Where(x => x.Id == usersBooks.UserId).FirstOrDefault();
                book = db.Books.Where(x => x.Id == usersBooks.BookId).FirstOrDefault();
                author = db.Authors.Where(x => x.Id == book.AuthorId).FirstOrDefault();
            }
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            //MailAddress from = new MailAddress("-", "Имя приложения");
            //// кому отправляем
            //MailAddress to = new MailAddress(user.Email);
            //// создаем объект сообщения
            //MailMessage m = new MailMessage(from, to);
            //// тема письма
            //m.Subject = "Возврат киниги";
            //// текст письма
            //m.Body = $"<h2>Срок сдачи книги {author.FirstName} {author.LastName} \"{book.Title}\" истёк</h2><p>Просьба сдать книгу в ближайшее время</p>";
            //// письмо представляет код html
            //m.IsBodyHtml = true;
            //// адрес smtp-сервера и порт, с которого будем отправлять письмо
            //SmtpClient smtp = new SmtpClient("smtp.mail.ru", 465);
            //// логин и пароль
            //smtp.Credentials = new NetworkCredential("-", "");
            //smtp.EnableSsl = true;
            //smtp.Send(m);

            //var fromAddress = new MailAddress("@", "Имя приложения");
            //var toAddress = new MailAddress(user.Email, user.Name);
            //const string fromPassword = "-";
            //const string subject = "Возврат киниги";
            //string body = $"<h2>Срок сдачи книги {author.FirstName} {author.LastName} \"{book.Title}\" истёк</h2><p>Просьба сдать книгу в ближайшее время</p>";

            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.mail.ru",
            //    Port = 465,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            //};
            //using (var message = new MailMessage(fromAddress, toAddress)
            //{
            //    Subject = subject,
            //    Body = body
            //})
            //{
            //    smtp.Send(message);
            //}

            return Redirect("~/UsersBooks/Index");
        }
    }
}