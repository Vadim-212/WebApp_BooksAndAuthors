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
        }

        public ActionResult Index()
        {
            return View(AutoMapper<IEnumerable<UsersBooksBM>, List<AuthorBook>>.Map(userBookService.GetUsersBooks));
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
                ViewBag.books = new SelectList(books, "Id", "Title", usersBooks.BooksId);
                ViewBag.users = new SelectList(users, "Id", "Name", usersBooks.UserId);
                return View(usersBooks);
            }
        }

        [HttpPost]
        public ActionResult CreateEdit(AuthorBook usersBooks)
        {
            List<BookModel> books = AutoMapper<IEnumerable<BookBM>, List<BookModel>>.Map(bookService.GetBooks);
            List<UserModel> users = AutoMapper<IEnumerable<UserBM>, List<UserModel>>.Map(userService.GetUsers);

            if (usersBooks.IssueDate == null || usersBooks.IssueDate < DateTime.Now)
            {

                ViewBag.books = new SelectList(books, "Id", "Title", usersBooks.BooksId);
                ViewBag.users = new SelectList(users, "Id", "Name", usersBooks.UserId);
                ViewBag.error = "Дата заказа не должна быть пустой и должна быть больше текущей даты";
                return View(usersBooks);
            }

            UsersBooksBM busersBooks = AutoMapper<AuthorBook, UsersBooksBM>.Map(usersBooks);

            if (userBookService.CheckUser(usersBooks.UserId))
            {
                userBookService.CreateOrUpdate(busersBooks);
                userBookService.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Данный пользователь критический задолжник!!!!";
                ViewBag.BooksId = new SelectList(books, "Id", "Title", usersBooks.BooksId);
                ViewBag.UserId = new SelectList(users, "Id", "Name", usersBooks.UserId);
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            userBookService.DeleteUserBook(id);
            userBookService.Save();
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
            AuthorBook usersBooks = AutoMapper<UsersBooksBM, AuthorBook>.Map(userBookService.GetUserBook, (int)id);
            usersBooks.ReturnDate = DateTime.Now;
            UsersBooksBM busersBooks = AutoMapper<AuthorBook, UsersBooksBM>.Map(usersBooks);
            userBookService.CreateOrUpdate(busersBooks);
            return RedirectToAction("Index", "UsersBooks");
        }
    }
}