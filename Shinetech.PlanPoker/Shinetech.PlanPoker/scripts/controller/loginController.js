appModule.controller('loginController', ['$scope','$location','loginService', function ($scope,$location, loginService) {

    $scope.user = {};
    $scope.message = "";
    $scope.isBusy = false;

    $scope.login = function () {
        $scope.isBusy = true;
        loginService.login($scope.user.email, $scope.user.password, function () {
            $location.path("/dashboard");
            $scope.isBusy = false;
        }, function () {
            $scope.isError = true;
            $scope.message = "The email or password is wrong!";
            $scope.isBusy = false;
        });
    };

}]);


