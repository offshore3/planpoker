using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Castle.Core.Internal;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.IRepository;
using Shinetech.PlanPoker.LogicModel;
using Shinetech.PlanPoker.Repository.UnitOfWork;

namespace Shinetech.PlanPoker.Logic
{
    public class InviteLogic:IInviteLogic
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IInviteRepository _inviteRepository;
        private readonly IUserRepository _userRepository;

        public InviteLogic(IProjectRepository projectRepository, IUnitOfWorkFactory unitOfWorkFactory, IInviteRepository inviteRepository, IUserRepository userRepository)
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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public InviteLogicModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InviteLogicModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ParticipatesLogicModel> GetParticipatesByProjectId(int projectId)
        {
            var invites = _inviteRepository.Query().Where(x => x.Project.Id == projectId);
            var users = _userRepository.Query().Where(x => invites.Select(y => y.User.Id).Contains(x.Id));
            var participatesLogicModels = new List<ParticipatesLogicModel>();
            var invitesList = invites.ToList();
            var usersList = users.ToList();
            for (int i = 0; i < invites.Count(); i++)
            {
                var participatesLogicModel = new ParticipatesLogicModel
                {
                    Email = usersList[i].Email,
                    IsRegister = invitesList[i].IsRegister,
                    ProjectId = invitesList[i].Project.Id,
                    UserName = usersList[i].Name,
                    UserId = usersList[i].Id
                };
                participatesLogicModels.Add(participatesLogicModel);
            }
            return participatesLogicModels;
        }
    }
}
