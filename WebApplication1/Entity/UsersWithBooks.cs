using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity
{
    public class UsersWithBooks
    {
        public int Id { get; set; }
        public string User { get; set; }
        public int UserId { get; set; }
        public string Book { get; set; }
        public int BookId { get; set; }
    }
}