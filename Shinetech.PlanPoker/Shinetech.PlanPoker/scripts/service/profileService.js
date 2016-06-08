appModule.service("profileService", ['$http', '$q', function ($q,$http) {
    this.getUser = function (userId) {
        return $http({
            method: "Get",
            url: webAPI + "/api/user/" + userId
        }).then(function (response) {
            return $q.when(response);
        });
    }

    this.editUser = function (user) {
        return $http({
            method: "Put",
            url: webAPI + "/api/user",
            data: user
        }).then(function (response) {
            return $q.when(response);
        });
    }
}]);