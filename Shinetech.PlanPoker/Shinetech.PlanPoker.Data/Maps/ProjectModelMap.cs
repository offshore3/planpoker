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
        }
    }
}
