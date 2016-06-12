appModule.controller("shinetechController", ['$scope', '$location', '$cookieStore', function ($scope, $location, $cookieStore) {
    $scope.removeCookie = function () {
        $cookieStore.remove("Authorization");
        $cookieStore.remove("LoginUserId");
        $location.path("/login");
    }
}])