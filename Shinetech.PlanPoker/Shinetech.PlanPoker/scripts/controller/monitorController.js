appModule.controller('monitorController', ['$rootScope', '$scope', '$timeout','$routeParams', 'dashboardService', function ($rootScope, $scope, $timeout,$routeParams, dashboardService) {
    $scope.customerIdSubscribed;
    $scope.webAPI = webAPI;
    
    $scope.initMonitor = function () {
        $scope.projectCode = $routeParams.projectCode;
        var projectId = $scope.projectCode.split('-')[0];
        dashboardService.getEstimateUsers(projectId, function (data) {
            if ($scope.customerIdSubscribed &&
                    $scope.customerIdSubscribed.length > 0 &&
                    $scope.customerIdSubscribed !== projectId) {
                hub.server.unsubscribe($scope.customerIdSubscribed);
            }
            hub.server.subscribe(projectId);
            $scope.customerIdSubscribed = projectId;
            if (data == null) {
                $rootScope.estimates = [];
                return;
            }
            $rootScope.estimates = data.EstimateViewModel;
            $rootScope.isShowResult = data.IsShow;
            $rootScope.averagePoint = data.AveragePoint;
        }, function () {
            $rootScope.estimates = [];
        });
    }
    setTimeout($scope.initMonitor, 500);
}
])