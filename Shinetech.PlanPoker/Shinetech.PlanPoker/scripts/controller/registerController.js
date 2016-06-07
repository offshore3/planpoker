appModule.controller('registerController', ['$scope', 'registerService', function ($scope, registerService) {

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


