using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using BL.BuisnessModel;
using BL.Service;
using DL.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BookController : Controller
    {
        IBookService bookService;
        IAuthorService authorService;
        public BookController(IBookService serv, IAuthorService serv2)
        {
            bookService = serv;
            authorService = serv2;
        }

        public ActionResult Index()
        {
            return View(AutoMapper<IEnumerable<BookBM>, List<BookModel>>.Map(bookService.GetBooks));
        }

        public ActionResult CreateEdit(int? id=0)
        {
            BookModel book = new BookModel();
            List<AuthorModel> authors = AutoMapper<IEnumerable<AuthorBM>, List<AuthorModel>>.Map(authorService.GetAuthors);
            ViewBag.AuthorId = new SelectList(authors, "Id", "FirstName");
            if (id != 0)
            {
                book = AutoMapper<BookBM, BookModel>.Map(bookService.GetBook,(int)id);
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult CreateEdit(BookModel book)
        {
            BookBM newBook = AutoMapper<BookModel, BookBM>.Map(book);
            bookService.CreateOrUpdate(newBook);
            bookService.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            bookService.DeleteBook(id);
            bookService.Save();
            return RedirectToAction("Index");
        }
    }
}