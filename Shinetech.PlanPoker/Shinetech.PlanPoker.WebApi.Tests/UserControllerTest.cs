using Moq;
using NUnit.Framework;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.Controllers;
using Shinetech.PlanPoker.WebApi.Tools;
using Shinetech.PlanPoker.WebApi.ViewModels;

namespace Shinetech.PlanPoker.WebApi.Tests
{
   [TestFixture]
    public class UserControllerTest
    {
        private Mock<IUserLogic> _iuserLogicMock;
        private UserController _userController;
       private Mock<ISendEmailHelper> _sendEmailHelper;

        [SetUp]
        public void SetUp() {
            _iuserLogicMock = new Mock<IUserLogic>();
            _sendEmailHelper=new Mock<ISendEmailHelper>();
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
        [Test]
        public void Edit_user_name_and_imagepath_should_edit_user()
        {
            //Arrange
            var userViewModel = new UserViewModel
            {
                Id = 1,
                Name = "Bill Gates",
                ImagePath = "/upload/aaa.jpg"
            };
            var userController = new UserController(_iuserLogicMock.Object);
            var userLogicModel = userViewModel.ToLogicModel();
            //Act
            userController.EditUser(userViewModel);
            //Assert
            _iuserLogicMock.Verify(x => x.Edit(userLogicModel), Times.AtMostOnce);
        }

        [Test]
        public void Edit_user_password_should_edit_user()
        {
            //Arrange
            var userViewModel = new UserViewModel
            {
                Id = 1,
                Email="4324@qq.com",
                Password = "123456"
            };
            var userController = new UserController(_iuserLogicMock.Object);
            var userLogicModel = userViewModel.ToLogicModel();
            //Act
            userController.EditUser(userViewModel);
            //Assert
            _iuserLogicMock.Verify(x => x.EditPassword(userLogicModel), Times.AtMostOnce);
        }

        [Test]
        public void GetUserByEmail_should_return_user_user_email()
        {
            //Arrange
            var email = "100@qq.com";
            var userController = new UserController(_iuserLogicMock.Object);
            //Act
            var user = userController.GetUserByEmail(email);
            //Assert
            _iuserLogicMock.Verify(x => x.GetUserByEmail(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ResetPassword_should_change_user_password()
        {
            //Arrange
            var userViewModel = new UserViewModel
            {
                Id = 1,
                Email = "100@qq.com",
                Password = "123456"
            };
            var userLogicModel = userViewModel.ToLogicModel();

            var userController = new UserController(_iuserLogicMock.Object);
            //Act
            userController.ResetPassword(userViewModel);
            //Assert
            _iuserLogicMock.Verify(x => x.EditPassword(userLogicModel), Times.AtMostOnce);
        }

        [Test]
        public void EditUserPassword_should_change_user_password()
        {
            //Arrange
            var userViewModel = new UserViewModel
            {
                Id = 1,
                Email = "100@qq.com",
                Password = "123456"
            };
            var userLogicModel = userViewModel.ToLogicModel();

            var userController = new UserController(_iuserLogicMock.Object);
            //Act
            userController.EditUserPassword(userViewModel);
            //Assert
            _iuserLogicMock.Verify(x => x.EditPassword(userLogicModel), Times.AtMostOnce);
        }

        [Test]
        public void SendEmail_should_change_user_password()
        {
            //Arrange
            var sendEmailViewModel = new SendEmailViewModel
            {
                MailViewModel=new MailViewModel()
                {
                    WebName = "123",
                    WebUrl="123",
                    WebTel="123",
                    AbsUrl="123",
                    EmailTo="123"
                },
                MailContentViewModel = new MailContentViewModel()
                {
                    Content = "",
                    MailTitle = "",
                    Title = ""
                }
            };

            var userController = new UserController(_iuserLogicMock.Object);
            //Act
            userController.SendEmail(sendEmailViewModel);
            //Assert
            _sendEmailHelper.Verify(x => x.SendEmail(It.IsAny<SendEmailViewModel>()), Times.AtMostOnce);
        }


        [Test]
        public void GetUsers_should_return_all_users()
        {
            //Arrange

            var userController = new UserController(_iuserLogicMock.Object);
            //Act
            userController.GetUsers();
            //Assert
            _iuserLogicMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetUserViewModel_should_return_logged_user()
        {
            //Arrange

            var userController = new UserController(_iuserLogicMock.Object);
            //Act
            var result = userController.GetUserViewModel();
            //Assert
            Assert.IsNull(result);
        }
        
    }
}
