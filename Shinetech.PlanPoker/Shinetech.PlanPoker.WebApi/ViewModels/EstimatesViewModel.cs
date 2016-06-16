using System.Collections.Generic;

namespace Shinetech.PlanPoker.WebApi.ViewModels
{
    public class EstimatesViewModel
    {
        public EstimatesViewModel()
        {
            EstimateViewModel = new List<EstimateViewModel>();
        }

        public List<EstimateViewModel> EstimateViewModel { get; set; }
        public bool IsShow { get; set; }
    }
}