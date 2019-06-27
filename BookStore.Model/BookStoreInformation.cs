﻿using System.Collections.Generic;
using BookStore.Model.HelpModels;

namespace BookStore.Model
{
    public class BookStoreInformation 
    {
        public int Id { get; set; }

        public BookStoreInformation()
        {
            this.Personals = new List<Personal>();
        }
        public string Name { get; set; }

        public string Address { get; set; }

        public List<Personal> Personals { get; set; }

        /*### BookStoreData
           - Id (int)
           - Name (strng)
           - Address (string)
           - Personal (List<Personal>)*/
    }
}