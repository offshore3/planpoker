using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shinetech.PlanPoker.LogicModel;

namespace Shinetech.PlanPoker.WebApi.ViewModels
{
    public static class Convert
    {
        public static UserLogicModel ToLogicModel(this UserViewModel user)
        {
            return new UserLogicModel
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Email = user.Email,
                ImagePath = user.ImagePath,
                Projects = user.Projects?.Select(x => x.ToLogicModel())
            };

        }

        public static UserViewModel ToViewModel(this UserLogicModel user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Email = user.Email,
                ImagePath = user.ImagePath,
                Projects = user.Projects?.Select(x => x.ToViewModel())
            };
        }

        public static ProjectLogicModel ToLogicModel(this ProjectViewModel project)
        {
            return new ProjectLogicModel
            {
                Id = project.Id,
                Name = project.Name,
                OwnerLogicModel = project.OwnerViewModel?.ToLogicModel(),
                Participates = project.Participates?.Select(x => x.ToLogicModel())
            };
        }

        public static ProjectViewModel ToViewModel(this ProjectLogicModel project)
        {
            return new ProjectViewModel
            {
                Id = project.Id,
                Name = project.Name,
                OwnerViewModel = project.OwnerLogicModel?.ToViewModel(),
                Participates = project.Participates?.Select(x => x.ToViewModel())
            };
        }

        public static ParticipatesViewModel ToViewModel(this ParticipatesLogicModel participates)
        {
            return new ParticipatesViewModel
            {
                UserId = participates.UserId,
                UserName = participates.UserName,
                ProjectId = participates.ProjectId,
                IsRegister = participates.IsRegister,
                Email = participates.Email
            };
        }
    }
}