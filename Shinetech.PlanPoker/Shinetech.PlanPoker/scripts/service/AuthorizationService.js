appModule.service("authorizationService", ['$http', function ($http) {
    this.setAuthorization= function(token) {
        var resultArray = token.split(',,');
        $.cookie('Authorization', resultArray[0], { expires: 7 });
        $.cookie('LoginUserId', resultArray[1], { expires: 7 });
        $http.defaults.headers.common['Authorization'] = resultArray[0];
        $http.defaults.headers.common['LoginUserId'] = resultArray[1];
    }
}]);