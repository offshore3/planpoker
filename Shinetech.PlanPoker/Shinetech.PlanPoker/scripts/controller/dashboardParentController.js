appModule.controller('dashboardParentController', ['$scope', function($scope) {
    $scope.$on('dashboardParentReloadProject', function () {
        $scope.$broadcast('dashboardReloadProject');
    });
}])