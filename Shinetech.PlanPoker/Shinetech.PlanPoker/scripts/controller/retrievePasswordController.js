appModule.controller('retrievePasswordController', ['$scope','$location', 'retrievePasswordService', 'registerService', 'emailtemplate', 'mailtemplatecontent', function ($scope,$location, retrievePasswordService, registerService, emailtemplate, mailtemplatecontent) {
    $scope.emailtemplate = emailtemplate;
    $scope.mailtemplatecontent = mailtemplatecontent;

    $scope.isExistEmail = true;

    $scope.retrievePassword = function () {
        $scope.emailtemplate.absUrl = $location.absUrl().replace('retrievepassword', 'resetpassword');
        registerService.checkEmailExist($scope.emailtemplate.emailto).then(function (response) {
            console.log(response.data);
            if (response.data)
            {
                retrievePasswordService.sendEmail($scope.emailtemplate, $scope.mailtemplatecontent).then(function (response) {
                    console.log(response.data);
                }, function () {

                });
            } else
            {
                $scope.isExistEmail = false;
            }
        });
        
    }
    
}]);


