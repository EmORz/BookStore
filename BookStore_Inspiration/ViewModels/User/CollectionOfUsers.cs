using System.Collections.Generic;

namespace BookStore_Inspiration.ViewModels.User
{
    public class CollectionOfUsers
    {
        public List<ListOfAllUserViewModels> Users { get; set; } = new List<ListOfAllUserViewModels>();
    }
}