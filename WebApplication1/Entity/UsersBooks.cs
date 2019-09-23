using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity
{
    public class UsersBooks
    {
        public int Id { get; set; }

        //[Required]
        //[ForeignKey("Users")]
        public int UserId { get; set; }

        //[Required]
        //[ForeignKey("Books")]
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime Time { get; set; }
        public DateTime ReturnDate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Books> Books { get; set; }

        //public Users Users { get; set; }
        //public Books Books { get; set; }
    }
}