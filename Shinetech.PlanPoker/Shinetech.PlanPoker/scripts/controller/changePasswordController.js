appModule.controller('changePasswordController', ['$scope','$location', 'changePasswordService', 'loginService', function ($scope, $location, changePasswordService, loginService) {

    $scope.user = {};
    $scope.isChangePasswordBusy = false;
    $scope.changePassword = function () {
        $scope.isChangePasswordBusy = true;
        loginService.getUser(function (data) {
            if (data != null && data.Password === $scope.user.oldpassword) {
                data.Password = $scope.user.password;
                changePasswordService.editUser(data, function () {
                    $("#changePasswordModal").modal("hide");
                    $scope.isChangePasswordBusy = false;
                });
            } else {
                $scope.message = "Old password is wrong";
                $scope.isStatus = true;
                $scope.isChangePasswordBusy = false;
            }
        });
    }
}]);