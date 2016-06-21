appModule.service("resetPasswordService", ['$http', function ($http) { 
    this.getUserByEmail = function (email, successCallback, errorCallback) {
        $http.get(webAPI +"api/getuserbyemail?email=" + email
       ).then(function (data) {
           successCallback(data.data);
       }, function (error) {
           errorCallback(error);
       });
    }

    this.resetUserPassword = function (user, successCallback, errorCallback) {
        $http.put(webAPI + "api/resetpassowrd", user
       ).then(function () {
           successCallback();
       }, function (error) {
           errorCallback(error);
       });
    }
    

}])