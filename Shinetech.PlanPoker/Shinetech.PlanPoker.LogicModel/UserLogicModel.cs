﻿using System;
using System.Collections.Generic;

namespace Shinetech.PlanPoker.LogicModel
{
    public class UserLogicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }
        public string ImagePath { get; set; }
        public string ResetPasswordToken { get; set; }
        public string OpenId { get; set; }
        public DateTime ExpiredTime { get; set; }
        public IEnumerable<ProjectLogicModel> Projects { get; set; }
    }
}
