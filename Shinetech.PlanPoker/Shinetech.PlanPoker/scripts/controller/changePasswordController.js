appModule.controller('changePasswordController', ['$scope','$location', '$cookieStore', 'changePasswordService', 'loginService', function ($scope, $location, $cookieStore, changePasswordService, loginService) {

    $scope.user = {};

    $scope.changePassword = function () {
        loginService.getUser(function (data) {
            if (data != null && data.Password === $scope.user.oldpassword) {
                data.Password = $scope.user.password;
                changePasswordService.editUser(data, function () {
                    $cookieStore.remove("Authorization");
                    $cookieStore.remove("LoginUserId");
                    $location.path("/login");
                });
            } else {
                $scope.message = "Old password is wrong";
                $scope.isStatus = true;
            }
        });
    }
}]);