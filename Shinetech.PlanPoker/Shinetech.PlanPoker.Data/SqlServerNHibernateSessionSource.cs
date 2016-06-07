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

    public class SqlServerNHibernateSessionSource : INHibernateSessionSource
    {
        private readonly NHibernate.Cfg.Configuration _configuration;
        private readonly ISessionFactory _factory;

        public SqlServerNHibernateSessionSource(string connectionString, IEnumerable<Assembly> mappingAssemblies)
        {
            _configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
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
            var export = new SchemaUpdate(_configuration);
            export.Execute(true, true);
        }

        public ISession OpenSession() { return _factory.OpenSession(); }
        public NHibernate.Cfg.Configuration GetConfiguration()
        {
            return _configuration;
        }
    }
}