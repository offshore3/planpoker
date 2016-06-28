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
            Map(m => m.Name).Nullable();
            Map(m => m.Email);
            Map(m => m.Password);
            Map(m => m.ImagePath).Nullable();
            Map(m => m.ResetPasswordToken).Nullable();
            Map(m => m.OpenId).Nullable();
            Map(m=>m.ExpiredTime).Nullable();
            HasManyToMany(x => x.Projects).Table("ProjectUserMap").AsSet();
        }
    }
}
