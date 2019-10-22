using BL;
using BL.Service;
using BL.BuisnessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Text;
using System.Net.Mail;
using WebApplication1.Helpers;

namespace WebApplication1.Controllers
{
    public class UsersBooksController : Controller
    {
        IUserBookService userBookService;
        IUserService userService;
        IBookService bookService;
        public UsersBooksController(IUserBookService serv, IUserService serv2, IBookService serv3)
        {
            userBookService = serv;
            userService = serv2;
            bookService = serv3;
            Log4Net.InitLogger();
        }

        public ActionResult Index()
        {
            List<AuthorBook> usersBooks = AutoMapper<IEnumerable<UsersBooksBM>, List<AuthorBook>>.Map(userBookService.GetUsersBooks);
            foreach(var item in usersBooks)
            {
                item.BooksName = bookService.GetBook(item.BookId).Title;
            }
            return View(usersBooks);
        }

        public ActionResult CreateEdit(int? id = 0)
        {
            ViewBag.date = DateTime.Now.ToString();
            List<BookModel> books = AutoMapper<IEnumerable<BookBM>, List<BookModel>>.Map(bookService.GetBooks);
            List<UserModel> users = AutoMapper<IEnumerable<UserBM>, List<UserModel>>.Map(userService.GetUsers);

            if (id == null)
            {
                ViewBag.books = new SelectList(books, "Id", "Title");
                ViewBag.users = new SelectList(users, "Id", "Name");
                return View(new AuthorBook());
            }
            else
            {
                AuthorBook usersBooks = AutoMapper<UsersBooksBM, AuthorBook>.Map(userBookService.GetUserBook, (int)id);
                ViewBag.books = new SelectList(books, "Id", "Title", usersBooks.BookId);
                ViewBag.users = new SelectList(users, "Id", "Name", usersBooks.UserId);
                return View(usersBooks);
            }
        }

        [HttpPost]
        public ActionResult CreateEdit(AuthorBook usersBooks)
        {
            List<BookModel> books = AutoMapper<IEnumerable<BookBM>, List<BookModel>>.Map(bookService.GetBooks);
            List<UserModel> users = AutoMapper<IEnumerable<UserBM>, List<UserModel>>.Map(userService.GetUsers);

            usersBooks.BookId = int.Parse(Request.Form["BookId"]);
            usersBooks.UserId = int.Parse(Request.Form["UserId"]);

            if (usersBooks.IssueDate == null || usersBooks.IssueDate < DateTime.Now)
            {

                ViewBag.books = new SelectList(books, "Id", "Title", usersBooks.BookId);
                ViewBag.users = new SelectList(users, "Id", "Name", usersBooks.UserId);
                ViewBag.error = "Дата заказа не должна быть пустой и должна быть больше текущей даты";
                return View(usersBooks);
            }

            if (usersBooks.Time == null || DateTime.Compare(usersBooks.Time, new DateTime(1, 1, 1)) == 0)
            {
                ViewBag.books = new SelectList(books, "Id", "Title", usersBooks.BookId);
                ViewBag.users = new SelectList(users, "Id", "Name", usersBooks.UserId);
                ViewBag.error = "Укажите срок сдачи книги";
                return View(usersBooks);
            }

            if (usersBooks.ReturnDate == null)
                usersBooks.ReturnDate = new DateTime(1900, 1, 1);
            else
            {
                if (DateTime.Compare((DateTime)usersBooks.ReturnDate, new DateTime(1, 1, 1)) == 0)
                    usersBooks.ReturnDate = new DateTime(1900, 1, 1);
            }

            UsersBooksBM busersBooks = AutoMapper<AuthorBook, UsersBooksBM>.Map(usersBooks);

            if (userBookService.CheckUser(usersBooks.UserId))
            {
                userBookService.CreateOrUpdate(busersBooks);
                userBookService.Save();
                Log4Net.Log.Info("UserBook create/edit");
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Данный пользователь критический задолжник!!!!";
                ViewBag.BooksId = new SelectList(books, "Id", "Title", usersBooks.BookId);
                ViewBag.UserId = new SelectList(users, "Id", "Name", usersBooks.UserId);
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            userBookService.DeleteUserBook(id);
            userBookService.Save();
            Log4Net.Log.Info("UserBook delete");
            return RedirectToAction("Index");
        }

        public ActionResult Download()
        {
            List<AuthorBook> dolj = AutoMapper<IEnumerable<UsersBooksBM>, List<AuthorBook>>.Map(userBookService.GetUsersBooks).Where(i => i.IssueDate < DateTime.Now).ToList();

            StringBuilder sb = new StringBuilder();
            string header = "#\tUser\tAuthor\tBook\tReturn";
            sb.Append(header);
            sb.Append("\r\n\r\n");
            sb.Append('-', header.Length * 2);
            sb.Append("\r\n\r\n");
            foreach (var item in dolj)
            {
                sb.Append((dolj.IndexOf(item) + 1) + "\t" + item.UserName + "\t" + item.AuthorName + "\t" + item.BooksName + "\t" + item.IssueDate.Date + "\r\n");
            }
            byte[] data = Encoding.ASCII.GetBytes(sb.ToString());

            string contentType = "text/plain";
            Log4Net.Log.Info("Download UsersBooks file");
            return File(data, contentType, "users.txt");
        }

        public ActionResult Link(int id)
        {
            UserBM user = userService.GetUser(id);
            MailAddress from = new MailAddress("@mail.ru", "RETURN MY BOOK!!!");
            // кому отправляем
            MailAddress to = new MailAddress(user.Email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "RETURN MY BOOK!!!";
            // текст письма - включаем в него ссылку
            m.Body = string.Format(user.Name + " верни книгу!!!!!");
            m.IsBodyHtml = true;
            // адрес smtp-сервера, с которого мы и будем отправлять письмо
            SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.ru", 587);
            // логин и пароль
            smtp.Credentials = new System.Net.NetworkCredential("@mail.ru", "*");
            smtp.EnableSsl = true;
            smtp.Send(m);
            return RedirectToAction("Index");
        }

        public ActionResult ReturnBook(int id)
        {
            //AuthorBook usersBooks = AutoMapper<UsersBooksBM, AuthorBook>.Map(userBookService.GetUserBook, (int)id);
            //usersBooks.ReturnDate = DateTime.Now;
            //UsersBooksBM busersBooks = AutoMapper<AuthorBook, UsersBooksBM>.Map(usersBooks);
            //userBookService.CreateOrUpdate(busersBooks);
            UsersBooksBM busersBooks = userBookService.GetUserBook(id);
            busersBooks.ReturnDate = DateTime.Now;
            userBookService.CreateOrUpdate(busersBooks);
            userBookService.Save();
            return RedirectToAction("Index", "UsersBooks");
        }
        
    }
}