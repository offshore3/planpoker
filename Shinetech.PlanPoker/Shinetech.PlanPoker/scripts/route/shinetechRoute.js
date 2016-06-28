appModule.config(function ($routeProvider) {
    $routeProvider

        .when("/login", {
            templateUrl: "template/login.html"
        })

        .when("/register", {
            templateUrl: "template/register.html"
        })

        .when("/retrievepassword", {
            templateUrl: "template/RetrievePassword.html"
        })

        .when("/resetpassword", {
            templateUrl: "template/ResetPassword.html"
        })

        .when("/dashboard", {
            templateUrl: "template/dashboard.html"
        })

        .when("/monitor/:projectCode/", {
            templateUrl: "template/monitor.html"
        })

        .otherwise("/login");
});