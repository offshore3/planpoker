appModule.controller('profileController', ['$scope', 'profileService', 'loginService', 'fileServices', function ($scope, profileService, loginService, fileServices) {
    $scope.user = {};

    $scope.getUserInfo = function () {
        loginService.getUser(function (data) {
            $scope.user = data;
        }, function () {
        });
    }

    $scope.uploadUserPicture = function () {
        $('#imageUpload').click();
    };

    $scope.saveUserInfo = function () {
        $scope.isBusy = true;
        if (userImagePath) {
            $scope.uploadPicture($scope.updateUser);
        } else {
            $scope.updateUser();
        }
    };

    $scope.updateUser = function () {
        profileService.editUser($scope.user, function (data) {
            $("#profileModal").modal("hide");
        }, function () {
            $scope.message = "update fail";
            $scope.isStatus = false;
        });
    }

    $scope.uploadPicture = function (successCallback) {
        $scope.userPicture = userImagePath;
        userImagePath = '';
        var url = webAPI + 'api/picture/upload';
        $scope.uploading = true;
        fileServices.uploadFileToUrl($scope.userPicture, url, function (progress) { }, function (picutreUrl) {
            $scope.user.ImagePath = picutreUrl;
            successCallback();
        }, function (error) {
        });
    };
}]);