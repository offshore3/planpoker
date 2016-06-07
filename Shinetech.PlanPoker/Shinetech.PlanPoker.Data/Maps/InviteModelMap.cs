using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Shinetech.PlanPoker.Data.Models;

namespace Shinetech.PlanPoker.Data.Maps
{
    public class InviteModelMap: ClassMap<InviteModel>
    {
        public InviteModelMap()
        {
            Table("InviteModel");
            LazyLoad();
            Id(x => x.Id);
            Map(m => m.IsRegister);
            References(x => x.User).Column("UserId");
            References(x => x.Project).Column("ProjectId");
        }
    }
}
