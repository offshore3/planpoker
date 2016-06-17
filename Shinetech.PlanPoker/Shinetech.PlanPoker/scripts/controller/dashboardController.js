appModule.controller('dashboardController', ['$scope', '$cookieStore', 'dashboardService', 'projectService', function ($scope,$cookieStore, dashboardService, projectService) {
     
    // create a proxy to signalr hub on web server
    $scope.estimates = [];
    $scope.customerIdSubscribed;

    $scope.selectedPoker = {
        text: "?",
        data: ""
    };

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
            pageCount: 12
        };

        $scope.projectCode = getQueryVariable('code');

        if ($scope.projectCode) {
            dashboardService.decryptProjectCode($scope.projectCode, function (data) {
                console.log(data);
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
        $scope.selectedPoker.data = poker.data;
        $scope.selectedPoker.text = "√";

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
            $scope.estimates = data;

            if ($scope.customerIdSubscribed &&
                    $scope.customerIdSubscribed.length > 0 &&
                    $scope.customerIdSubscribed !== $scope.seletedProjectId) {
                // unsubscribe to stope to get notifications for old customer
                hub.server.unsubscribe($scope.customerIdSubscribed);
            }
            // subscribe to start to get notifications for new customer
            $.connection.hub.start().done(function () {
                hub.server.subscribe($scope.seletedProjectId);
                $scope.customerIdSubscribed = $scope.seletedProjectId;
            });
        }, function () {
            $scope.estimates = [];
        });        
    };


    // signalr client functions
    hub.client.addItem = function (item) {
        $scope.estimates.push(item);
        // this is outside of angularjs, so need to apply
        $scope.$apply(); 
    }

    //ShinetechPlanPokerHub.client.updateItem = function (item) {
    //    var array = $scope.complaints;
    //    for (var i = array.length - 1; i >= 0; i--) {
    //        if (array[i].COMPLAINT_ID === item.COMPLAINT_ID) {
    //            array[i].DESCRIPTION = item.DESCRIPTION;
    //            $scope.$apply();
    //        }
    //    }
    //}
    
}]);


