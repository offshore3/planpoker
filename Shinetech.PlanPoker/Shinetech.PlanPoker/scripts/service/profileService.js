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

    this.uploadFileToUrl = function (file, uploadUrl) {
        var fd = new FormData();
        fd.append('file', file);
        console.log(uploadUrl);
        console.log(fd);
        $http.post(webAPI+"api/Upload/ImgUpload", fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })
        .success(function () {

        })
        .error(function () {

        });
    }
}]);

