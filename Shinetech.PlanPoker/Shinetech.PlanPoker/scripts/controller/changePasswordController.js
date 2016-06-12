appModule.controller('changePasswordController', ['$scope', '$cookieStore', 'changePasswordService', 'loginService', function ($scope, $cookieStore, changePasswordService, loginService) {

    $scope.user = {};

    $scope.changePassword = function () {
        loginService.getUser(function (data) {
            if (data != null || data != undefined)
            {
                loginService.login(data.Email, $scope.user.oldpassword, function () {
                    data.Password = $scope.user.password;
                    changePasswordService.editUser(data, function () {
                        $scope.message = "update sucess";
                        $scope.isStatus = true;                    
                    });

                }, function () {
                    $scope.message = "old password is wrong";
                    $scope.isStatus = true;
                });
            }
        });
    }
}]);