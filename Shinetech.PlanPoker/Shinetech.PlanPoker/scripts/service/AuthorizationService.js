appModule.service("authorizationService", ['$http', '$cookieStore', function ($http, $cookieStore) {
    this.setAuthorization= function(token) {
        var resultArray = token.split(',,');
        $cookieStore.put('Authorization', resultArray[0]);
        $cookieStore.put('LoginUserId', resultArray[1]);
        $http.defaults.headers.common['Authorization'] = $cookieStore.get('Authorization');
        $http.defaults.headers.common['LoginUserId'] = $cookieStore.get('LoginUserId');
    }
}]);