appModule.controller('resetPasswordController', ['$scope', 'resetPasswordService', '$location', function ($scope, resetPasswordService, $location) {
    $scope.user = {};
    $scope.resetPasswordEmail = getQueryVariable('code');
    
    $scope.initialize= function() {
        resetPasswordService.getUserByEmail($scope.resetPasswordEmail, function(data) {
            $scope.user = data;
        }, function() {

        });
    }

    $scope.resetPassword= function() {
        $scope.user.password = $scope.newPassword;
        resetPasswordService.resetUserPassword($scope.user, function () {
            $location.url("");
            $location.path("/login");
        }, function () {
        });
    }
}]);


