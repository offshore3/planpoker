using System;
using System.Collections.Generic;
using System.Linq;
using Shinetech.PlanPoker.Data.Models;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.IRepository;
using Shinetech.PlanPoker.LogicModel;
using Shinetech.PlanPoker.Repository.UnitOfWork;

namespace Shinetech.PlanPoker.Logic
{
    public class InviteLogic : IInviteLogic
    {
        private readonly IInviteRepository _inviteRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IUserRepository _userRepository;

        public InviteLogic(IProjectRepository projectRepository, IUnitOfWorkFactory unitOfWorkFactory,
            IInviteRepository inviteRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _inviteRepository = inviteRepository;
            _userRepository = userRepository;
        }


        public void Create(InviteLogicModel model)
        {
            throw new NotImplementedException();
        }

        public void Create(int projectId, string email)
        {

            var inviteModel = new InviteModel()
            {
                InviteEmail = email,
                Project = _projectRepository.Get(projectId),
                IsRegister = _userRepository.Query().Any(x=>x.Email==email),
                User = _userRepository.Query().FirstOrDefault(x=>x.Email==email)
            };
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _inviteRepository.Save(inviteModel);

                unitOfwork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _inviteRepository.Delete(id);

                unitOfwork.Commit();
            }
        }

        public InviteLogicModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public InviteLogicModel Get(int projectId, string email)
        {
            return _inviteRepository.Query().FirstOrDefault(x => x.Project.Id == projectId && x.InviteEmail==email).ToLogicModel();
        }

        public IEnumerable<InviteLogicModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ParticipatesLogicModel> GetParticipatesByProjectId(int projectId)
        {
            var invites = _inviteRepository.Query().Where(x => x.Project.Id == projectId&&x.IsRegister);
            var users = _userRepository.Query().Where(x => invites.Select(y => y.User.Id).Contains(x.Id)).ToList();

            return GetParticipateLogicModel(users, invites.ToList());
        }

        public bool CheckInviteExist(int projectId, string email)
        {
            return _inviteRepository.Query().Any(x => x.InviteEmail == email && x.Project.Id == projectId);
        }

        private IEnumerable<ParticipatesLogicModel> GetParticipateLogicModel(List<UserModel> userModels,
            List<InviteModel> inviteModels)
        {
            return inviteModels.Select((t, i) => new ParticipatesLogicModel
            {
                Id = t.Id,
                Email = userModels[i].Email,
                IsRegister = t.IsRegister,
                ProjectId = t.Project.Id,
                UserName = userModels[i].Name,
                UserId = userModels[i].Id
            }).ToList();
        }
    }
}