﻿appModule.controller('loginController', ['$scope','$location','loginService', function ($scope,$location, loginService) {

    $scope.user = {};
    $scope.message = "";

    $scope.login = function () {
        console.log($scope.user);
        loginService.login($scope.user.email, $scope.user.password, function () {
            $location.path("/profile");
        }, function() {
            
        });
    };

    $scope.testAuthorize = function () {
        loginService.testAuthorize(function (data) {
            console.log(data.data);
        }, function () {

        });
    }

    $scope.testGetUser = function () {
        loginService.testGetUser(function (data) {
            console.log(data.data);
        }, function () {

        });
    }

    $scope.testUpdateUser = function () {
        var command = {
            Id: 1,
            Name: "Joy101",
            ImagePath:"test"
        }
        loginService.testUpdateUser(command,function (data) {
            console.log(data.data);
        }, function () {

        });
    }
    

}]);


