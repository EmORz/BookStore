using System.Collections.Generic;
using System.Linq;
using BookStore.Data;
using BookStore.Model;
using BookStore.Services.Contracts;

namespace BookStore.Services
{
    public class PersonalUserDataForSpecialOfert
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Gender { get; set; }
    }

    public class UserServices : IUserServices
    {
        private readonly BookStoreDbContext context;

        //todo - is it good ide?
        private PersonalUserDataForSpecialOfert ClientData(string input)
        {
            PersonalUserDataForSpecialOfert info = new PersonalUserDataForSpecialOfert();
            var year = "";
            var month = "";
            var day = "";
            var gender = "";
            var valid1 = "";
            var valid2 = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (i < 2)
                {
                    year += input[i];
                }
                else if (i >= 2 && i <= 3)
                {
                    month += input[i];
                }
                else if (i >= 4 && i <= 5)
                {
                    day += input[i];
                }
                else if (i >= 6 && i <= 7)
                {
                    valid1 += input[i];
                }
                else if (i >= 8 && i <= 9)
                {
                    valid2 += input[i];
                }
            }

            for (int j = 0; j < valid2.Length; j++)
            {
                if (j == 0)
                {
                    var temp = int.Parse(valid2[j].ToString());
                    gender = (temp % 2 == 0 ? "Man" : "Woman");
                }
            }

            //add 40  or add 20
            var monthNum = int.Parse(month);
            var currentYear = "";

            if (monthNum > 20 && monthNum < 33)
            {
                monthNum -= 20;
                currentYear = "18" + year;
            }
            else if (monthNum > 40 && monthNum < 53)
            {
                monthNum -= 40;
                currentYear = "20" + year;
            }
            else
            {
                currentYear = "19" + year;
            }

            info.Year = currentYear;
            info.Month = monthNum.ToString();
            info.Day = day;
            info.Gender = gender;

            return info;

        }
        public UserServices(BookStoreDbContext context)
        {
            this.context = context;
        }

        public PersonalUserDataForSpecialOfert GetUserByUCN(string ucn)
        {
            var user = this.context.BookStoreUsers.FirstOrDefault(x => x.UCN == ucn);
            var usn = user?.UCN;
            var clientData = this.ClientData(usn);

            return clientData;
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