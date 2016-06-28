appModule.controller("shinetechController", ['$scope', '$location', '$cookieStore', '$rootScope', function ($scope, $location, $cookieStore, $rootScope) {
    $scope.removeCookie = function () {
        $rootScope.estimates = [];
        $cookieStore.remove("Authorization");
        $cookieStore.remove("LoginUserId");
        LinkedInSignOut();
        $location.path("/login");
    }
}])