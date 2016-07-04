appModule.controller("shinetechController", ['$scope', '$location', '$rootScope', function ($scope, $location, $rootScope) {
    $scope.removeCookie = function () {
        $rootScope.estimates = [];
        $.removeCookie('Authorization');
        $.removeCookie('LoginUserId');
        $location.path("/login");
    }
}])