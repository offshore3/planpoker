using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Shinetech.PlanPoker.Data.Models;

namespace Shinetech.PlanPoker.Data.Maps
{
    public class ProjectModelMap : ClassMap<ProjectModel>
    {
        public ProjectModelMap()
        {
            Table("ProjectModel");
            LazyLoad();
            Id(x => x.Id);
            Map(m => m.Name);
            References(x => x.Owner).Column("UserId");
            HasManyToMany(x => x.Participates).Table("Project").AsSet();
        }
    }
}
