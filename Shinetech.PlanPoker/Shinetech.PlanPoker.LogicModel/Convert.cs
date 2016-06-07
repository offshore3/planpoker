using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shinetech.PlanPoker.Data.Models;

namespace Shinetech.PlanPoker.LogicModel
{
    public static class Convert
    {
        public static UserLogicModel ToLogicModel(this UserModel user)
        {
            return new UserLogicModel
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Email = user.Email,
                ImagePath = user.ImagePath,
                Projects = user.Projects?.Select(x=>x.ToLogicModel())
            };

        }

        public static UserModel ToModel(this UserLogicModel user)
        {
            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Email = user.Email,
                Projects = user.Projects?.Select(x => x.ToModel())
            };
        }

        public static ProjectLogicModel ToLogicModel(this ProjectModel project)
        {
            return new ProjectLogicModel
            {
                Id = project.Id,
                Name = project.Name,
                OwnerLogicModel = project.Owner.ToLogicModel(),
                Participates = project.Participates?.Select(x=>x.ToLogicModel())
            };
        }

        public static ProjectModel ToModel(this ProjectLogicModel project)
        {
            return new ProjectModel
            {
                Id = project.Id,
                Name = project.Name,
                Owner = project.OwnerLogicModel.ToModel(),
                Participates = project.Participates?.Select(x => x.ToModel())
            };
        }
    }
}
