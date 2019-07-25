using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BookStore.Data;
using BookStore.Model;
using BookStore.Services.Contracts;

namespace BookStore.Services
{
    public class UserServices : IUserServices
    {
        private readonly BookStoreDbContext context;
        
       private static string PasswordHash = "DB28C8F1-D3A0-4C82-ABB0-96E7E97825FE";
       private static readonly string SaltKey = "f9@4%aEb";
       private static readonly string VIKey = "Aeb4!98@b47oytrE";

        //todo - is it good idea?
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
        public BookStoreUser GetUserByUcn(string ucn)
        {
            var user = this.context.BookStoreUsers.FirstOrDefault(x => x.UCN == ucn);
            return user;
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

        public string EncryptData(string input)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(input);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            var temp = Convert.ToBase64String(cipherTextBytes);

            return temp;

        }

        public string DecryptData(string encryptUcn)
        {
            var user = this.context.BookStoreUsers.FirstOrDefault(x => x.UCN == encryptUcn);

            var tempEnc = user.UCN;

            byte[] cipherTextBytes = Convert.FromBase64String(tempEnc);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            var temp = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
            return temp;
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

        public void EditUCN(BookStoreUser user, string ucn)
        {
            if (user == null)
            {
                return;
            }

            user.UCN = ucn;
            this.context.SaveChanges();
        }

        public void DeleteUCN(BookStoreUser user)
        {
            user.UCN = null;
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