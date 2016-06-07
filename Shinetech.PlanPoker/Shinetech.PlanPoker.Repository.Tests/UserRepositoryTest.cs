using System;
using System.Linq;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Shinetech.PlanPoker.Data;
using Shinetech.PlanPoker.Data.Models;
using Shinetech.PlanPoker.IRepository;
using Shinetech.PlanPoker.Repository.Installer;

namespace Shinetech.PlanPoker.Repository.Tests
{
    public class UserRepositoryTest
    {
        private WindsorContainer _container;
        private IDisposable _scope;
        [SetUp]
        public void Setup()
        {
            _container = new WindsorContainer();
            RepositoryInstaller.InstallForTest(_container);
            _scope = _container.BeginScope();
        }

        [Test]
        public void Save_user()
        {
            var userRepository = _container.Resolve<IUserRepository>();
            userRepository.Save(new UserModel
            {
                Email = "123",
                Name = "123",
                Password = "123"
            });
            var userModels = userRepository.Query().Where(x => x.Email == "123").ToList();
            Assert.AreEqual(1, userModels.Count);
        }

        [TearDown]
        public void DropTable()
        {
            _scope.Dispose();
            var configuration = _container.Resolve<INHibernateSessionSource>().GetConfiguration();
            var export = new SchemaExport(configuration);
            export.Drop(false, true);
        }
    }
}
