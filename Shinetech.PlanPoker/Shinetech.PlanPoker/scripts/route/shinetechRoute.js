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

        .when("/dashboard", {
            templateUrl: "template/dashboard.html",
            controller:"dashboardController"
        })

        //.when("/profile", {
        //    templateUrl: "template/profile.html",
        //    controller:"profileController"
        //})

        //.when("/changePassword", {
        //    templateUrl: "template/ChangePassword.html",
        //    controller:"changePasswordController"
        //})

        //.when("/projects", {
        //    templateUrl: "template/project.html",
        //    controller:"projectController"
        //})

        .otherwise("/login");
});