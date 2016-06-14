appModule.service("changePasswordService", ['$http', 'httpProxy', '$cookieStore', function ($http, httpProxy, $cookieStore) {

    this.editUser = function (user, successCallback, errorCallback) {
        httpProxy.put("api/changepassword", user
       ).then(function (token) {
           var resultArray = token.split('&');
           $cookieStore.put('Authorization', resultArray[0]);
           $cookieStore.put('LoginUserId', resultArray[1]);
           $http.defaults.headers.common['Authorization'] = $cookieStore.get('Authorization');
           $http.defaults.headers.common['LoginUserId'] = $cookieStore.get('LoginUserId');
           successCallback(token);
       }, function (error) {
           errorCallback(error);
       });


    }
}]);

