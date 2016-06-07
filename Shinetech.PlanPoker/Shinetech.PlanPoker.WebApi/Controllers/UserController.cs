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
    public class UserController : ApiController
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        [Route("users")]
        public void create(UserViewModel userViewModel) {
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
        [Route("get-all")]
        [BasicAuthorize]
        public string GetAll()
        {
            return "OK";
        }
    }
}