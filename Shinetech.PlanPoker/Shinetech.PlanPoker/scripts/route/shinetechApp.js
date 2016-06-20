var webAPI = "http://localhost:25455/",
    Token = "Token",
    userImagePath = "",
    appModule = angular.module("shinetech-app", ["ngRoute", "ngCookies"]),
    hub = $.connection.ShinetechPlanPokerHub;

angular.module("shinetech-app").run([
    "$rootScope", "$location", "$routeParams", "$cookieStore", function($rootScope, $location, $routeParams, $cookieStore) {
        $rootScope.$on("$routeChangeSuccess", routeChangeSuccess);
        $rootScope.$on('$locationChangeStart', locationChangeStart);
        function routeChangeSuccess() {
            $rootScope.isSettingsShow = true;
            if ($location.path() === "/login" || $location.path() === "/register" || $location.path() === "/retrievepassword" || $location.path() === "/resetpassword"||$location.path()==="/monitor") {
                $rootScope.isSettingsShow = false;
            } 
        }

        function locationChangeStart() {
            if ($location.path() === "/dashboard") {
                if ($cookieStore.get("LoginUserId") === undefined || $cookieStore.get("LoginUserId") === null) {
                    console.log(11);
                    $rootScope.gotoLogin();
                }
            }
        }

        $rootScope.gotoLogin= function() {
            $location.path("login");
        }

        $rootScope.gotoRegister= function() {
            $location.path("register");
        }

        $rootScope.gotoRetrievePassword = function () {
            $location.path("retrievepassword");
        }

        $rootScope.estimates = [];


        $.connection.hub.url = webAPI+"signalr";
        $.connection.hub.start().done(function () {
            //console.log("connected");
            //$("div[data-autorefresh='true']").each(function (i, e) {
            //    var $this = $(this);
            //    var projectId = $this.attr('project-id');
            //    hub.server.join(projectId);
            //});

        })
        .fail(function () {

        });

        hub.client.refreshEstimateResult = function (message) {
            //logic
        };

        hub.client.addItem = function (item) {

            var existItem = E($rootScope.estimates).FirstOrDefault(undefined, function (t) {
                return t.UserId === item.UserId;
            });
            if (existItem) {
                existItem.SelectedPoker = item.SelectedPoker;
            } else {
                $rootScope.estimates.push(item);
            }
            $rootScope.$apply();
        };

        hub.client.showEstimateResult = function (data) {
            console.log(data);
            $rootScope.isShowResult = data.IsShow;
            $rootScope.averagePoint = data.AveragePoint;
            $rootScope.$apply();
        };
        
        hub.client.clearEstimate = function () {
            $rootScope.estimates = [];
            $rootScope.isShowResult = false;
            $rootScope.$apply();
        };
    }
]);