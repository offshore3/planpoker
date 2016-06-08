using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.Tools;
using Shinetech.PlanPoker.WebApi.ViewModels;
using Convert = Shinetech.PlanPoker.WebApi.ViewModels.Convert;

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
        public void Create(UserViewModel userViewModel) {
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
        [Route("test-authorize")]
        [BasicAuthorize]
        public string TestAuthorize()
        {
            return "OK";
        }

        
    }
}