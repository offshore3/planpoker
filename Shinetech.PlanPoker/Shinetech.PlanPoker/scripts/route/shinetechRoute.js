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

        .when("/resetpassword", {
            templateUrl: "template/ResetPassword.html",
            controller:"resetPasswordController"
        })

        .otherwise("/login");
});