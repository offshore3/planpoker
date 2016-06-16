using Moq;
using NUnit.Framework;
using Shinetech.PlanPoker.Data.Models;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.IRepository;
using Shinetech.PlanPoker.LogicModel;
using Shinetech.PlanPoker.Repository.UnitOfWork;

namespace Shinetech.PlanPoker.Logic.Tests
{
    [TestFixture]
    public class ProjectLogicTest
    {
        private Mock<IProjectRepository> _projectRepository;
        private Mock<IInviteRepository> _inviteRepository;
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
        private Mock<IUnitOfWork> _uniteOfWorkMock;
        private IProjectLogic _projectLogic;

        [SetUp]
        public void SetUp()
        {
            _projectRepository = new Mock<IProjectRepository>();
            _inviteRepository = new Mock<IInviteRepository>();
            _unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            _uniteOfWorkMock = new Mock<IUnitOfWork>();
            _projectLogic = new ProjectLogic(_projectRepository.Object, _unitOfWorkFactory.Object, _inviteRepository.Object);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _projectRepository = null;
            _uniteOfWorkMock = null;
            _unitOfWorkFactory = null;
        }

        [Test]
        public void Create_will_create_project_by_call_Create_method_in_project_logic()
        {

            //Arrange
            var projectLogicModel = new ProjectLogicModel
            {
                Name = "project1"
            };
            var userModel = new UserLogicModel
            {
                Id = 2,
                Email = "test",
                Name = "joy"
            };
            
            //Act
            _projectLogic.Create(userModel,projectLogicModel);
            //Assert
            _projectRepository.Verify(x=>x.Save(It.IsAny<ProjectModel>()),Times.Once);
        }
    }
}