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
                        Price = item.Price,
                        Authors = db.Authors.ToList()
                    });
                }
            }
            return View(books);
        }

        public ActionResult Create()
        { 
            BookWithAuthor b = new BookWithAuthor();
            using (Library db = new Library())
            {
                b.Authors = db.Authors.ToList();
                List<SelectListItem> sli = new List<SelectListItem>();

                foreach(var item in db.Authors.ToList())
                {
                    sli.Add(new SelectListItem()
                    {
                        Selected = false,
                        Text = item.FirstName + " " + item.LastName,
                        Value = item.Id.ToString()
                    });
                }

                sli[0].Selected = true;
                ViewBag.authorsList = sli;
            }
            
            return View(b);
        }

        [HttpPost]
        public ActionResult Create(BookWithAuthor book)
        {
            Books b;
            using (Library db = new Library())
            {
                b = new Books()
                {
                    Id = book.Id,
                    AuthorId = int.Parse(Request.Form["authorsList"]),
                    Title = book.Title,
                    Pages = book.Pages,
                    Price = book.Price
                };
                db.Books.Add(b);
                db.SaveChanges();
            }
            return Redirect("Index");
        }

        public ActionResult Edit(int id)
        {
            BookWithAuthor book;
            using (Library db = new Library())
            {
                Books b = db.Books.Where(x => x.Id == id).FirstOrDefault();
                List<SelectListItem> sli = new List<SelectListItem>();
                bool isSelected = false;
                foreach(var item in db.Authors.ToList())
                {
                    if (item.Id == b.AuthorId)
                        isSelected = true;
                    else
                        isSelected = false;
                    sli.Add(new SelectListItem()
                    {
                        Selected = isSelected,
                        Text = item.FirstName + " " + item.LastName,
                        Value = item.Id.ToString()
                    });
                }
                ViewBag.authorsList = sli;
                book = new BookWithAuthor()
                {
                    Id = b.Id,
                    SelectedAuthor = sli.Where(x => x.Selected == true).FirstOrDefault(),
                    Title = b.Title,
                    Pages = b.Pages,
                    Price = b.Price
                };

            }
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(BookWithAuthor book)
        {
            using (Library db = new Library())
            {
                db.Books.Where(x => x.Id == book.Id).FirstOrDefault().AuthorId = int.Parse(Request.Form["authorsList"]);
                db.Books.Where(x => x.Id == book.Id).FirstOrDefault().Title = book.Title;
                db.Books.Where(x => x.Id == book.Id).FirstOrDefault().Pages = book.Pages;
                db.Books.Where(x => x.Id == book.Id).FirstOrDefault().Price = book.Price;
                db.SaveChanges();
            }
            return Redirect("~/Book/Index");
        }

        public ActionResult CreateEdit(int id)
        {
            BookWithAuthor book;
            using (Library db = new Library())
            {
                if (id == -1)
                {
                    List<SelectListItem> sli1 = new List<SelectListItem>();

                    foreach (var item in db.Authors.ToList())
                    {
                        sli1.Add(new SelectListItem()
                        {
                            Selected = false,
                            Text = item.FirstName + " " + item.LastName,
                            Value = item.Id.ToString()
                        });
                    }

                    sli1[0].Selected = true;
                    ViewBag.authorsList = sli1;
                    return View();
                }

                
                Books b = db.Books.Where(x => x.Id == id).FirstOrDefault();
                List<SelectListItem> sli = new List<SelectListItem>();
                bool isSelected = false;
                foreach (var item in db.Authors.ToList())
                {
                    if (item.Id == b.AuthorId)
                        isSelected = true;
                    else
                        isSelected = false;
                    sli.Add(new SelectListItem()
                    {
                        Selected = isSelected,
                        Text = item.FirstName + " " + item.LastName,
                        Value = item.Id.ToString()
                    });
                }
                ViewBag.authorsList = sli;
                book = new BookWithAuthor()
                {
                    Id = b.Id,
                    SelectedAuthor = sli.Where(x => x.Selected == true).FirstOrDefault(),
                    Title = b.Title,
                    Pages = b.Pages,
                    Price = b.Price
                };

            }
            return View(book);
        }
        [HttpPost]
        public ActionResult CreateEdit(BookWithAuthor book)
        {
            using (Library db = new Library())
            {
                Books newBook = new Books()
                {
                    Id = book.Id,
                    AuthorId = int.Parse(Request.Form["authorsList"]),
                    Title = book.Title,
                    Pages = book.Pages,
                    Price = book.Price
                };
                try
                {
                    var b = db.Books.Where(x => x.Id == book.Id).First();
                    db.Entry(newBook).State = System.Data.Entity.EntityState.Modified;
                }
                catch
                {
                    db.Books.Add(newBook);
                }
                db.SaveChanges();
            }
            return Redirect("~/Book/Index");
        }

        public ActionResult Delete(int id)
        {
            using (Library db = new Library())
            {
                db.Books.Remove(db.Books.Where(x => x.Id == id).FirstOrDefault());
                db.SaveChanges();
            }
            return Redirect("~/Book/Index");
        }

        public ActionResult Survey()
        {
            Surveys survey = new Surveys();
            return View(survey);
        }
        
        [HttpPost]
        public ActionResult Survey(Surveys obj)
        {
            string ageRadioValue = Request.Params["surveyAgeRadios"];
            obj.AgeValue = int.Parse(ageRadioValue);

            string readingRadioValue = Request.Params["surveyBookReadRadios"];
            obj.ReadingValue = int.Parse(readingRadioValue);

            obj.FantasticsGenre = 0;
            obj.DetectiveGenre = 0;
            obj.HorrorGenre = 0;
            obj.NovelGenre = 0;
            obj.ClassicGenre = 0;
            obj.ScienceGenre = 0;
            obj.ComputersGenre = 0;
            obj.ArtGenre = 0;
            obj.AdventureGenre = 0;
            obj.BuisnessGenre = 0;

            string genresCheckboxesValues = Request.Params["bookGenreChecks"];
            string[] cbValues = genresCheckboxesValues.Split(',');
            foreach(var item in cbValues)
            {
                switch(item)
                {
                    case "1":
                        obj.FantasticsGenre = 1;
                        break;
                    case "2":
                        obj.DetectiveGenre = 1;
                        break;
                    case "3":
                        obj.HorrorGenre = 1;
                        break;
                    case "4":
                        obj.NovelGenre = 1;
                        break;
                    case "5":
                        obj.ClassicGenre = 1;
                        break;
                    case "6":
                        obj.ScienceGenre = 1;
                        break;
                    case "7":
                        obj.ComputersGenre = 1;
                        break;
                    case "8":
                        obj.ArtGenre = 1;
                        break;
                    case "9":
                        obj.AdventureGenre = 1;
                        break;
                    case "10":
                        obj.BuisnessGenre = 1;
                        break;
                }
            }

            obj.AuthorsSentenceText = Request.Form["authorsSentenceInp"];
            obj.SentenceText = Request.Form["sentenceInp"];
            return RedirectToAction("Index");
        }
    }
}