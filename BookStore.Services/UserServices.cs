using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BookStore.Data;
using BookStore.Model;
using BookStore.Services.Contracts;
using BookStore.Services.Helper;

namespace BookStore.Services
{
    public class UserServices : IUserServices
    {
        private readonly BookStoreDbContext context;

        private static readonly string PasswordHash = "DB28C8F1-D3A0-4C82-ABB0-96E7E97825FE";
        private static readonly string SaltKey = "f9@4%aEb";
        private static readonly string VIKey = "Aeb4!98@b47oytrE";

        public UserServices(BookStoreDbContext context)
        {
            this.context = context;
        }

        public BookStoreUser GetUserById(string id)
        {
            var userFromDb = this.context.BookStoreUsers.FirstOrDefault(x => x.Id == id);

            return userFromDb;
        }

        public BookStoreUser GetUserByUcn(string ucn)
        {
            var user = this.context.BookStoreUsers.FirstOrDefault(x => x.UCN == ucn);
            return user;
        }

        public IList<PersonalUserDataForSpecialOfert> ClientMetric(string ucn)
        {
            PersonalUserDataForSpecialOfert info = new PersonalUserDataForSpecialOfert();
            List<PersonalUserDataForSpecialOfert> listOfData = new List<PersonalUserDataForSpecialOfert>();


            if (!IsValidUCN(ucn))
            {
                info.IsValidUCN = false;
                info.Gender = "n";
                info.Month = "n";
                info.Region = "n";
                info.Year = "n";
                info.Day = "n";
                listOfData.Add(info);
                return listOfData;
            }

            #region Varable
            var year = "";
            var month = "";
            var day = "";
            var gender = "";
            var valid1 = "";
            var valid2 = "";
            var region = "";
            #endregion


            #region BgRegion
            for (int g = 0; g < ucn.Length - 1; g++)
            {
                if (g > 5)
                {

                    region += (ucn[g]);
                }
            }
            //=>

            var numRegio = int.Parse(region);

            var currentRegio = "";

            if (numRegio >= 0 && numRegio <= 43)
            {
                currentRegio = "Благоевград";
            }
            if (numRegio >= 44 && numRegio <= 93)
            {
                currentRegio = "Бургас";
            }
            if (numRegio >= 94 && numRegio <= 139)
            {
                currentRegio = "Варна";
            }
            if (numRegio >= 140 && numRegio <= 169)
            {
                currentRegio = "Велико Търново";
            }
            if (numRegio >= 170 && numRegio <= 183)
            {
                currentRegio = "Видин";
            }
            if (numRegio >= 184 && numRegio <= 217)
            {
                currentRegio = "Враца";
            }
            if (numRegio >= 218 && numRegio <= 233)
            {
                currentRegio = "Габрово";
            }
            if (numRegio >= 234 && numRegio <= 281)
            {
                currentRegio = "Кърджали";
            }
            if (numRegio >= 282 && numRegio <= 301)
            {
                currentRegio = "Кюстендил";
            }
            if (numRegio >= 302 && numRegio <= 319)
            {
                currentRegio = "Ловеч";
            }
            if (numRegio >= 320 && numRegio <= 341)
            {
                currentRegio = "Монтана";
            }
            if (numRegio >= 342 && numRegio <= 377)
            {
                currentRegio = "Пазарджик";
            }
            if (numRegio >= 378 && numRegio <= 395)
            {
                currentRegio = "Перник";
            }
            if (numRegio >= 396 && numRegio <= 435)
            {
                currentRegio = "Плевен";
            }
            if (numRegio >= 436 && numRegio <= 501)
            {
                currentRegio = "Пловдив";
            }
            if (numRegio >= 502 && numRegio <= 501)
            {
                currentRegio = "Пловдив";
            }
            if (numRegio >= 502 && numRegio <= 527)
            {
                currentRegio = "Разград";
            }
            if (numRegio >= 528 && numRegio <= 555)
            {
                currentRegio = "Русе";
            }
            if (numRegio >= 556 && numRegio <= 575)
            {
                currentRegio = "Силистра";
            }
            if (numRegio >= 576 && numRegio <= 601)
            {
                currentRegio = "Сливен";
            }
            if (numRegio >= 602 && numRegio <= 623)
            {
                currentRegio = "Смолян";
            }
            if (numRegio >= 624 && numRegio <= 721)
            {
                currentRegio = "София - град";
            }
            if (numRegio >= 722 && numRegio <= 751)
            {
                currentRegio = "София - област";
            }
            if (numRegio >= 752 && numRegio <= 789)
            {
                currentRegio = "Стара загора";
            }
            if (numRegio >= 790 && numRegio <= 821)
            {
                currentRegio = "Добрич (Толбухин)";
            }
            if (numRegio >= 822 && numRegio <= 843)
            {
                currentRegio = "Търговище";
            }
            if (numRegio >= 844 && numRegio <= 871)
            {
                currentRegio = "Хасково";
            }
            if (numRegio >= 872 && numRegio <= 903)
            {
                currentRegio = "Шумен";
            }
            if (numRegio >= 904 && numRegio <= 925)
            {
                currentRegio = "Ямбол";
            }
            if (numRegio >= 926 && numRegio <= 999)
            {
                currentRegio = "Друг/Неизвестен";
            }

            #endregion

            for (int i = 0; i < ucn.Length; i++)
            {
                if (i < 2)
                {
                    year += ucn[i];
                }
                else if (i >= 2 && i <= 3)
                {
                    month += ucn[i];
                }
                else if (i >= 4 && i <= 5)
                {
                    day += ucn[i];
                }
                else if (i >= 6 && i <= 7)
                {
                    valid1 += ucn[i];
                }
                else if (i >= 8 && i <= 9)
                {
                    valid2 += ucn[i];
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

            info.Region = currentRegio;
            info.Year = currentYear;
            info.Month = monthNum.ToString();
            info.Day = day;
            info.Gender = gender;
            listOfData.Add(info);

            return listOfData;
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

        public void DeleteUser(string id)
        {
            var userFromDb = this.context.BookStoreUsers.FirstOrDefault(x => x.Id == id);

            userFromDb.UserName = "xxx";
            userFromDb.FirstName = "xxx";
            userFromDb.LastName = "xxx";
            userFromDb.UCN = "xxx";
            userFromDb.PhoneNumber = "xxx";
            userFromDb.Email = "xxx";

            this.context.BookStoreUsers.Update(userFromDb);
            this.context.SaveChanges();

        }

        public bool EditAdmin(BookStoreUser user)
        {
            var id = user.Id;
            if (user == null)
            {
                return false;
            }
            var userFromDn = this.context.BookStoreUsers.FirstOrDefault(x => x.Id == id);
            userFromDn.FirstName = user.FirstName;
            userFromDn.LastName = user.LastName;
            userFromDn.Email = user.Email;
            userFromDn.PhoneNumber = user.PhoneNumber;



            this.context.BookStoreUsers.Update(user);
            this.context.SaveChanges();



            return true;
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

        #region Private Methods

        private bool IsValidUCN(string input)
        {
            if (input.Length != 10)
            {
                return false;
            }
            var total = 0;
            var last = input[input.Length - 1];
            int[] numChec = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            for (int r = 0; r < input.Length - 1; r++)
            {
                var temp = int.Parse(input[r].ToString()) * numChec[r];
                total += temp;
            }
            var rem = total % 11;
            if (rem.ToString() == last.ToString())
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}