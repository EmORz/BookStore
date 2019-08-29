using System.Collections.Generic;
using BookStore_Inspiration.Controllers;

namespace BookStore_Inspiration.ViewModels.UserRequest
{
    public class IndexUserRequestViewModel
    {
        public IList<UserRequestViewModel> UserRequestsViewModel { get; set; }

        public UserRequestViewModel UserRequestViewModel { get; set; }
    }
}