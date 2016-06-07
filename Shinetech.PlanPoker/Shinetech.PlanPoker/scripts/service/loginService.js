appModule.service("loginService", ['$http', function ($http) {

    this.login = function (command, successCallback, errorCallback) {
        $http.post(webApi + "api/login", command).then(function (token) {
            $http.defaults.headers.common['Authorization'] = token.data.substring(1, token.data.length - 1);
            successCallback();
        }, function (error) {
            errorCallback(error);
        });
    };

    this.getAllUser = function (successCallback, errorCallback) {
        $http.get(webApi + "api/get-all").then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }
}])