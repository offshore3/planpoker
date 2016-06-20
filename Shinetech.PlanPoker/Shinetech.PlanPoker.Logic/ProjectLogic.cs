using System;
using System.Collections.Generic;
using System.Linq;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.IRepository;
using Shinetech.PlanPoker.LogicModel;
using Shinetech.PlanPoker.Repository.UnitOfWork;

namespace Shinetech.PlanPoker.Logic
{
    public class ProjectLogic : IProjectLogic
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IInviteRepository _inviteRepository;

        public ProjectLogic(IProjectRepository projectRepository, IUnitOfWorkFactory unitOfWorkFactory, IInviteRepository inviteRepository)
        {
            _projectRepository = projectRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _inviteRepository = inviteRepository;
        }

        public void Create(UserLogicModel user, ProjectLogicModel model)
        {
            var projectModel = model.ToModel();
            projectModel.Owner = user.ToModel();
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _projectRepository.Save(projectModel);
                unitOfwork.Commit();
            }
        }

        public void Edit(ProjectLogicModel model)
        {
            var userModel = _projectRepository.GetForUpdate(model.Id);
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                userModel.Name = model.Name;
                userModel.Participates = model.Participates?.Select(x => x.ToModel());

                unitOfwork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var invites = _inviteRepository.Query().Where(x => x.Project.Id == id);
                foreach (var invite in invites)
                {
                    _inviteRepository.Delete(invite.Id);
                }
                _projectRepository.Delete(id);

                unitOfwork.Commit();
            }
        }

        public ProjectLogicModel Get(int id)
        {
            return _projectRepository.Get(id).ToLogicModel();
        }

        public IEnumerable<ProjectLogicModel> GetAll()
        {
            return _projectRepository.Query()?.Select(x => new ProjectLogicModel
            {
                Id = x.Id,
                Name = x.Name,
                OwnerLogicModel = x.Owner.ToLogicModel(),
                Participates = x.Participates.Select(y => y.ToLogicModel())
            });
        }

        public IEnumerable<ProjectLogicModel> GetProjectByUser(int userId, int pageNumber, int pageCount,
            string queryText)
        {
            var skipCount = (pageNumber - 1)*pageCount;
            var isQueryByText = !string.IsNullOrEmpty(queryText);
            var participateProjectIds =
                _inviteRepository.Query().Where(x => x.User.Id == userId).Select(x => x.Project.Id).ToList();
            return _projectRepository.Query()?.Where(x => (x.Owner.Id == userId || participateProjectIds.Contains(x.Id)) &&
                                                          (!isQueryByText || x.Name.Contains(queryText)))
                .OrderBy(x => x.Name)
                .Skip(skipCount)
                .Take(pageCount)
                .Select(x => new ProjectLogicModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    OwnerLogicModel = x.Owner.ToLogicModel()
                });
        }

        public int GetPages(int userId, int pageNumber, int pageCount, string queryText)
        {
            var isQueryByText = !string.IsNullOrEmpty(queryText);
            var participateProjectIds =
                _inviteRepository.Query().Where(x => x.User.Id == userId).Select(x => x.Project.Id).ToList();
            var projectCounts = _projectRepository.Query()
                .Count(x => (x.Owner.Id == userId || participateProjectIds.Contains(x.Id)) && (!isQueryByText || x.Name.Contains(queryText)));
            return (int) Math.Ceiling((decimal) projectCounts/pageCount);
        }
    }
}