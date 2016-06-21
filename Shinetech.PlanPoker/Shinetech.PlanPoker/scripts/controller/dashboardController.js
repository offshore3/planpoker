appModule.controller('dashboardController', ['$rootScope', '$scope', '$cookieStore', 'dashboardService', 'projectService', function ($rootScope, $scope, $cookieStore, dashboardService, projectService) {

    $scope.customerIdSubscribed;

    $scope.selectedPoker = {
        text: "√",
        data: ""
    };
    
    $scope.pokers = {
        poker: [
            { data: 1, myStyle: { "left": "16%" } },
            { data: 2, myStyle: { "left": "20%" } },
            { data: 3, myStyle: { "left": "24%" } },
            { data: 5, myStyle: { "left": "28%" } },
            { data: 8, myStyle: { "left": "32%" } },
            { data: 13, myStyle: { "left": "36%" } },
            { data: 20, myStyle: { "left": "40%" } },
            { data: 40, myStyle: { "left": "44%" } },
            { data: 100, myStyle: { "left": "48%" } },
            { data: 's', myStyle: { "left": "52%" } },
            { data: 'm', myStyle: { "left": "56%" } },
            { data: 'l', myStyle: { "left": "60%" } },
            { data: 'xs', myStyle: { "left": "64%" } },
            { data: 'coffee', myStyle: { "left": "68%" } },
            { data: 'yes', myStyle: { "left": "72%" } },
            { data: 'no', myStyle: { "left": "76%" } }
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
            });
        }

        projectService.queryProjects(command, function (data) {
            $scope.projects = data.ProjectViewModels;
        }, function () {

        });
    };

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
        if ($scope.seletedProjectId == undefined || $scope.seletedProjectId == null || $scope.seletedProjectId == "") {
            $rootScope.estimates = [];
            return;
        }
        dashboardService.encryptProjectCode($scope.seletedProjectId, function (data) {
            $scope.href = "#/monitor?code=" + data;
        }, function () {
        });

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

    $scope.showEstimate = function () {
        dashboardService.showEstimate($scope.seletedProjectId, function (data) {

        }, function () { });
    }

    $scope.clearEstimate = function () {
        $scope.isFloat = "";
        dashboardService.removeEstimate($scope.seletedProjectId, function () {

        }, function () {

        });
    }

}]);


