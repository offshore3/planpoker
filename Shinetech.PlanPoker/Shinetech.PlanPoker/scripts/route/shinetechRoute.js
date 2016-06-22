appModule.config(function ($routeProvider) {
    $routeProvider

        .when("/login", {
            templateUrl: "template/login.html",
            controller: "loginController"
        })

        .when("/register", {
            templateUrl: "template/register.html",
            controller: "registerController"
        })

        .when("/retrievepassword", {
            templateUrl: "template/RetrievePassword.html",
            controller:"retrievePasswordController"
        })

        .when("/resetpassword", {
            templateUrl: "template/ResetPassword.html",
            controller:"resetPasswordController"
        })

        .when("/dashboard", {
            templateUrl: "template/dashboard.html",
            controller:"dashboardController"
        })

        .when("/monitor/:projectCode/", {
            templateUrl: "template/monitor.html",
            controller: "monitorController"
        })

        .otherwise("/login");
});