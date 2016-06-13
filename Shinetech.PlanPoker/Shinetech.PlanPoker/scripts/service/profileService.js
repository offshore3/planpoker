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
        console.log(fd);
        httpProxy.post("api/uploadImage", fd).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });

        //, {
        //    transformRequest: angular.identity,
        //    headers: { 'Content-Type': 'application/json' }
        //}

    }

    //this.uploadFileToUrl = function (file, uploadUrl) {
    //    var fd = new FormData();
    //    fd.append('file', file);
    //    console.log(uploadUrl);
    //    console.log(fd);
    //    $http.post(webAPI + "api/uploadImg", fd, {
    //        transformRequest: angular.identity,
    //        headers: { 'Content-Type': undefined }
    //    })
    //    .success(function () {

    //    })
    //    .error(function () {

    //    });
    //}

}]);

