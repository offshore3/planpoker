appModule.controller('monitorController', ['$rootScope', '$scope', '$cookieStore', 'dashboardService', function ($rootScope, $scope, $cookieStore, dashboardService) {
    $scope.customerIdSubscribed;
    $scope.projectId = getQueryVariable('code');

    dashboardService.getEstimateUsers($scope.projectId, function (data) {
        if ($scope.customerIdSubscribed &&
                $scope.customerIdSubscribed.length > 0 &&
                $scope.customerIdSubscribed !== $scope.projectId) {
            hub.server.unsubscribe($scope.customerIdSubscribed);
        }
        hub.server.subscribe($scope.projectId);
        $scope.customerIdSubscribed = $scope.projectId;
        if (data == null) {
            $rootScope.estimates = [];
            return;
        }
        $rootScope.estimates = data.EstimateViewModel;
        $rootScope.isShowResult = data.IsShow;
    }, function () {
        $rootScope.estimates = [];
    });

}
])