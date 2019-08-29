using System;

namespace BookStore_Inspiration.ViewModels.UserRequest
{
    public class UserRequestViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }

        public bool Seen { get; set; }

        public DateTime RequestDate { get; set; }
    }
}