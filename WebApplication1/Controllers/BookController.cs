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
using System.IO;
using System.Drawing;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class BookController : Controller
    {
        IBookService bookService;
        IAuthorService authorService;
        IGenreService genreService;
        public BookController(IBookService serv, IAuthorService serv2,IGenreService serv3)
        {
            bookService = serv;
            authorService = serv2;
            genreService = serv3;
        }

        public ActionResult Index()
        {
            List<BookModel> books = AutoMapper<IEnumerable<BookBM>, List<BookModel>>.Map(bookService.GetBooks);
            foreach(var item in books)
            {
                item.AuthorName = authorService.GetAuthor(item.AuthorId).FirstName + " " + authorService.GetAuthor(item.AuthorId).LastName;
                item.GenreName = genreService.GetGenre(item.GenreId).Name;
            }
            return View(books);
        }

        public ActionResult CreateEdit(int? id=0)
        {
            BookModel book = new BookModel();
            List<AuthorModel> authors = AutoMapper<IEnumerable<AuthorBM>, List<AuthorModel>>.Map(authorService.GetAuthors);
            List<GenreModel> genres = AutoMapper<IEnumerable<GenreBM>, List<GenreModel>>.Map(genreService.GetGenres);
            ViewBag.AuthorId = new SelectList(authors, "Id", "FirstName");
            ViewBag.GenreId = new SelectList(genres, "Id", "Name");
            if (id != 0)
            {
                book = AutoMapper<BookBM, BookModel>.Map(bookService.GetBook,(int)id);
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult CreateEdit(BookModel book, HttpPostedFileBase imageBook = null)
        {
            BookBM newBook = AutoMapper<BookModel, BookBM>.Map(book);

            
            byte[] imageData = null;
            var im = imageBook;
            if (imageBook != null)
            {
                using (var binaryReader = new BinaryReader(imageBook.InputStream))
                {
                    imageData = binaryReader.ReadBytes(imageBook.ContentLength);
                }
                newBook.Image = imageData;
            }


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
        public ActionResult GetBooks()
        {
            List<BookModel> books = AutoMapper<IEnumerable<BookBM>, List<BookModel>>.Map(bookService.GetBooks);
            foreach (var item in books)
            {
                item.Image = null;
            }
            return Json(books, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGenres()
        {
            List<GenreModel> genres = AutoMapper<IEnumerable<GenreBM>, List<GenreModel>>.Map(genreService.GetGenres);
            return Json(genres, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBook(int id)
        {
            BookModel book = AutoMapper<BookBM, BookModel>.Map(bookService.GetBook, id);
            return Json(book, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CreateEditAjax(string obj)
        {
            BookModel book = JsonConvert.DeserializeObject<BookModel>(obj);
            BookBM oldBook = AutoMapper<BookModel, BookBM>.Map(book);
            bookService.CreateOrUpdate(oldBook);
            bookService.Save();
            return RedirectToActionPermanent("Index", "Book");
        }
    }
}