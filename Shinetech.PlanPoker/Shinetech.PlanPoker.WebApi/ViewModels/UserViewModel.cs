using System;
using System.Collections.Generic;

namespace Shinetech.PlanPoker.WebApi.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }
        public string ImagePath { get; set; }
        public string ResetPasswordToken { get; set; }
        public DateTime ExpiredTime { get; set; }
        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}