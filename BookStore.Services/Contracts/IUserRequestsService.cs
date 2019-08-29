using System.Collections.Generic;
using BookStore.Model;

namespace BookStore.Services.Contracts
{
    public interface IUserRequestsService
    {
        void Create(string title, string email, string content);

        IEnumerable<UserRequest> All();

        UserRequest GetRequestById(int id);

        void Seen(int id);

        IEnumerable<UserRequest> GetUnseenRequests();

        void Unseen(int id);

        bool Delete(int id);
    }
}