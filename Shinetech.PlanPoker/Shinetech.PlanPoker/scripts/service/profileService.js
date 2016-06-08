appModule.service("profileService", ['httpProxy', function (httpProxy) {

    this.getUser = function (successCallback, errorCallback) {
        httpProxy.get("api/user").then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }

    this.editUser = function (user, successCallback, errorCallback) {
        httpProxy({
            method: "Put",
            url: "api/user",
            data: user
        }).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }
}]);