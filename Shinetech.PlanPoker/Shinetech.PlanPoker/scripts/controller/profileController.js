appModule.controller('profileController', ['$scope', 'profileService', function ($scope, profileService) {
    $scope.user = {};

    profileService.getUser(function (data) {
        $scope.user = data;
    }, function () {

    });

    $scope.uploadUserPicture = function () {
        $scope.user.ImagePath = $scope.imagepPath;
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

}]);