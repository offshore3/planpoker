appModule.controller('loginController', ['$scope', 'loginService', function ($scope, loginService) {

    $scope.user = {
        isError: false,
        message: ''
    };

    $scope.onlogin = function () {
        var command = {
            UserName: $scope.user.email,
            password: $scope.user.password
        };
        loginService.login(command, function () {

        }, function () {

        });
    };

    $scope.getAllUser = function () {
        loginService.getAllUser(function (data) {
            console.log(data);
        }, function () {

        });
    }
}]);


