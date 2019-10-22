﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entity
{
    public class LibraryInitializer: DropCreateDatabaseAlways<Library>
    {
        protected override void Seed(Library db)
        {
            // base.Seed(context);
            db.Authors.Add(new Authors() { FirstName = "Stephen", LastName = "King" });
            db.Authors.Add(new Authors() { FirstName = "Cake", LastName = "Dickinson" });
            db.Genre.Add(new Genre() { Name = "Novel" });
            db.Genre.Add(new Genre() { Name = "Detective" });
            db.Books.Add(new Books() { AuthorId = 1, GenreId = 1, Title = "Book 1", Pages = 200, Price = 500 });
            db.Books.Add(new Books() { AuthorId = 2, GenreId = 2, Title = "Book 2", Pages = 196, Price = 450 });
            db.Users.Add(new Users() { Name = "User1", Email = "myemail@mail.com" });
            db.Users.Add(new Users() { Name = "User2", Email = "usermail@mail.com" });
            db.UsersBooks.Add(new UsersBooks() { BookId = 1, UserId = 1, IssueDate = DateTime.Now, Time = DateTime.Now.AddDays(10), ReturnDate = new DateTime(1900, 1, 1, 0, 0, 0) });
            db.UsersBooks.Add(new UsersBooks() { BookId = 2, UserId = 2, IssueDate = DateTime.Now.AddDays(-6), Time = DateTime.Now.AddDays(20), ReturnDate = new DateTime(1900, 1, 1, 0, 0, 0) });
        }
    }

}
