appModule.controller('retrievePasswordController', ['$scope', '$location', 'retrievePasswordService', 'registerService', 'emailtemplate', 'mailtemplatecontent', function ($scope, $location, retrievePasswordService, registerService, emailtemplate, mailtemplatecontent) {
    $scope.emailtemplate = emailtemplate;
    $scope.mailtemplatecontent = mailtemplatecontent;

    $scope.isExistEmail = true;

    $scope.retrievePassword = function () {
        $scope.emailtemplate.absUrl = $location.absUrl().replace('retrievepassword', 'resetpassword');

        registerService.checkEmailExist($scope.emailtemplate.emailto).then(function (response) {
            if (response.data) {
                retrievePasswordService.sendEmail($scope.emailtemplate, $scope.mailtemplatecontent).then(function (response) {
                    $scope.isExistEmail = true;
                    $scope.isSendEmail = true;
                }, function () {
                    $scope.isExistEmail = true;
                    $scope.isSendEmail = false;
                });
            } else {
                $scope.isExistEmail = false;
            }
        });

    }

}]);


