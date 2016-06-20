using System.Collections.Generic;

namespace Shinetech.PlanPoker.WebApi.ViewModels
{
    public class Estimates
    {
        public Estimates()
        {
            EstimateList = new List<Estimate>();
            IsShow = false;
            AveragePoint = 0;
        }

        public List<Estimate> EstimateList { get; set; }

        public bool IsShow { get; set; }
        public int AveragePoint { get; set; }

    }
}