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

    $scope.$on('socialLoginSuccess', function (d, data) {
        $scope.user.email = null;
        $scope.user.Name = data.firstName + " " + data.lastName;
        $scope.user.OpenId = data.id;
        $scope.user.Password = null;
        $scope.user.ComfirmPassword = null;
        loginService.getOrCreateUser($scope.user, function(parameters) {
            $location.path("/dashboard");
        }, function(parameters) {

        });
    });

}]);


