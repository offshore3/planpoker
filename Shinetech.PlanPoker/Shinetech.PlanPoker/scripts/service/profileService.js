appModule.service("profileService", ['$http', function ($http) {

    this.getUser = function (successCallback, errorCallback) {
        $http.get(webAPI + "api/user").then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }

    this.editUser = function (user, successCallback, errorCallback) {
        $http({
            method: "Put",
            url: webAPI + "api/user",
            data: user
        }).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }
}]);