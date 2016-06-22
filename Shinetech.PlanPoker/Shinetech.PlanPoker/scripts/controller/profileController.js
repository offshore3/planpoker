appModule.controller('profileController', ['$scope', 'profileService', 'loginService', 'fileServices', function ($scope, profileService, loginService, fileServices) {
    $scope.user = {};
    $scope.isSaveProfileBusy = false;

    $scope.getUserInfo = function () {
        loginService.getUser(function (data) {
            $scope.user = data;
            if ($scope.user.ImagePath != null) {
                $scope.user.ImagePath = webAPI + $scope.user.ImagePath;
            }
        }, function () {
        });
    }

    $scope.uploadUserPicture = function () {
        $('#imageUpload').click();
    };

    $scope.saveUserInfo = function () {
        $scope.isSaveProfileBusy = true;
        if (userImagePath) {
            $scope.uploadPicture($scope.updateUser);
            $scope.isSaveProfileBusy = false;
        } else {
            $scope.updateUser();
            $scope.isSaveProfileBusy = false;
        }
    };

    $scope.updateUser = function () {
        if ($scope.user.ImagePath != null) {
            $scope.user.ImagePath = $scope.user.ImagePath.substring($scope.user.ImagePath.lastIndexOf("Image"));
        }
        profileService.editUser($scope.user, function (data) {
            if ($scope.user.ImagePath != null) {
                $scope.user.ImagePath = webAPI + $scope.user.ImagePath;
            }
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