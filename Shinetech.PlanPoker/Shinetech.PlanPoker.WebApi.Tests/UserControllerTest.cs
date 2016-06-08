using Moq;
using NUnit.Framework;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.Controllers;
using Shinetech.PlanPoker.WebApi.ViewModels;

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
            var userController = new UserController(_iuserLogicMock.Object);
            _iuserLogicMock.Setup(x => x.CheckEmailExist(userViewModelMock.Email)).Returns(true);
            //Act
            var result = userController.CheckEmailExist(userViewModelMock.Email);
            //Assert
            _iuserLogicMock.Verify(x => x.CheckEmailExist(userViewModelMock.Email), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void Check_email_when_email_does_not_exist_should_return_false() {
            //Arrange
            var userViewModelMock = new UserViewModel
            {
                Email = "123456@qq.com",
                Password = "123456"
            };
            var userController = new UserController(_iuserLogicMock.Object);
            _iuserLogicMock.Setup(x => x.CheckEmailExist(userViewModelMock.Email)).Returns(false);
            //Act
            var result = userController.CheckEmailExist(userViewModelMock.Email);
            //Assert
            _iuserLogicMock.Verify(x => x.CheckEmailExist(userViewModelMock.Email), Times.Once);
            Assert.IsFalse(result);
        }

        [Test]
        public void Create_user_when_email_does_not_exist_check_email_should_return_false()
        {
            //Arrange
            var userViewModelMock = new UserViewModel
            {
                Email = "123456@qq.com",
                Password = "123456"
            };
            var userController = new UserController(_iuserLogicMock.Object);
            _iuserLogicMock.Setup(x => x.CheckEmailExist(userViewModelMock.Email)).Returns(false);
            var userLogicModel = userViewModelMock.ToLogicModel();
            _iuserLogicMock.Setup(x=>x.Create(userLogicModel));
            //Act
            var result = userController.CheckEmailExist(userViewModelMock.Email);
            userController.Create(userViewModelMock);
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
