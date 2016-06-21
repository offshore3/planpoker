appModule.controller('resetPasswordController', ['$scope', 'resetPasswordService', 'retrievePasswordService','$location', function ($scope, resetPasswordService,retrievePasswordService, $location) {
    $scope.user = {};

    $scope.initialize = function () {
        $scope.currentTime = new Date();
        $scope.resetPasswordToken = getQueryVariable('code');
        if ($scope.resetPasswordToken) {
            retrievePasswordService.decryptResetPasswordCode($scope.resetPasswordToken, function (data) {
                var resultArray = data.split('&');
                resetPasswordService.getUserByEmail(resultArray[0], function (data) {
                    $scope.user = data;
                }, function () {

                });
            }, function () {

            });
        }
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


