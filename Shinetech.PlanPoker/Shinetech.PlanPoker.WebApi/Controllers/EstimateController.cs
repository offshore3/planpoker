using System.Linq;
using System.Web.Http;
using Shinetech.PlanPoker.Data.Common;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.Controllers;
using Shinetech.PlanPoker.WebApi.ViewModels;
using Shinetech.PlanPoker.WebApi.Hubs;
using Shinetech.PlanPoker.WebApi.Tools;
using System;

namespace Shinetech.PlanPoker.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class EstimateController : ApiControllerWithHub<ShinetechPlanPokerHub>
    {
        private readonly ICacheManager _cacheManager;
        private readonly IUserLogic _userLogic;

        public EstimateController(ICacheManager cacheManager, IUserLogic userLogic) : base(userLogic)
        {
            _cacheManager = cacheManager;
            _userLogic = userLogic;
        }

        [Route("estimate")]
        [HttpPost]
        [BasicAuthorize]
        public IHttpActionResult Insert(Estimate estimate)
        {
            if (_cacheManager.KeyExist(estimate.ProjectId))
            {
                UpdateEsitmate(estimate);
            }
            else
            {
                InsertEstimate(estimate);
            }
            var estimateViewModel = GetEstimateViewModel(estimate);
            var subscribed = Hub.Clients.Group(estimate.ProjectId);
            subscribed.addItem(estimateViewModel);

            return CreatedAtRoute("DefaultApi", new { controller = "estimate", id = estimate.ProjectId }, estimate);
        }



        [Route("estimateDelete")]
        [HttpDelete]
        public void Delete(string projectId)
        {
            if (_cacheManager.KeyExist(projectId)) _cacheManager.Remove(projectId);

            var subscribed = Hub.Clients.Group(projectId);
            subscribed.clearEstimate(null);
        }


        [Route("estimates")]
        [HttpGet]
        public IHttpActionResult Get(string projectId)
        {
            var estimatesViewModel = GetEstimatesViewModel(ProjectHelper.GetProjectId(projectId));

            return Ok(estimatesViewModel);
        }

        [Route("estimateShowCard")]
        [HttpGet]
        public void ShowCard(string projectId)
        {
            if (!_cacheManager.KeyExist(projectId)) return;

            var estimates = _cacheManager.Get<Estimates>(projectId);
            if (estimates != null) {
                var sum = 0;
                var notNum = 0;

                foreach (var estimate in estimates.EstimateList)
                {
                    if (!isNumberic(estimate.SelectedPoker))
                    {
                        notNum--;
                        continue;
                    }
                    sum += int.Parse(estimate.SelectedPoker);
                }
                estimates.AveragePoint = (int)Math.Ceiling((decimal)sum / (estimates.EstimateList.Count + notNum));
                estimates.IsShow = true;

                var subscribed = Hub.Clients.Group(projectId);
                subscribed.showEstimateResult(estimates);
            }            
        }

        [Route("estimateIsCleared")]
        [HttpGet]
        public bool IsCleared(string projectId)
        {
            return !_cacheManager.KeyExist(projectId);
        }

        private void UpdateEsitmate(Estimate estimate)
        {
            var estimates = _cacheManager.Get<Estimates>(estimate.ProjectId);
            var existedUser = estimates.EstimateList.FirstOrDefault(u => u.UserId == estimate.UserId);
            if (existedUser == null)
            {
                estimates.EstimateList.Add(estimate);
            }
            else
            {
                existedUser.SelectedPoker = estimate.SelectedPoker;
            }
        }

        private void InsertEstimate(Estimate estimate)
        {
            var estimates = new Estimates();
            estimates.EstimateList.Add(estimate);
            _cacheManager.Add(estimate.ProjectId, estimates);
        }

        private EstimatesViewModel GetEstimatesViewModel(string projectId)
        {
            var estimatesViewModel = new EstimatesViewModel();
            if (!_cacheManager.KeyExist(projectId))
            {
                return null;
            } 
            var estimates = _cacheManager.Get<Estimates>(projectId);
            estimatesViewModel.IsShow = estimates.IsShow;
            estimatesViewModel.AveragePoint = estimates.AveragePoint;
            if (estimates.EstimateList.Count <= 0) return estimatesViewModel;

            foreach (var item in estimates.EstimateList)
            {
                var user = _userLogic.Get(item.UserId);
                estimatesViewModel.EstimateViewModel.Add(new EstimateViewModel
                {
                    ProjectId = item.ProjectId,
                    SelectedPoker = item.SelectedPoker,
                    UserImage = user.ImagePath,
                    UserName = user.Name,
                    UserId = user.Id.ToString()
                });
            }
            return estimatesViewModel;
        }

        private EstimateViewModel GetEstimateViewModel(Estimate estimate)
        {
            var user = _userLogic.Get(estimate.UserId);
            var estimateViewModel = new EstimateViewModel
            {
                ProjectId = estimate.ProjectId,
                SelectedPoker = estimate.SelectedPoker,
                UserImage = user.ImagePath,
                UserName = user.Name,
                UserId = user.Id.ToString()
            };
            return estimateViewModel;
        }

        private bool isNumberic(string message)
        {
            var rex =new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(message))
            {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
