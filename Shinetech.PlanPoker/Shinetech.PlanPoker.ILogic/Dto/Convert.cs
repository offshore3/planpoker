using System.Linq;
using Shinetech.PlanPoker.Data.Models;

namespace Shinetech.PlanPoker.ILogic.Dto
{

    public static class Convert
    {
        public static UserDto ToDto(this UserModel model)
        {
            return new UserDto
            {
                Id = model.Id,
                Name = model.Name,
                Password = model.Password,
                Email = model.Email,
                Image = model.Image,
                MyProjectsDto = model.MyProjects.Select(x => x.ToDto())
            };

        }

        public static UserModel ToModel(this UserDto user)
        {
            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Email = user.Email,
                Image = user.Image,
                MyProjects = user.MyProjectsDto.Select(x => x.ToModel())
            };
        }

        public static ProjectDto ToDto(this ProjectModel model)
        {
            return new ProjectDto
            {
                Id = model.Id,
                Name = model.Name,
                OwnerDto = model.Owner.ToDto(),
                ParticipatesDto = model.Participates.Select(x => x.ToDto())
            };
        }

        public static ProjectModel ToModel(this ProjectDto model)
        {
            return new ProjectModel
            {
                Id = model.Id,
                Name = model.Name,
                Owner = model.OwnerDto.ToModel(),
                Participates = model.ParticipatesDto.Select(x => x.ToModel())
            };
        }
    }
}
