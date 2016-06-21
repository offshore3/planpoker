using System.Linq;
using Shinetech.PlanPoker.LogicModel;

namespace Shinetech.PlanPoker.WebApi.ViewModels
{
    public static class ConvertModel
    {
        public static UserLogicModel ToLogicModel(this UserViewModel user)
        {
            return user == null ? null : new UserLogicModel
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                ComfirmPassword=user.ComfirmPassword,
                Email = user.Email,
                ImagePath = user.ImagePath,
                ResetPasswordToken=user.ResetPasswordToken,
                ExpiredTime=user.ExpiredTime,
                Projects = user.Projects?.Select(x => x.ToLogicModel())
            };
        }

        public static UserViewModel ToViewModel(this UserLogicModel user)
        {
            return user == null ? null : new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                ComfirmPassword=user.ComfirmPassword,
                Email = user.Email,
                ImagePath = user.ImagePath,
                ResetPasswordToken=user.ResetPasswordToken,
                ExpiredTime=user.ExpiredTime,
                Projects = user.Projects?.Select(x => x.ToViewModel())
            };
        }

        public static ProjectLogicModel ToLogicModel(this ProjectViewModel project)
        {
            return project == null ? null : new ProjectLogicModel
            {
                Id = project.Id,
                Name = project.Name,
                OwnerLogicModel = project.OwnerViewModel?.ToLogicModel(),
                Participates = project.Participates?.Select(x => x.ToLogicModel())
            };
        }

        public static ProjectViewModel ToViewModel(this ProjectLogicModel project)
        {
            return project == null ? null : new ProjectViewModel
            {
                Id = project.Id,
                Name = project.Name,
                OwnerViewModel = project.OwnerLogicModel?.ToViewModel(),
                Participates = project.Participates?.Select(x => x.ToViewModel())
            };
        }

        public static ParticipatesViewModel ToViewModel(this ParticipatesLogicModel participates)
        {
            return participates == null ? null : new ParticipatesViewModel
            {
                Id = participates.Id,
                UserId = participates.UserId,
                UserName = participates.UserName,
                ProjectId = participates.ProjectId,
                IsRegister = participates.IsRegister,
                Email = participates.Email
            };
        }

        public static InviteViewModel ToViewModel(this InviteLogicModel invite)
        {
            return invite == null ? null : new InviteViewModel
            {
                Id=invite.Id,
                InviteEmail = invite.InviteEmail,
                IsRegister =invite.IsRegister,
                Project=invite.Project.ToViewModel(),
                User=invite.User.ToViewModel()
            };
        }

        public static SendEmailLogicModel ToLogicModel(this SendEmailViewModel email)
        {
            return email == null ? null : new SendEmailLogicModel
            {
                MailLogicModel = email.MailViewModel.ToLogicModel(),
                MailContentLogicModel = email.MailContentViewModel.ToLogicModel()
            };
        }

        public static MailLogicModel ToLogicModel(this MailViewModel model)
        {

            return model == null ? null : new MailLogicModel
            {
                WebName = model.WebName,
                WebUrl = model.WebUrl,
                WebCompany = model.WebCompany,
                WebAddress = model.WebAddress,
                WebTel = model.WebTel,
                EmailSmtp = model.EmailSmtp,
                EmailSsl = model.EmailSsl,
                EmailPort = model.EmailPort,
                EmailFrom = model.EmailFrom,
                EmailTo = model.EmailTo,
                EmailCode = model.EmailCode,
                EmailUserName = model.EmailUserName,
                EmailPassWord = model.EmailPassWord,
                EmailNickName = model.EmailNickName,
                AbsUrl = model.AbsUrl
            };
        }

        public static MailContentLogicModel ToLogicModel(this MailContentViewModel model)
        {
            return model == null ? null : new MailContentLogicModel
            {
                Title= model.Title,
                MailTitle=model.MailTitle,
                Content=model.Content
            };
        }
        

    }
}