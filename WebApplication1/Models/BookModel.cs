using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        public Image Img { get; set; }
        [Required]
        [Display(Name = "Upload File")]
        public HttpPostedFileBase ImgAttached { get; set; }

        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        public int? Pages { get; set; }

        [Required]
        public int? Price { get; set; }
    }
}