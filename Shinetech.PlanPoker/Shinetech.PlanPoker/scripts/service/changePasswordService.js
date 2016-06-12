appModule.service("changePasswordService", ['httpProxy', function (httpProxy) {

    this.editUser = function (user, successCallback, errorCallback) {
        httpProxy.put("api/changepassword", user
       ).then(function (data) {
           successCallback(data);
       }, function (error) {
           errorCallback(error);
       });


    }
}]);

