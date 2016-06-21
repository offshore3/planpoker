﻿appModule.controller('changePasswordController', ['$scope','$location', '$cookieStore', 'changePasswordService', 'loginService', function ($scope, $location, $cookieStore, changePasswordService, loginService) {

    $scope.user = {};

    $scope.changePassword = function () {
        loginService.getUser(function (data) {
            if (data != null && data.Password === $scope.user.oldpassword) {
                data.Password = $scope.user.password;
                data.ComfirmPassword = $scope.user.oldpassword;
                changePasswordService.editUser(data, function () {
                    $("#changePasswordModal").modal("hide");
                });
            } else {
                $scope.message = "Old password is wrong";
                $scope.isStatus = true;
            }
        });
    }
}]);