using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Entity;
using System.Data.Entity;

namespace DL.Repository
{
    public class UserRepository : IRepository<Users>
    {
        private Library db;

        public UserRepository(Library db)
        {
            this.db = db;
        }

        public void Create(Users item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            Users user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public IEnumerable<Users> Find(Func<Users, bool> predicate)
        {
            IEnumerable<Users> users = db.Users.Where(predicate);
            return users;
        }

        public Users Get(int id)
        {
            Users user = db.Users.Find(id);
            return user;
        }

        public IEnumerable<Users> GetAll()
        {
            return db.Users.ToList();
        }

        public void Update(Users item)
        {
            //db.Entry(item).State = EntityState.Modified;
            db.Users.Where(x => x.Id == item.Id).FirstOrDefault().Name = item.Name;
            db.Users.Where(x => x.Id == item.Id).FirstOrDefault().Email = item.Email;
        }
    }
}
