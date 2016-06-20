using System.Collections.Generic;

namespace Shinetech.PlanPoker.WebApi.ViewModels
{
    public class EstimatesViewModel
    {
        public EstimatesViewModel()
        {
            EstimateViewModel = new List<EstimateViewModel>();
            AveragePoint = 0;
        }

        public List<EstimateViewModel> EstimateViewModel { get; set; }
        public bool IsShow { get; set; }
        public int AveragePoint { get; set; }
    }
}