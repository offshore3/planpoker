using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shinetech.PlanPoker.WebApi.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserViewModel OwnerViewModel { get; set; }
        public IEnumerable<UserViewModel> Participates { get; set; }
    }
}