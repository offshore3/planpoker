using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Shinetech.PlanPoker.WebApi.Startup))]

namespace Shinetech.PlanPoker.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
