var webAPI = "http://localhost:25455/";
var Token = "Token";
var userImagePath = "";
var appModule = angular.module("shinetech-app", ["ngRoute", "ngCookies"]);

angular.module("shinetech-app").run([
    "$rootScope", "$location", "$routeParams", "$cookieStore", function($rootScope, $location, $routeParams, $cookieStore) {
        $rootScope.$on("$routeChangeSuccess", routeChangeSuccess);
        $rootScope.$on('$locationChangeStart', locationChangeStart);
        function routeChangeSuccess() {
            $rootScope.isSettingsShow = true;
            if ($location.path() === "/login" || $location.path() === "/register" || $location.path() === "/retrievepassword" || $location.path() === "/resetpassword") {
                $rootScope.isSettingsShow = false;
            } 
        }

        function locationChangeStart() {
            if ($location.path() === "/dashboard") {
                if ($cookieStore.get("LoginUserId") === undefined || $cookieStore.get("LoginUserId") === null) {
                    console.log(11);
                    $location.path("/login");
                }
            }
        }
    }
]);