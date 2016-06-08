appModule.service("registerService", ['$q', '$http', function ($q, $http) { 

    this.checkEmailExist = function (email) {
        return $http({
            method: "Get",
            url: webAPI + "api/checkemail?email=" + email
        }).then(function (response) {
            return $q.when(response);
        });
    };

    this.createUser = function (user) {
        return $http({
            method: "POST",
            url: webAPI + "api/user",
            data: user
        }).then(function (response) {
            return $q.when(response);
        });
    };

}])