appModule.controller('unionLoginController', ['$scope', 'registerService', function ($scope, registerService) {

    $scope.isUpdateEmailBusy = false;

    $scope.updateUserEmail = function () {
        $scope.isUpdateEmailBusy = true;
        registerService.checkEmailExist($scope.user.Email).then(function (response) {
            if (response.data) {
                $scope.isExist = response.data;
                $scope.isUpdateEmailBusy = false;
            }
            else {
                registerService.updateUserEmail($scope.user).then(function () {
                    $("#unionLoginModal").modal("hide");
                });
            }
        });
    };
}]);