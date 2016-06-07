appModule.controller('registerController', ['$scope', 'registerService', function ($scope, registerService) {

    $scope.user = {};

    $scope.registerUser = function () {
        registerService.createUser($scope.user).then(function (response) {

        });
    };
    
}]);


