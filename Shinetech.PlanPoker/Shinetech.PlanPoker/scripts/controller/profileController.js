appModule.controller('profileController', ['$scope', 'profileService', 'loginService', 'fileServices', function ($scope, profileService, loginService, fileServices) {
    $scope.user = {};

    $scope.getUserInfo = function () {
        loginService.getUser(function (data) {
            $scope.user = data;
        }, function () {
        });
    }

    $scope.uploadUserPicture = function () {
        $scope.user.ImagePath = $scope.imagepPath;
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
            $scope.message = "update sucess";
            $scope.isStatus = true;
        }, function () {
            $scope.message = "update fail";
            $scope.isStatus = false;
        });
    }

    $scope.uploadFile = function () {
        var file = $scope.myFile;
        console.log(file);
        profileService.uploadFileToUrl(file);
    };

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