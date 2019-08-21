using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity;

namespace WebApplication1.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            List<BookWithAuthor> books = new List<BookWithAuthor>();
            using (Library db = new Library())
            {
                List<Books> b = db.Books.ToList();
                foreach (var item in b)
                {
                    books.Add(new BookWithAuthor()
                    {
                        Id = item.Id,
                        Author = db.Authors.Where(x => x.Id == item.AuthorId).FirstOrDefault().FirstName + " " + db.Authors.Where(x => x.Id == item.AuthorId).FirstOrDefault().LastName,
                        Title = item.Title,
                        Pages = item.Pages,
                        Price = item.Price
                    });
                }
            }
            return View(books);
        }
    }
}