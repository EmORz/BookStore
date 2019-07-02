using BookStore.Model;
using BookStore.Services.Contracts;

namespace BookStore.Services
{
    public class UserServices : IUserServices
    {
        public BookStoreUser GetUserByUsername(string username)
        {
            throw new System.NotImplementedException();
        }

        public void EditFirstName(BookStoreUser user, string firstName)
        {
            throw new System.NotImplementedException();
        }

        public void EditLastName(BookStoreUser user, string lastName)
        {
            throw new System.NotImplementedException();
        }

        public void EditUsername(BookStoreUser user, string userName)
        {
            throw new System.NotImplementedException();
        }

        public void EditEmail(BookStoreUser user, string email)
        {
            throw new System.NotImplementedException();
        }

        public void EditPhonenumber(BookStoreUser user, string phonenumber)
        {
            throw new System.NotImplementedException();
        }
    }
}