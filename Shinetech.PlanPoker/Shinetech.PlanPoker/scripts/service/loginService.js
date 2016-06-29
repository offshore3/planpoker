appModule.service("loginService", ['$http', 'authorizationService', 'httpProxy', function ($http, authorizationService, httpProxy) {
    
    this.login = function (email,password,successCallback, errorCallback) {
        $http.get(webAPI + "api/login?email=" + email + "&password=" + password).then(function (token) {
            if (token.data.length > 0) {
                authorizationService.setAuthorization(token.data);
                successCallback();
            } else {
                errorCallback();
            }
        });
    };

    this.getUser = function (successCallback, errorCallback) {
        httpProxy.get("api/user").then(function (data) {
            successCallback(data);
        }, function () {

        });
    }

}])