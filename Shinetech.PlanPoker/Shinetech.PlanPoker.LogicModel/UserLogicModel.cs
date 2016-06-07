﻿using System.Collections.Generic;
using System.Linq;
using Shinetech.PlanPoker.Data.Models;

namespace Shinetech.PlanPoker.LogicModel
{
    public class UserLogicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<ProjectLogicModel> Projects { get; set; }
    }
}