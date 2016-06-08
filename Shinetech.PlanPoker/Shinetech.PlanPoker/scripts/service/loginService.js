appModule.service("loginService", ['$http', function ($http) {
    
    this.login = function (email,password,successCallback, errorCallback) {
        $http.get(webAPI + "api/login?email=" + email + "&password=" + password).then(function (token) {
            if (token.data.length > 0) {
                var resultArray = token.data.split('&');
                $http.defaults.headers.common['Authorization'] = resultArray[0];
                $http.defaults.headers.common['LoginUserId'] = resultArray[1];
            }
            successCallback();
        }, function (error) {
            errorCallback(error);
        });
    };

    this.testAuthorize = function (successCallback, errorCallback) {
        $http.get(webAPI + "api/test-authorize").then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }
    this.testGetUser = function (successCallback, errorCallback) {
        $http.get(webAPI + "api/user").then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }

    this.testUpdateUser = function (command, successCallback, errorCallback) {
        $http({
            method: "Put",
            url: webAPI + "api/user",
            data: command
        }).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }
    
}])