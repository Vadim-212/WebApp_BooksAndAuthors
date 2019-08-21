﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entity
{
    public class BookWithAuthor
    {
        public int Id { get; set; }

        public string Author { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public int? Pages { get; set; }

        public int? Price { get; set; }


    }
}