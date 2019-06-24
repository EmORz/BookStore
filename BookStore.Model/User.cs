using BookStore.Model.HelpModels;

namespace BookStore.Model
{
    public class User : EntityBase<string>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phonenumber { get; set; }

        public FullName FullName { get; set; }

        public string Address { get; set; }
        /* - Username (string)
           - Password (string)
           - Email (string)
           - Phonenumber
           - Full Name (string)
           - Address (string)*/
    }
}