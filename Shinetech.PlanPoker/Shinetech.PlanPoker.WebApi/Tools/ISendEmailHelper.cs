using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shinetech.PlanPoker.WebApi.ViewModels;

namespace Shinetech.PlanPoker.WebApi.Tools
{
    public interface ISendEmailHelper
    {
        bool SendEmail(SendEmailViewModel sendEmailViewModel);
    }
}
