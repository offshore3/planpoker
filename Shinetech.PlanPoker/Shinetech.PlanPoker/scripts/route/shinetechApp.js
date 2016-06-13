var webAPI = "http://localhost:25455/";
var Token = "Token";
var appModule = angular.module("shinetech-app", ["ngRoute", "ngCookies"]);

angular.module("shinetech-app").run(["$rootScope", "$location", "$routeParams", function ($rootScope, $location, $routeParams) {
    $rootScope.$on("$routeChangeSuccess", routeChangeSuccess);
    function routeChangeSuccess() {
        $rootScope.isSettingsShow = true;
        if ($location.path() === "/login" || $location.path() === "/register" || $location.path() === "/resetpassword") {
            $rootScope.isSettingsShow = false;
        }
    }

}]);
