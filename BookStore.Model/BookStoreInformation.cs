using System.Collections.Generic;

namespace BookStore.Model
{
    public class BookStoreInformation 
    {
        public int Id { get; set; }

        public BookStoreInformation()
        {
            this.Personals = new List<Personal>();
            this.Products = new List<Product>();
        }
        public string NameOfBookStore { get; set; }

      


        public string Address { get; set; }

        public List<Personal> Personals { get; set; }

        public List<Product> Products { get; set; }



        /*### BookStoreData
           - Id (int)
           - Name (strng)
           - Address (string)
           - Personal (List<Personal>)*/
    }
}