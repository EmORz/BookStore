using System.Collections.Generic;
using BookStore.Model;

namespace BookStore.Services.Contracts
{
    public interface IUserServices
    {
        BookStoreUser GetUserByUsername(string username);

        IList<BookStoreUser> GetAllUsers();

        void EditFirstName(BookStoreUser user, string firstName);

        void EditLastName(BookStoreUser user, string lastName);

        void EditUsername(BookStoreUser user, string userName);

        void EditEmail(BookStoreUser user, string email);

        void EditPhonenumber(BookStoreUser user, string phonenumber);

    }
}