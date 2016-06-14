using FluentNHibernate.Mapping;
using Shinetech.PlanPoker.Data.Models;

namespace Shinetech.PlanPoker.Data.Maps
{
    public class InviteModelMap : ClassMap<InviteModel>
    {
        public InviteModelMap()
        {
            Table("InviteModel");
            LazyLoad();
            Id(x => x.Id);
            Map(m => m.IsRegister);
            Map(m => m.InviteEmail);
            References(x => x.User).Column("UserId");
            References(x => x.Project).Column("ProjectId");
        }
    }
}
