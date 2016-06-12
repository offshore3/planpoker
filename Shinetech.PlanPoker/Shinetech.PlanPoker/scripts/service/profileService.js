appModule.service("profileService", ['httpProxy', '$http', function (httpProxy, $http) {

    this.getUser = function (successCallback, errorCallback) {
        httpProxy.get("api/user").then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }

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
        httpProxy.post("api/uploadImg", fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function () {
            
        },function(){
            
        });

        //$http.post(webAPI + "api/uploadImg", fd, {
        //    transformRequest: angular.identity,
        //    headers: { 'Content-Type': undefined }
        //})
        //.success(function () {

        //})
        //.error(function () {

        //});
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

