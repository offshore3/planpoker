appModule.controller('profileController', ['$scope', 'profileService', function ($scope, profileService) {
    $scope.user = {};

    profileService.getUser(function (data) {
        console.log(data.data);
        $scope.user = data.data;
    }, function () {

    });

    $scope.updateUser = function () {
        profileService.editUser($scope.user, function (data) {
            $scope.message = "update sucess";
            $scope.isStatus = true;
        }, function () {
            $scope.message = "update failed";
            $scope.isStatus = false;
        });
    }

}]);