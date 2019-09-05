using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DL.Entity
{
    public class UsersBooks
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime Time { get; set; }
        public DateTime ReturnDate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Books> Books { get; set; }
    }
}