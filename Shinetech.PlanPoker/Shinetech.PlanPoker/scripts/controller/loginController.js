appModule.controller('loginController', ['$scope', 'loginService', function ($scope, loginService) {

    $scope.user = {};
    $scope.message = "";

    $scope.login = function () {
        console.log($scope.user);
        loginService.login($scope.user.email, $scope.user.password, function () {

        }, function() {
            
        });
    };

    $scope.testAuthorize = function () {
        loginService.testAuthorize(function (data) {
            console.log(data.data);
        }, function () {

        });
    }
}]);


