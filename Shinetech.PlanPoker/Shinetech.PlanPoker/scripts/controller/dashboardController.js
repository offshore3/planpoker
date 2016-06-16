appModule.controller('dashboardController', ['$scope', '$cookieStore', 'dashboardService', 'projectService', function ($scope,$cookieStore, dashboardService, projectService) {
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

    $scope.selectedPoker = {
        text: "?",
        data: ""
    };

    $scope.loadProjects = function () {
        var command = {
            queryText: '',
            pageNumber: 1,
            pageCount: 12
        };

        $scope.projectCode = getQueryVariable('code');
        dashboardService.decryptProjectCode($scope.projectCode, function (data) {
            console.log(data);
            $scope.projectId = data;
        });

        projectService.queryProjects(command, function (data) {
            $scope.projects = data.ProjectViewModels;
        }, function () {

        });
    };

    $scope.cardSelect = function (poker) {
        console.log(poker);
        console.log($scope.projectId);
        $scope.isFloat = poker.data;
        $scope.selectedPoker.data = poker.data;
        $scope.selectedPoker.text = "√";

        var command = {
            ProjectId: $scope.projectId,
            UserId: $cookieStore.get("LoginUserId"),
            SelectedPoker: poker.data
        };

        dashboardService.selectCard(command, function () {

        }, function () {

        });
    }

}]);


