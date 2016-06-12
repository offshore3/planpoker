using Moq;
using NUnit.Framework;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.LogicModel;
using Shinetech.PlanPoker.WebApi.Controllers;
using Shinetech.PlanPoker.WebApi.ViewModels;

namespace Shinetech.PlanPoker.WebApi.Tests
{
    public class ProjectControllerTest
    {
        private Mock<IProjectLogic> _projectLogicMock;
        private Mock<IUserLogic> _userLogicMock;
        private ProjectController _projectController;
        [SetUp]
        public void SetUp()
        {
            _projectLogicMock = new Mock<IProjectLogic>();
            _userLogicMock = new Mock<IUserLogic>();
            _projectController = new ProjectController(_userLogicMock.Object, _projectLogicMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _projectLogicMock = null;
            _userLogicMock = null;
            _projectController = null;
        }

        [Test]
        public void DeleteProject_should_call_DeleteProject_method_once_in_project_controller()
        {
            //Arrange

            //Act
            _projectController.DeleteProject(It.IsAny<int>());
            //Assert
            _projectLogicMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        [Ignore("This call is a web request.")]
        public void CreateProject_should_call_CreateProject_method_once_in_project_controller()
        {
            //Arrange
            ProjectLogicModel model = new ProjectLogicModel
            {
                Name = "Test1",
                Id = 5
            };
            //Act
            _projectController.CreateProject(model.ToViewModel());
            //Assert
            _projectLogicMock.Verify(x => x.Create(It.IsAny<UserLogicModel>(),It.IsAny<ProjectLogicModel>()), Times.Once);
        }

        [Test]
        public void EditProject_should_call_EditProject_method_once_in_project_controller()
        {
            //Arrange
            ProjectLogicModel model = new ProjectLogicModel
            {
                Name = "Test1",
                Id = 5
            };
            //Act
            _projectController.EditProject(model.ToViewModel());
            //Assert
            _projectLogicMock.Verify(x => x.Edit(It.IsAny<ProjectLogicModel>()), Times.Once);
        }

    }
}