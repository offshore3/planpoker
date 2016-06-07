﻿appModule.controller('registerController', ['$scope', 'registerService', function ($scope, registerService) {

    $scope.user = {};

    $scope.registerUser = function () {
        registerService.checkEmailExist($scope.user.email).then(function (response) {
            if (response.data) {
                $scope.isExist = response.data;
            }
            else
            {
                registerService.createUser($scope.user).then(function (response) {

                });
            }
        });        
    };
    
}]);


