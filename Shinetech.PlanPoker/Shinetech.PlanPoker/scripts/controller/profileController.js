appModule.controller('profileController', ['$scope', 'profileService', function ($scope, profileService) {
    $scope.user = {};
    $scope.user.image = "/images/user.png";

}]);




            

            //profileService.getUser().then(function (response) {
            //    $scope.user = response.data;
            //    if (response.data.image != null || response.data.Image !== '') {
            //        $scope.user.image = response.data.Image;
            //    }
            //    else {
            //        $scope.user.image = "/images/user.png";
            //    }
            //});

            //$scope.updateUser = function () {
            //    loginService.login($scope.user.username, $scope.user.oldpassword).then(function (response) {
            //        console.log(response.data.Status);
            //        if (response.data.Status) {
            //            profileService.editUser($scope.user).then(function () {
            //                $scope.message = "update sucess";
            //                $scope.isStatus = true;
            //            });
            //        } else {
            //            $scope.message = "old password is wrong";
            //            $scope.isStatus = false;
            //        }
            //    });