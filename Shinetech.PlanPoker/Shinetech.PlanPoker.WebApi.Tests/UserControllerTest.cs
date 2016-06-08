using NUnit.Framework;
using Moq;
using Shinetech.PlanPoker.WebApi.ViewModels;
using Shinetech.PlanPoker.ILogic;

namespace Shinetech.PlanPoker.WebApi.Tests
{
   [TestFixture]
    public class UserControllerTest
    {
        private Mock<IUserLogic> _iuserLogicMock;

        [SetUp]
        public void SetUp() {
            _iuserLogicMock = new Mock<IUserLogic>();
        }

        [TearDown]
        public void TearDown() {

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
    }
}
