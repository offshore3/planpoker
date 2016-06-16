using Moq;
using NUnit.Framework;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.Controllers;

namespace Shinetech.PlanPoker.WebApi.Tests
{
    [TestFixture]
    public class InviteControllerTest
    {
        private Mock<IInviteLogic> _inviteLogicMock;
        private Mock<IUserLogic> _userLogicMock;
        private InviteController _inviteController;
        [SetUp]
        public void SetUp()
        {
            _inviteLogicMock = new Mock<IInviteLogic>();
            _userLogicMock = new Mock<IUserLogic>();
            _inviteController=new InviteController(_userLogicMock.Object, _inviteLogicMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _inviteLogicMock = null;
            _userLogicMock = null;
            _inviteController = null;
        }

        [Test]
        public void GetParticipatesByProjectId_will_return_participates_in_this_project()
        {
            //Arrange
            var projectId = 1;
            //Act
            var result = _inviteController.GetParticipatesByProjectId(projectId);
            //Assert
            _inviteLogicMock.Verify(x => x.GetParticipatesByProjectId(It.IsAny<int>()), Times.Once);
            Assert.IsEmpty(result);
        }

        [Test]
        public void DeleteParticipates_will_delete_participates_in_this_project_by_participateId()
        {
            //Arrange
            var participateId = 1;
            //Act
            _inviteController.DeleteParticipates(participateId);
            //Assert
            _inviteLogicMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }
        
    }
}
