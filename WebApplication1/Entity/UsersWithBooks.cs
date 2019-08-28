using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddThh:mm}")]
        public DateTime IssueDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddThh:mm}")]
        public DateTime Time { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddThh:mm}")]
        public DateTime ReturnDate { get; set; }
    }
}