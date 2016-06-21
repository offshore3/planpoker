appModule.controller('loginController', ['$scope','$location','loginService', function ($scope,$location, loginService) {

    $scope.user = {};
    $scope.message = "";

    $scope.login = function () {
        loginService.login($scope.user.email, $scope.user.password, function () {
            $location.path("/dashboard");
        }, function () {
            $scope.isError = true;
            $scope.message = "The email or password is wrong!";
        });
    };

}]);


