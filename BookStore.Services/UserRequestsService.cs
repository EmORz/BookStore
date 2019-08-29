using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Data;
using BookStore.Model;
using BookStore.Services.Contracts;

namespace BookStore.Services
{
    public class UserRequestsService : IUserRequestsService
    {
        private const int BULGARIAN_HOURS_FROM_UTC_TIME = 2;

        private readonly BookStoreDbContext db;

        public UserRequestsService(BookStoreDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<UserRequest> All()
        {
            return db.UserRequests;
        }

        public void Create(string title, string email, string content)
        {
            var userRequest = new UserRequest
            {
                Title = title,
                Email = email,
                Content = content,
                RequestDate = DateTime.UtcNow.AddHours(BULGARIAN_HOURS_FROM_UTC_TIME)
            };

            this.db.UserRequests.Add(userRequest);
            this.db.SaveChanges();
        }

        public bool Delete(int id)
        {
            var userRequest = this.GetRequestById(id);

            if (userRequest == null)
            {
                return false;
            }

            this.db.UserRequests.Remove(userRequest);
            this.db.SaveChanges();

            return true;
        }

        public UserRequest GetRequestById(int id)
        {
            return this.db.UserRequests.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<UserRequest> GetUnseenRequests()
        {
            return this.db.UserRequests.Where(x => x.Seen == false);
        }

        public void Seen(int id)
        {
            var userRequest = this.GetRequestById(id);

            if (userRequest == null)
            {
                return;
            }

            userRequest.Seen = true;
            this.db.SaveChanges();
        }

        public void Unseen(int id)
        {
            var userRequest = this.GetRequestById(id);

            if (userRequest == null)
            {
                return;
            }

            userRequest.Seen = false;
            this.db.SaveChanges();
        }
    }
}