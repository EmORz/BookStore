using System.Linq;
using System.Threading.Tasks;

using BookStore.Services.Contracts;
using BookStore_Inspiration.ViewModels.UserRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Inspiration.Controllers
{
    public class UserRequestsController : Controller
    {
        private readonly IUserRequestsService userRequestService;
        

        public UserRequestsController(IUserRequestsService userRequestService)
        {
            this.userRequestService = userRequestService;
          
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(int id)
        {
            var userRequestsViewModel = this.userRequestService.All().OrderByDescending(x => x.RequestDate).Select(x => new UserRequestViewModel()
            {
                Content = x.Content,
                Title = x.Title,
                Email = x.Email,
                Id = x.Id,
                RequestDate = x.RequestDate,
                Seen = x.Seen
            }).ToList();

            var currentUserRequest = this.userRequestService.GetRequestById(id);
            var temp = new  UserRequestViewModel();
            if (currentUserRequest == null)
            {
                temp = userRequestsViewModel.FirstOrDefault();
            }
            else
            {
                temp = new UserRequestViewModel()
                {
                    Content = currentUserRequest.Content,
                    Email = currentUserRequest.Email,
                    Id = currentUserRequest.Id,
                    RequestDate = currentUserRequest.RequestDate,
                    Seen = currentUserRequest.Seen,
                    Title = currentUserRequest.Title
                };
            }
          

           

            this.userRequestService.Seen(id);

            var viewModel = new IndexUserRequestViewModel
            {
                UserRequestsViewModel = userRequestsViewModel,
                UserRequestViewModel = temp
            };

            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Delete(int id)
        {
            var userRequests = this.userRequestService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Unseen(int id)
        {
            this.userRequestService.Unseen(id);

            return RedirectToAction(nameof(Index));
        }
    }
}