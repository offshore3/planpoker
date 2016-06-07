using FluentNHibernate.Mapping;
using Shinetech.PlanPoker.Data.Models;

namespace Shinetech.PlanPoker.Data.Maps
{
    public class UserModelMap : ClassMap<UserModel>
    {
        public UserModelMap()
        {
            Table("UserModel");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Email);
            Map(x => x.Password);
        }
    }
}
