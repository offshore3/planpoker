using Moq;
using NUnit.Framework;
using Shinetech.PlanPoker.LogicModel;
using Shinetech.PlanPoker.IRepository;
using Shinetech.PlanPoker.Data.Models;
using Shinetech.PlanPoker.Repository.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace Shinetech.PlanPoker.Logic.Tests
{
    [TestFixture]
    public class UserLogicTest
    {
        private Mock<IUserRepository> _iuserRepositoryMock;
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
        private Mock<IUnitOfWork> _uniteOfWorkMock;

        [SetUp]
        public void SetUp() {
            _iuserRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            _uniteOfWorkMock = new Mock<IUnitOfWork>();
        }

        [TearDown]
        public void TearDown() {

        }

        [Test]
        public void Check_email_when_email_exists_should_return_true() {

            //Arrange
            var email = "123456@qq.com";
            var users = new List<UserModel>();
            var userLogicModel = new UserLogicModel
            {
                Email = "123456@qq.com",
                Password = "123456"
            };
            var userLogic =new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.ToModel();
            users.Add(user);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x => x.Query()).Returns(users.AsQueryable());
            //Act
            var result = userLogic.CheckEmailExist(email);
            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Check_email_when_email_does_not_exist_should_return_false()
        {
            //Arrange
            var email = "1234567@qq.com";
            var users = new List<UserModel>();
            var userLogicModel = new UserLogicModel
            {
                Email = "123456@qq.com",
                Password = "123456"
            };
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.ToModel();
            users.Add(user);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x => x.Query()).Returns(users.AsQueryable());
            //Act
            var result = userLogic.CheckEmailExist(email);
            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Create_user_when_email_does_not_exist_check_email_should_return_false() {
            //Arrange
            var userLogicModel = new UserLogicModel
            {
                Email = "123456@qq.com",
                Password = "123456"
            };
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.ToModel();
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            //Act
            userLogic.Create(userLogicModel);
            //Assert
            
        }
    }
}
