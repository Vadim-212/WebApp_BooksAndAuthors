﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            List<Users> users;
            using (Library db = new Library())
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(db.UsersBooks.ToList(), JsonRequestBehavior.AllowGet);
                }
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
        
        public ActionResult ShowHistory(int id)
        {
            List<UsersBooks> usersBooks;
            using (Library db = new Library())
            {
                usersBooks = db.UsersBooks.Where(x => x.UserId == id).ToList();
                foreach (var item in usersBooks)
                    item.Books = null;
            }
            return PartialView("Partial/_UserOrdersHistoryPartial", usersBooks);
            //return Json(usersBooks, JsonRequestBehavior.AllowGet);
        }
    }
}