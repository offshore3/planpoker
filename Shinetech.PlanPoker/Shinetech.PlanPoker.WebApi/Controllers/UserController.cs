using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.Logic.Tools;
using Shinetech.PlanPoker.WebApi.Tools;
using Shinetech.PlanPoker.WebApi.ViewModels;

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
            try
            {
                _userLogic.Create(userLogicModel);
            }
            catch (PlanPokerException exception)
            {
                LogHelper.WriteLog(exception.GetType(), exception.Key);
            }
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
        [PlanPokerAuthorize]
        public UserViewModel GetUserViewModel()
        {
            return LoginUser;
        }

        [HttpPut]
        [Route("user")]
        [PlanPokerAuthorize]
        public void EditUser(UserViewModel userViewModel)
        {
            _userLogic.Edit(userViewModel.ToLogicModel());
        }

        [HttpGet]
        [Route("getuserbyemail")]
        public UserViewModel GetUserByEmail(string email)
        {
           return _userLogic.GetUserByEmail(email).ToViewModel();
        }

        [HttpPut]
        [Route("resetpassowrd")]
        public void ResetPassword(UserViewModel userViewModel)
        {
            _userLogic.EditPassword(userViewModel.ToLogicModel());
        }

        [HttpPut]
        [Route("changepassword")]
        [PlanPokerAuthorize]
        public string EditUserPassword(UserViewModel userViewModel)
        {
            _userLogic.EditPassword(userViewModel.ToLogicModel());

            return Login(userViewModel.Email, userViewModel.Password);
        }

        [HttpPost]
        [Route("sendforgetpasswordmail")]
        public bool SendForgetPassowrdEmail(SendEmailViewModel sendEmailViewModel)
        {
            return _userLogic.SendForgetPassowrdEmail(sendEmailViewModel.ToLogicModel());
        }

        [HttpGet]
        [Route("resetpassworddecrypt")]
        public string DecryptProjectCode(string resetPasswordToken)
        {
            return TokenGenerator.DecodeToken(resetPasswordToken);
        }

        [HttpGet]
        [Route("login-with-linkedin")]
        public HttpResponseMessage LoginWithLinkedIn(string code, string state)
        {
            var result = _userLogic.LoginWithLinkedIn(code, state);
            
            var response = Request.CreateResponse(HttpStatusCode.Redirect);
            
            response.Headers.Location = new Uri(result);
            return response;
        }

        [HttpGet]
        [Route("login-with-facebook")]
        public HttpResponseMessage LoginWithFacebook(string code, string state)
        {
            var result = _userLogic.LoginWithLinkedIn(code, state);

            var response = Request.CreateResponse(HttpStatusCode.Redirect);

            response.Headers.Location = new Uri(result);
            return response;
        }
    }
}