appModule.controller('loginController', ['$scope','$location','loginService', function ($scope,$location, loginService) {

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

}]);


