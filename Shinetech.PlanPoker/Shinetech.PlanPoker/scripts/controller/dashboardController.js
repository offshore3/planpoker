appModule.controller('dashboardController', ['$rootScope', '$scope', '$cookieStore', 'dashboardService', 'projectService', function ($rootScope, $scope, $cookieStore, dashboardService, projectService) {
    
    $scope.customerIdSubscribed;

    $scope.selectedPoker = {
        text: "√",
        data: ""
    };
    //hub.server.join("123");
    $scope.pokers = {
        poker: [
            { data: 1, myStyle: { "left": "20%" } },
            { data: 2, myStyle: { "left": "23%" } },
            { data: 3, myStyle: { "left": "26%" } },
            { data: 5, myStyle: { "left": "29%" } },
            { data: 8, myStyle: { "left": "32%" } },
            { data: 13, myStyle: { "left": "35%" } },
            { data: 20, myStyle: { "left": "38%" } },
            { data: 40, myStyle: { "left": "41%" } },
            { data: 100, myStyle: { "left": "44%" } },
            { data: 's', myStyle: { "left": "47%" } },
            { data: 'm', myStyle: { "left": "50%" } },
            { data: 'l', myStyle: { "left": "53%" } },
            { data: 'xs', myStyle: { "left": "56%" } },
            { data: 'coffee', myStyle: { "left": "59%" } },
            { data: 'yes', myStyle: { "left": "62%" } },
            { data: 'no', myStyle: { "left": "65%" } }
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

        if ($scope.seletedProjectId == undefined) return;
        console.log(poker);
        console.log($scope.seletedProjectId);
        $scope.isFloat = poker.data;

        var command = {
            ProjectId: $scope.seletedProjectId,
            UserId: $cookieStore.get("LoginUserId"),
            SelectedPoker: poker.data
        };

        dashboardService.selectCard(command, function (data) {
            console.log(data);

        }, function () {

        });
    };

    $scope.changeProject = function () {
        if ($scope.seletedProjectId == undefined) return;

        dashboardService.getEstimateUsers($scope.seletedProjectId, function (data) {
            $rootScope.estimates = data.EstimateViewModel;
            $rootScope.isShowResult = data.IsShow;
            if ($scope.customerIdSubscribed &&
                    $scope.customerIdSubscribed.length > 0 &&
                    $scope.customerIdSubscribed !== $scope.seletedProjectId) {
                // unsubscribe to stope to get notifications for old customer
                hub.server.unsubscribe($scope.customerIdSubscribed);
            }
            // subscribe to start to get notifications for new customer
            //$.connection.hub.start().done(function () {
                hub.server.subscribe($scope.seletedProjectId);
                $scope.customerIdSubscribed = $scope.seletedProjectId;
            //});
        }, function () {
            $rootScope.estimates = [];
        });        
    };

    $scope.showEstimate = function () {
        dashboardService.showEstimate($scope.seletedProjectId, function () {
            $scope.isShowResult = true;
        }, function () { });
    }

    // signalr client functions
    
    
}]);


