using FluentNHibernate.Mapping;
using Shinetech.PlanPoker.Data.Models;

namespace Shinetech.PlanPoker.Data.Maps
{
    public class UserModelMap : ClassMap<UserModel>
    {
        public UserModelMap()
        {
            Table("UserModel");
            LazyLoad();
            Id(x => x.Id);
            Map(m => m.Password);
            Map(m => m.Email);
            Map(m => m.Name).Nullable();
            Map(m => m.Image).Length(8000).Nullable();
            HasMany(x => x.MyProjects).Cascade.AllDeleteOrphan().Inverse().Table("Project").KeyColumn("ProjectId");
            HasManyToMany(x => x.ParticipatedProjects).Table("Project").AsSet();
        }
    }
}
