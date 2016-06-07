appModule.controller('resetPasswordController', ['$scope', 'resetPasswordService', function ($scope, resetPasswordService) {

    $scope.user = {
        isError: false,
        message: ''
    };
    
}]);


