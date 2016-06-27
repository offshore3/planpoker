appModule.controller('dashboardController', ['$rootScope', '$scope', '$cookieStore', 'dashboardService', 'projectService', function ($rootScope, $scope, $cookieStore, dashboardService, projectService) {

    $scope.customerIdSubscribed;
    $rootScope.isShowResult = false;
    $scope.webAPI = webAPI;
    $scope.selectedPoker = {
        text: "√",
        data: ""
    };
    
    $scope.pokers = {
        poker: [
            { data: 1, myStyle: { "left": "17.5%" }, pokerclass: "card-poker-1" },
            { data: 2, myStyle: { "left": "21.5%" }, pokerclass: "card-poker-2" },
            { data: 3, myStyle: { "left": "25.5%" }, pokerclass: "card-poker-3" },
            { data: 5, myStyle: { "left": "29.5%" }, pokerclass: "card-poker-5" },
            { data: 8, myStyle: { "left": "33.5%" }, pokerclass: "card-poker-8" },
            { data: 13, myStyle: { "left": "37.5%" }, pokerclass: "card-poker-13" },
            { data: 20, myStyle: { "left": "41.5%" }, pokerclass: "card-poker-20" },
            { data: 40, myStyle: { "left": "45.5%" }, pokerclass: "card-poker-40" },
            { data: 100, myStyle: { "left": "49.5%" }, pokerclass: "card-poker-100" },
            { data: 's', myStyle: { "left": "53.5%" }, pokerclass: "card-poker-s" },
            { data: 'm', myStyle: { "left": "57.5%" }, pokerclass: "card-poker-m" },
            { data: 'l', myStyle: { "left": "61.5%" }, pokerclass: "card-poker-l" },
            { data: 'xs', myStyle: { "left": "65.5%" }, pokerclass: "card-poker-xs" },
            { data: 'rest', myStyle: { "left": "69.5%" }, pokerclass: "card-poker-rest" },
            { data: 'yes', myStyle: { "left": "73.5%" }, pokerclass: "card-poker-yes" },
            { data: 'no', myStyle: { "left": "77.5%" }, pokerclass: "card-poker-no" }
        ]
    };

    $scope.loadProjects = function () {
        var command = {
            queryText: '',
            pageNumber: 1,
            pageCount: 100
        };

        $scope.projectCode = getQueryVariable('code');

        if ($scope.projectCode) {
            dashboardService.decryptProjectCode($scope.projectCode, function (data) {
                $scope.projectId = data;
                $scope.isFloat = "";
                $scope.seletedProjectId = data;
                $scope.changeProject();
            });
        }

        projectService.queryProjects(command, function (data) {
            $scope.projects = data.ProjectViewModels;
        }, function () {

        });
    };

    $scope.$on('dashboardReloadProject', function () {
        $scope.loadProjects();
    });

    $scope.cardSelect = function (poker) {

        if ($scope.seletedProjectId == undefined || $scope.seletedProjectId == null || $scope.seletedProjectId == "" || $rootScope.isShowResult) return;
        $scope.isFloat = poker.data;

        var command = {
            ProjectId: $scope.seletedProjectId,
            UserId: $cookieStore.get("LoginUserId"),
            SelectedPoker: poker.data
        };

        dashboardService.selectCard(command, function (data) {

        }, function () {

        });
    };

    $scope.changeProject = function () {
        $rootScope.isShowResult = false;
        $rootScope.averagePoint = "";
        $scope.isFloat = "";
        if ($scope.seletedProjectId == undefined || $scope.seletedProjectId == null || $scope.seletedProjectId === "") {
            $rootScope.estimates = [];
            return;
        }

        $scope.href = "#/monitor/" + $scope.seletedProjectId + "-" + $cookieStore.get('LoginUserId');

        dashboardService.getEstimateUsers($scope.seletedProjectId, function (data) {
            if ($scope.customerIdSubscribed &&
                    $scope.customerIdSubscribed.length > 0 &&
                    $scope.customerIdSubscribed !== $scope.seletedProjectId) {
                hub.server.unsubscribe($scope.customerIdSubscribed);
            }
            hub.server.subscribe($scope.seletedProjectId);
            $scope.customerIdSubscribed = $scope.seletedProjectId;
            if (data == null) {
                $rootScope.estimates = [];
                return;
            }

            $rootScope.estimates = data.EstimateViewModel;
            $rootScope.isShowResult = data.IsShow;
            $rootScope.averagePoint = data.AveragePoint;
        }, function () {
            $rootScope.estimates = [];
        });

    };
    $scope.isShowEstimateBusy = false;
    $scope.showEstimate = function () {
        $scope.isShowEstimateBusy = true;
        dashboardService.showEstimate($scope.seletedProjectId, function () {
            $scope.isShowEstimateBusy = false;
        }, function() {
            $scope.isShowEstimateBusy = false;
        });
    }

    $scope.isClearEstimateBusy = false;
    $scope.clearEstimate = function () {
        $scope.isClearEstimateBusy = true;
        $scope.isFloat = "";
        dashboardService.removeEstimate($scope.seletedProjectId, function () {
            $scope.isClearEstimateBusy = false;
        }, function () {
            $scope.isClearEstimateBusy = false;
        });
    }

}]);


