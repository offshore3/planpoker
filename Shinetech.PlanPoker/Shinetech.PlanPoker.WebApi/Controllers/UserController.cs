using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.Tools;
using Shinetech.PlanPoker.WebApi.ViewModels;
using Shinetech.PlanPoker.Logic.Tools;

namespace Shinetech.PlanPoker.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class UserController : BaseController
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic) : base(userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        [Route("user")]
        public void Create(UserViewModel userViewModel)
        {
            var userLogicModel = userViewModel.ToLogicModel();
            _userLogic.Create(userLogicModel);
        }

        [HttpGet]
        [Route("users")]
        public List<UserViewModel> GetUsers()
        {
            return _userLogic.GetAll().Select(x => new UserViewModel
            {
                Email = x.Email,
                Name = x.Name,
                Password = x.Password
            }).ToList();
        }

        [HttpGet]
        [Route("checkemail")]
        public bool CheckEmailExist(string email)
        {
            return _userLogic.CheckEmailExist(email);
        }

        [HttpGet]
        [Route("login")]
        public string Login(string email, string password)
        {
            return _userLogic.Login(email, password);
        }

        [HttpGet]
        [Route("user")]
        [BasicAuthorize]
        public UserViewModel GetUserViewModel()
        {
            return LoginUser;
        }

        [HttpPut]
        [Route("user")]
        [BasicAuthorize]
        public void EditUser(UserViewModel userViewModel)
        {
            _userLogic.Edit(userViewModel.ToLogicModel());
        }

        [HttpGet]
        [Route("get-user-by-email")]
        public UserViewModel GetUserByEmail(string email)
        {
           return _userLogic.GetUserByEmail(email).ToViewModel();
        }

        [HttpPut]
        [Route("reset-user-passowrd")]
        public void ResetPassword(UserViewModel userViewModel)
        {
            _userLogic.EditPassword(userViewModel.ToLogicModel());
        }

        [HttpPut]
        [Route("changepassword")]
        [BasicAuthorize]
        public string EditUserPassword(UserViewModel userViewModel)
        {
            _userLogic.EditPassword(userViewModel.ToLogicModel());

            return Login(userViewModel.Email, userViewModel.Password);
        }

        [HttpPost]
        [Route("sendemail")]
        public bool SendEmail(SendEmailViewModel sendEmailViewModel)
        {
            return SendEmailHelper.SendEmail(sendEmailViewModel);
        }
    }
}