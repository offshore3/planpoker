appModule.controller('registerController', ['$scope', '$location', 'registerService', function ($scope, $location, registerService) {

    $scope.user = {};
    $scope.inviteProjectId = getQueryVariable('code');

    $scope.registerUser = function () {

        registerService.createUser($scope.user).then(function () {
            if ($scope.inviteProjectId != null || $scope.inviteProjectId != undefined) {
                var command = {
                    EndCodeProjectId: $scope.inviteProjectId,
                    Email: $scope.user.email
                };
                registerService.inviteUser(command).then(function () {

                });
            }
            $location.path('/login');
        });

        //registerService.checkEmailExist($scope.user.email).then(function (response) {
        //    if (response.data) {
        //        $scope.isExist = response.data;
        //    }
        //    else
        //    {
                
        //    }
        //});        
    };
    
}]);


