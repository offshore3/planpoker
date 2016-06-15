appModule.controller('retrievePasswordController', ['$scope', 'retrievePasswordService', 'registerService', 'emailtemplate', 'mailtemplatecontent', function ($scope, retrievePasswordService, registerService, emailtemplate, mailtemplatecontent) {
    $scope.emailtemplate = emailtemplate;
    $scope.mailtemplatecontent = mailtemplatecontent;
    $scope.isExistEmail = true;

    $scope.retrievePassword = function () {

        
        //registerService.checkEmailExist($scope.user.email).then(function (response) {
        //    if (response.data) {
        //        $scope.isExist = response.data;
        //    }
        //    else {
        //        registerService.createUser($scope.user).then(function (response) {
        //            $location.path('/login');
        //        });
        //    }
        //});
        console.log($scope.emailtemplate.emailto);
        registerService.checkEmailExist($scope.emailtemplate.emailto).then(function (response) {
            console.log(response.data);
            if (response.data)
            {
                retrievePasswordService.sendEmail($scope.emailtemplate, $scope.mailtemplatecontent).then(function () {

                }, function () {

                });
            } else
            {
                $scope.isExistEmail = false;
            }
        });
        
    }
    
}]);


