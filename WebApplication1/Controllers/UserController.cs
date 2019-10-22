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
using WebApplication1.Helpers;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        IUserService userService;
        IUserBookService userBookService;
        public UserController(IUserService serv,IUserBookService serv2)
        {
            userService = serv;
            userBookService = serv2;
            Log4Net.InitLogger();
        }

        public ActionResult Index()
        {
            List<UserModel> users = AutoMapper<IEnumerable<UserBM>, List<UserModel>>.Map(userService.GetUsers);

            if (users.Count > 0)
            {
                List<UserModel> usersList2 = users;
                List<UserModelSmall> u = new List<UserModelSmall>();
                foreach (var item in usersList2)
                {
                    u.Add(new UserModelSmall()
                    {
                        Id = item.Id,
                        UserName = item.Name,
                        BookCount = userBookService.GetUsersBooks().Where(x => x.UserId == item.Id).Count()
                    });
                }
                u.OrderByDescending(x => x.BookCount);
                if (u.Count > 5)
                    users.First().Top5Users = u.Take(5);
                else
                    users.First().Top5Users = u.Take(5);
            }
            return View(users);
        }

        public ActionResult HistoryBooks()
        {
            return PartialView();
        }

        public ActionResult CreateEdit(int? id=0)
        {
                List<AuthorBook> ab =  AutoMapper<IEnumerable<UsersBooksBM>, List<AuthorBook>>.Map(userService.GetReturnBooks,(int)id);
                UserModel user = AutoMapper<UserBM, UserModel>.Map(userService.GetUser,(int)id);
                ViewBag.books = ab;
                return View(user);
        }

        [HttpPost]
        public ActionResult CreateEdit(UserModel model)
        {
            UserBM user = AutoMapper<UserModel, UserBM>.Map(model);
            userService.CreateOrUpdate(user);
            userService.Save();
            Log4Net.Log.Info("User create/edit");
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            userService.DeleteUser(id);
            userService.Save();
            Log4Net.Log.Info("User delete");
            return RedirectToAction("Index");
        }
        public ActionResult GetUsers()
        {
            List<UserModel> users = AutoMapper<IEnumerable<UserBM>, List<UserModel>>.Map(userService.GetUsers);
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}