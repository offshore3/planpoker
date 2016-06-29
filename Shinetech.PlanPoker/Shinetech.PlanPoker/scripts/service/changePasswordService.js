appModule.service("changePasswordService", ['$http', 'httpProxy', 'authorizationService', function ($http, httpProxy, authorizationService) {

    this.editUser = function (user, successCallback, errorCallback) {
        httpProxy.put("api/changepassword", user
       ).then(function (token) {
           authorizationService.setAuthorization(token);
           successCallback(token);
       }, function (error) {
           errorCallback(error);
       });


    }
}]);

