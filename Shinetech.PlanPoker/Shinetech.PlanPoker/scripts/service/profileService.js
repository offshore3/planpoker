appModule.service("profileService", ['httpProxy', '$http', function (httpProxy, $http) {
    
    this.editUser = function (user, successCallback, errorCallback) {
        httpProxy.put("api/user", user
       ).then(function (data) {
           successCallback(data);
       }, function (error) {
           errorCallback(error);
       });
    }

    this.uploadFileToUrl = function (file) {
        var fd = new FormData();
        fd.append('file', file);
        httpProxy.post("api/uploadImage", fd).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });

    }

}]);

