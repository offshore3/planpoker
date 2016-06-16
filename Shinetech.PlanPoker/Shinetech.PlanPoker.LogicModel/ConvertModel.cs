using System.Linq;
using Shinetech.PlanPoker.Data.Models;

namespace Shinetech.PlanPoker.LogicModel
{
    public static class ConvertModel
    {
        public static UserLogicModel ToLogicModel(this UserModel user)
        {
            return user==null?null: new UserLogicModel
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Email = user.Email,
                ImagePath = user.ImagePath,
                Projects = user.Projects?.Select(x => x.ToLogicModel())
            };
        }

        public static UserModel ToModel(this UserLogicModel user)
        {
            return user == null ? null : new UserModel
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
            return project == null ? null : new ProjectLogicModel
            {
                Id = project.Id,
                Name = project.Name,
                OwnerLogicModel = project.Owner?.ToLogicModel(),
                Participates = project.Participates?.Select(x => x.ToLogicModel())
            };
        }

        public static ProjectModel ToModel(this ProjectLogicModel project)
        {
            return project == null ? null : new ProjectModel
            {
                Id = project.Id,
                Name = project.Name,
                Owner = project.OwnerLogicModel?.ToModel(),
                Participates = project.Participates?.Select(x => x.ToModel())
            };
        }

        public static InviteLogicModel ToLogicModel(this InviteModel invite)
        {
            return invite == null ? null : new InviteLogicModel
            {
                Id=invite.Id,
                InviteEmail= invite.InviteEmail,
                IsRegister=invite.IsRegister,
                Project=invite.Project.ToLogicModel(),
                User=invite.User.ToLogicModel()
            };
        }
    }
}