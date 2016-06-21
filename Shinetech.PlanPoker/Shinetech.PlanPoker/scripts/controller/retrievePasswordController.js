appModule.controller('retrievePasswordController', ['$scope', '$location', 'retrievePasswordService', 'registerService', 'emailtemplate', 'mailtemplatecontent', function ($scope, $location, retrievePasswordService, registerService, emailtemplate, mailtemplatecontent) {
    $scope.emailtemplate = emailtemplate;
    $scope.mailtemplatecontent = mailtemplatecontent.retrievepassword;

    $scope.isExistEmail = true;

    $scope.retrievePassword = function () {
        $scope.emailtemplate.absUrl = $location.absUrl().replace('retrievepassword', 'resetpassword');
        $scope.emailtemplate.EmailCode = $scope.emailtemplate.emailto;
        registerService.checkEmailExist($scope.emailtemplate.emailto).then(function (response) {
            console.log(response.data);
            if (response.data) {
                retrievePasswordService.sendEmail($scope.emailtemplate, $scope.mailtemplatecontent).then(function (response) {
                    $scope.isExistEmail = true;
                    $scope.isSendEmail = true;
                }, function () {

                });
            } else {
                $scope.isExistEmail = false;
            }
        });

    }

}]);


