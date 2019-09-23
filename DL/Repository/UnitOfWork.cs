using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Entity;

namespace DL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Library db;
        private AuthorRepository authorRepository;
        private BookRepository bookRepository;
        private UserRepository userRepository;
        private UsersBooksRepository usersBooksRepository;
        private GenreRepository genreRepository;
        private bool disposed = false;

        public UnitOfWork(Library db)
        {
            this.db = db;
        }
        public IRepository<Authors> Authors
        {
            get
            {
                if (authorRepository == null)
                    authorRepository = new AuthorRepository(db);
                return authorRepository;
            }
        }

        public IRepository<Books> Books
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BookRepository(db);
                return bookRepository;
            }
        }

        public IRepository<Users> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<UsersBooks> UsersBooks
        {
            get
            {
                if (usersBooksRepository == null)
                    usersBooksRepository = new UsersBooksRepository(db);
                return usersBooksRepository;
            }
        }

        public IRepository<Genre> Genre
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }
        
        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
