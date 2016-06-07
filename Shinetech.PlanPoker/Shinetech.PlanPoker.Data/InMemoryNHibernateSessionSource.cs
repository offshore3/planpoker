using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Shinetech.PlanPoker.Data
{
    public class InMemoryNHibernateSessionSource : INHibernateSessionSource, IDisposable
    {
        private readonly NHibernate.Cfg.Configuration _configuration;
        private readonly ISessionFactory _factory;
        private readonly ISession _connectionCreatingSession;

        public InMemoryNHibernateSessionSource(IEnumerable<Assembly> mappingAssemblies)
        {
            _configuration = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString("FullUri=file:memorydb.db?mode=memory&cache=shared"))
                .ExposeConfiguration(
                c =>
                {
                    c.SetProperty(NHibernate.Cfg.Environment.CommandTimeout,
                      TimeSpan.FromMinutes(3).TotalSeconds.ToString(CultureInfo.InvariantCulture));
                    new SchemaExport(c).Execute(true, true, false);
                })
                .Mappings(x =>
                {
                    foreach (var mappingAssembly in mappingAssemblies)
                    {
                        x.FluentMappings.AddFromAssembly(mappingAssembly);
                    }
                }).BuildConfiguration();

            _factory = _configuration.BuildSessionFactory();
            _connectionCreatingSession=_factory.OpenSession();
            var connection = _connectionCreatingSession.Connection;

            new SchemaExport(_configuration).Execute(
                               false,
                               true,
                              justDrop: false,
                              connection: connection,
                              exportOutput: null);
            var export = new SchemaUpdate(_configuration);
            export.Execute(true, true);
        }

        public ISession OpenSession() { return _factory.OpenSession(); }
        public NHibernate.Cfg.Configuration GetConfiguration()
        {
            return _configuration;
        }

        public void Dispose()
        {
            _connectionCreatingSession.Dispose();
        }
    }
}