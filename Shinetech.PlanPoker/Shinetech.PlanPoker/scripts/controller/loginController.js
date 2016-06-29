appModule.controller('loginController', ['$scope', '$location', 'loginService', 'authorizationService', function ($scope, $location, loginService, authorizationService) {

    $scope.user = {};
    $scope.message = "";
    $scope.isLoginBusy = false;

    $scope.login = function () {
        $scope.isLoginBusy = true;
        loginService.login($scope.user.email, $scope.user.password, function () {
            $location.path("/dashboard");
            $scope.isLoginBusy = false;
        }, function () {
            $scope.isError = true;
            $scope.message = "The email or password is wrong!";
            $scope.isLoginBusy = false;
        });
    };

    $scope.authorization = getQueryVariable('authorization');
    if ($scope.authorization) {
        authorizationService.setAuthorization($scope.authorization);
        location.href = "/#/dashboard";
    }
    
}]);


