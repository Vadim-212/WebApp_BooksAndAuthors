using DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BL.BuisnessModel;
using BL;
using BL.Service;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private UserService userService;
        public UserController()
        {
            userService = new UserService(new DL.Repository.UnitOfWork(new Library()));
        }
        // GET: User
        public ActionResult Index()
        {
            List<Users> users;
            using (Library db = new Library())
            {
                users = db.Users.ToList();
            }
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Users user)
        {
            using (Library db = new Library())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            return Redirect("Index");
        }
        
        public ActionResult Edit(int id)
        {
            Users user;
            using (Library db = new Library())
            {
                user = db.Users.Where(x => x.Id == id).FirstOrDefault();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Users user)
        {
            using (Library db = new Library())
            {
                db.Users.Where(x => x.Id == user.Id).FirstOrDefault().Name = user.Name;
                db.Users.Where(x => x.Id == user.Id).FirstOrDefault().Email = user.Email;
                db.SaveChanges();
            }
            return Redirect("~/User/Index");
        }

        public ActionResult CreateEdit(int id)
        {
            if (id == -1)
                return View();
            Users user;
            using (Library db = new Library())
            {
                user = db.Users.Where(x => x.Id == id).First();
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult CreateEdit(Users user)
        {
            using (Library db = new Library())
            {
                Users u = new Users();
                try
                {
                    u = db.Users.Where(x => x.Id == user.Id).First();
                }
                catch
                {
                    db.Users.Add(user);
                }
                if (u.Name != null && u.Email != null && u.Id != 0)
                {
                    db.Users.Where(x => x.Id == user.Id).First().Name = user.Name;
                    db.Users.Where(x => x.Id == user.Id).First().Email = user.Email;
                }
                db.SaveChanges();
            }
            return Redirect("~/User/Index");
        }

        public ActionResult Delete(int id)
        {
            using (Library db = new Library())
            {
                db.Users.Remove(db.Users.Where(x => x.Id == id).FirstOrDefault());
                db.SaveChanges();
            }
            return Redirect("~/User/Index");
        }

        public ActionResult GetTop5Orders(int userId)
        {
            List<UsersBooks> ba = AutoMapper<IEnumerable<UsersBooksBM>, List<UsersBooks>>.Map(userService.GetReturnBooks, (int)userId);
            Users user = AutoMapper<UserBM, Users>.Map(userService.GetUser, (int)userId);

            var result = new JsonResult
            {
                Data = new { res = ba },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            return result;
        }
    }
}