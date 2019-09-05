using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BuisnessModel
{
    public class UsersBooksBM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime Time { get; set; }
        public DateTime ReturnDate { get; set; }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string UserName { get; set; }
        public string BookName { get; set; }
    }
}
