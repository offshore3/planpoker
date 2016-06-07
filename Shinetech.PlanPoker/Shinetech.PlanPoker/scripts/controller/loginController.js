appModule.controller('loginController', ['$scope', 'loginService', function ($scope, loginService) {

    $scope.user = {};
    $scope.message = "";

    $scope.login = function () {
        console.log($scope.user);
        loginService.login($scope.user, function (response) {

        });
    };

    //$scope.getAllUser = function () {
    //    loginService.getAllUser(function (data) {
    //        console.log(data);
    //    }, function () {

    //    });
    //}
}]);


