appModule.controller('resetPasswordController', ['$scope', 'resetPasswordService', '$location', function ($scope, resetPasswordService, $location) {
    $scope.user = {};
    $scope.resetPasswordEmail = getQueryVariable('email');
    
    $scope.initialize= function() {
        resetPasswordService.getUserByEmail($scope.resetPasswordEmail, function(data) {
            $scope.user = data;
        }, function() {

        });
    }
    if ($scope.resetPasswordEmail != null) {
        $scope.initialize();
    }

    $scope.resetPassword= function() {
        $scope.user.password = $scope.newPassword;
        resetPasswordService.resetUserPassword($scope.user, function () {
            $location.path("/login");
        }, function () {
        });
    }
}]);


