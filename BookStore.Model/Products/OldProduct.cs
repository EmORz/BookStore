using BookStore.Model.HelpModels;

namespace BookStore.Model
{
    public class OldProduct : EntityBase<int>
    {
        public OldProduct()
        {
            this.User = new User();
        }
        public string Name { get; set; }

        public TypeOldProduct Products { get; set; }

        public bool IsDonation { get; set; }

        public decimal Price { get; set; }

        public User User { get; set; }

        /*### OldBook
           - Id (int)
           - Name (string)
           - Type (enum) (books/other)
           - IsDonation (bool)
           - Price (decimal)*/

    }
}