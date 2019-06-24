﻿using System.Text.RegularExpressions;

namespace BookStore.Model
{
    public class Book
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Publishing { get; set; }

        public string ISBN { get; set; }

        public string YearOfPublishing { get; set; }

        public string Language { get; set; }
    }
}