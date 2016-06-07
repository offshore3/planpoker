appModule.service("loginService", ['$http', function ($http) {
    
    this.login = function (email,password,successCallback, errorCallback) {
        $http.get(webAPI + "api/login?email=" + email + "&password=" + password).then(function (token) {
            if (token.data.length > 0) {
                $http.defaults.headers.common['Authorization'] = token.data;
            }
            successCallback();
        }, function (error) {
            errorCallback(error);
        });
    };

    this.getAllUser = function (successCallback, errorCallback) {
        $http.get(webAPI + "api/get-all").then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }
}])