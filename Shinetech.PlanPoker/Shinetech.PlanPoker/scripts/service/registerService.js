appModule.service("registerService", ['$q', '$http', function ($q, $http) {  

    this.createUser = function (user) {
        console.log(user);
        return $http({
            method: "POST",
            url: webAPI + "/api/user",
            data: user
        }).then(function (response) {
            return $q.when(response);
        });
    };

}])