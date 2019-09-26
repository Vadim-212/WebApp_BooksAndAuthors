using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity
{
    public partial class Surveys
    {
        public int Id { get; set; }

        public int AgeValue { get; set; }
        
        public int ReadingValue { get; set; }

        public int FantasticsGenre { get; set; }
        public int DetectiveGenre { get; set; }
        public int HorrorGenre { get; set; }
        public int NovelGenre { get; set; }
        public int ClassicGenre { get; set; }
        public int ScienceGenre { get; set; }
        public int ComputersGenre { get; set; }
        public int ArtGenre { get; set; }
        public int AdventureGenre { get; set; }
        public int BuisnessGenre { get; set; }

        public string AuthorsSentenceText { get; set; }

        public string SentenceText { get; set; }
    }
}