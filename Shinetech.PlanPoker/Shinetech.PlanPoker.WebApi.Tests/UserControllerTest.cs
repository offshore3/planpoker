using NUnit.Framework;
using Moq;
using Shinetech.PlanPoker.WebApi.ViewModels;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.Controllers;

namespace Shinetech.PlanPoker.WebApi.Tests
{
   [TestFixture]
    public class UserControllerTest
    {
        private Mock<IUserLogic> _iuserLogicMock;
        private UserController _userController;

        [SetUp]
        public void SetUp() {
            _iuserLogicMock = new Mock<IUserLogic>();
            _userController = new UserController(_iuserLogicMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _iuserLogicMock = null;
            _userController = null;
        }

        [Test]
        public void Check_email_when_email_exists_should_return_true() {
            //Arrange
            var userViewModelMock = new UserViewModel
            {
                Email="123456@qq.com",
                Password="123456"
            };

            
            //Act

            //Assert
        }

        [Test]
        public void Check_email_when_email_does_not_exist_should_return_false() {
            //Arrange
            var userViewModelMock = new UserViewModel
            {
                Email = "123456@qq.com",
                Password = "123456"
            };
            //Act

            //Assert
        }

        [Test]
        public void Check_email_when_email_does_not_exist_should_return_false_and_create_user()
        {
            //Arrange
            var userViewModelMock = new UserViewModel
            {
                Email = "123456@qq.com",
                Password = "123456"
            };
            //Act

            //Assert
        }

        [Test]
        public void Login_should_call_Login_method_once_in_user_controller()
        {
            //Arrange
            var userViewModel = new UserViewModel
            {
                Email = "123456@qq.com",
                Password = "123456"
            };

            //Act
            _userController.Login(userViewModel.Email, userViewModel.Password);

            //Assert
            _iuserLogicMock.Verify(x => x.Login(It.IsAny<string>(),It.IsAny<string>()), Times.Once);
        }
    }
}
