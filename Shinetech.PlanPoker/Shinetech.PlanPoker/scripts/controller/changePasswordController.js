appModule.controller('changePasswordController', ['$scope','$location', '$cookieStore', 'changePasswordService', 'loginService', function ($scope, $location, $cookieStore, changePasswordService, loginService) {

    $scope.user = {};

    $scope.changePassword = function () {
        loginService.getUser(function (data) {
            if (data != null || data != undefined)
            {
                loginService.login(data.Email, $scope.user.oldpassword, function () {
                    data.Password = $scope.user.password;
                    changePasswordService.editUser(data, function () {
                        $cookieStore.remove("Authorization");
                        $cookieStore.remove("LoginUserId");
                        $location.path("/login");
                    });

                }, function () {
                    $scope.message = "Old password is wrong";
                    $scope.isStatus = true;
                });
            }
        });
    }
}]);