using System.Collections.Generic;
using System.Linq;
using BookStore.Data;
using BookStore.Model;
using BookStore.Services.Contracts;

namespace BookStore.Services
{
    public class UserServices : IUserServices
    {
        private readonly BookStoreDbContext context;

        public UserServices(BookStoreDbContext context)
        {
            this.context = context;
        }
        public BookStoreUser GetUserByUsername(string username)
        {
            return this.context.BookStoreUsers.FirstOrDefault(x => x.UserName == username);
        }

        public IList<BookStoreUser> GetAllUsers()
        {
            var allUsers = this.context.BookStoreUsers.ToList();
            return allUsers;
        }

        public void EditFirstName(BookStoreUser user, string firstName)
        {
            if (user == null)
            {
                return;
            }

            user.FirstName = firstName;
            this.context.SaveChanges();
        }

        public void EditLastName(BookStoreUser user, string lastName)
        {
            if (user == null)
            {
                return;
            }

            user.LastName = lastName;
            this.context.SaveChanges();
        }

        public void EditUsername(BookStoreUser user, string userName)
        {
            if (user == null)
            {
                return;
            }

            user.UserName = userName;
            this.context.SaveChanges();
        }

        public void EditEmail(BookStoreUser user, string email)
        {
            if (user == null)
            {
                return;
            }

            user.Email = email;
            this.context.SaveChanges();
        }

   
        public void EditPhonenumber(BookStoreUser user, string phonenumber)
        {
            if (user == null)
            {
                return;
            }

            user.PhoneNumber = phonenumber;
            this.context.SaveChanges();
        }
    }
}