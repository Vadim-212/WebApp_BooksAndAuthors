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
    public class UserController : Controller
    {
        IUserService userService;
        public UserController(IUserService serv)
        {
            userService = serv;
        }

        public ActionResult Index()
        {
            return View(AutoMapper<IEnumerable<UserBM>,List<UserModel>>.Map(userService.GetUsers));
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
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            userService.DeleteUser(id);
            userService.Save();
            return RedirectToAction("Index");
        }
        public ActionResult GetUsers()
        {
            List<UserModel> users = AutoMapper<IEnumerable<UserBM>, List<UserModel>>.Map(userService.GetUsers);
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}