using System.Collections.Generic;
using BookStore.Model;
using BookStore.Services.Helper;

namespace BookStore.Services.Contracts
{
    public interface IUserServices
    {
        IList<PersonalUserDataForSpecialOfert> ClientMetric(string ucn);
        BookStoreUser GetUserByUsername(string username);
        BookStoreUser GetUserByUcn(string ucn);

        IList<BookStoreUser> GetAllUsers();

        string EncryptData(string input);

        string DecryptData(string encryptData);

        void EditFirstName(BookStoreUser user, string firstName);

        void EditLastName(BookStoreUser user, string lastName);
        void EditUCN(BookStoreUser user, string ucn);

        void DeleteUCN(BookStoreUser user);

        void EditUsername(BookStoreUser user, string userName);

        void EditEmail(BookStoreUser user, string email);

        void EditPhonenumber(BookStoreUser user, string phonenumber);

    }
}