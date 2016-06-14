appModule.controller('retrievePasswordController', ['$scope', 'retrievePasswordService','registerService','emailtemplate', function ($scope, retrievePasswordService,registerService, emailtemplate) {


    $scope.emailtemplate = emailtemplate;
    console.log($scope.emailtemplate);
    //console.log(emailtemplate);
    $scope.sendEmail = function () {
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
        //registerService.checkEmailExist($scope.emailtemplate.emailto).then(function (response) {
        //    if (response.data) {
        //        $scope.isExistEmail = response.data;
        //    } else {
        //        resetPasswordService.
        //    }
        //});
        
    }
    
}]);


