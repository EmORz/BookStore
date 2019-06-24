using BookStore.Model.HelpModels;

namespace BookStore.Model
{
    public class Personal : EntityBase<string>
    {
        public FullName FullName { get; set; }

        public string Email { get; set; }

        public string Phonenumber { get; set; }

        /*## Personal
           - Id (string)
           - Name(string)
           - Email (string)
           - Phonenumber (string)*/
    }
}