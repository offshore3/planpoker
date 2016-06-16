using System.Collections.Generic;

namespace Shinetech.PlanPoker.WebApi.ViewModels
{
    public class Estimates
    {
        public Estimates()
        {
            EstimateList = new List<Estimate>();
            IsShow = false;
        }

        public List<Estimate> EstimateList { get; set; }

        public bool IsShow { get; set; }

    }
}