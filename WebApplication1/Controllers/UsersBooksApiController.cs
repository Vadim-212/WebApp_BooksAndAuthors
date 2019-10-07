using BL;
using BL.BuisnessModel;
using BL.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class UsersBooksApiController : ApiController
    {

        IUserBookService userBookService;
        IUserService userService;
        IBookService bookService;

        public UsersBooksApiController(IUserBookService serv, IUserService serv2, IBookService serv3)
        {
            userBookService = serv;
            userService = serv2;
            bookService = serv3;
        }

        // GET: api/UsersBooksApi
        [HttpGet]
        public IEnumerable<AuthorBook> GetUsersBooks()
        {
            List<AuthorBook> usersBooks = AutoMapper<IEnumerable<UsersBooksBM>, List<AuthorBook>>.Map(userBookService.GetUsersBooks);
            return usersBooks;
        }

        // GET: api/UsersBooksApi/5
        [ResponseType(typeof(AuthorBook))]
        [HttpGet]
        public IHttpActionResult GetUsersBooks(int id)
        {
            AuthorBook usersBooks = AutoMapper<UsersBooksBM, AuthorBook>.Map(userBookService.GetUserBook, (int)id);
            if (usersBooks == null)
            {
                return NotFound();
            }

            return Ok(usersBooks);
        }

        // PUT: api/UsersBooksApi/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutUserBook(int id, AuthorBook userBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userBook.Id)
            {
                return BadRequest();
            }

            UsersBooksBM ub = AutoMapper<AuthorBook, UsersBooksBM>.Map(userBook);
            userBookService.CreateOrUpdate(ub);

            try
            {
                userBookService.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserBookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UsersBooksApi
        [ResponseType(typeof(AuthorBook))]
        [HttpPost]
        public IHttpActionResult PostUserBook(AuthorBook userBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsersBooksBM ub = AutoMapper<AuthorBook, UsersBooksBM>.Map(userBook);

            userBookService.CreateOrUpdate(ub);
            userBookService.Save();

            return CreatedAtRoute("DefaultApi", new { id = userBook.Id }, userBook);
        }

        // DELETE: api/UsersBooksApi/5
        [ResponseType(typeof(AuthorBook))]
        [HttpDelete]
        public IHttpActionResult DeleteUserBook(int id)
        {
            UsersBooksBM userBook = userBookService.GetUserBook(id);
            if (userBook == null)
            {
                return NotFound();
            }

            userBookService.DeleteUserBook(id);
            userBookService.Save();

            return Ok(userBook);
        }

        private bool UserBookExists(int id)
        {
            UsersBooksBM userBook = userBookService.GetUserBook(id);
            if (userBook == null)
                return false;
            return true;
        }
    }
}
