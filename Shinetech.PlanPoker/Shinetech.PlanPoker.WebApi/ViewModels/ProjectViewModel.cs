using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shinetech.PlanPoker.WebApi.ViewModels
{
    public class ProjectsViewModel
    {
        public int Pages { get; set; }

        public IEnumerable<ProjectViewModel> ProjectViewModels { get; set; }
    }
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserViewModel OwnerViewModel { get; set; }
        public IEnumerable<UserViewModel> Participates { get; set; }
    }
}