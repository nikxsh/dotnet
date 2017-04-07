
module.registerCtrl('UserController', ["$scope", "$rootScope", "$http", "$window", "UserDataService", "Constants", function ($scope, $rootScope, $http, $window, UserDataService, Constants) {
    $scope.Name = "Users";
    $scope.pagingRequest = { PageNumber: 1, PageSize: Constants.itemsPerPage, SearchString: '' };
    $scope.maxSize = 10;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;
    $scope.itemsPerPage = Constants.itemsPerPage;
    $scope.pageSize = [{ id: 1, name: "5" }, { id: 2, name: "10" }, { id: 3, name: "50" }];
    $scope.selected = undefined;
    $scope.searchResult = [];

    UserDataService.GetUsers($scope.pagingRequest)
    .then(function (result) {
        //Succes
        $scope.data = result;
    },
    function () {
        //error
    });

    $scope.$on("updateList", function (e, result) {
        $scope.data = result.data;

        UserDataService.UserCount()
        .then(function (data) {
            //success
            $scope.bigTotalItems = data;
        },
        function () {
            //error
        });

        $scope.bigCurrentPage = 1;
    });

    UserDataService.UserCount()
    .then(function (data) {
        //success
        $scope.bigTotalItems = data;
    },
    function () {
        //error
    });

    $scope.pageChanged = function () {
        //what to do on page change
        $scope.pagingRequest.PageNumber = $scope.bigCurrentPage;

        UserDataService.GetUsers($scope.pagingRequest)
        .then(function (result) {
            //Succes
            $scope.data = result;
        },
        function () {
            //error
        });
    };

    $scope.customRowsSelected = function () {

        $scope.pagingRequest.pageSize = $scope.pageSize.name.name;
        $scope.itemsPerPage = $scope.pagingRequest.pageSize;
        UserDataService.GetUsers($scope.pagingRequest)
            .then(function (result) {
                //Succes
                $scope.data = result;
                $scope.bigTotalItems = data.length;
            },
            function () {
                //error
            });
    };

    $scope.search = function () {
        if ($scope.keyword != '' && $scope.keyword != undefined) {

            $scope.pagingRequest.SearchString = $scope.keyword;
            UserDataService.GetUsers($scope.pagingRequest)
                    .then(function (result) {
                        //Succes
                        if (result.length > 0) {
                            $scope.bigTotalItems = result.length;
                            $scope.data = result;
                        }
                        else {
                            UserDataService.UserCount()
                                    .then(function (data) {
                                        //success
                                        $scope.bigTotalItems = data;
                                    },
                                    function () {
                                        //error
                                    });

                            $scope.data = {};
                        }
                    },
                    function () {
                        //error
                    });
        }
        else {

            $scope.pagingRequest.SearchString = '';

            UserDataService.GetUsers($scope.pagingRequest)
           .then(function (result) {
               //Succes
               $scope.data = result;
           },
           function () {
               //error
           });

            UserDataService.UserCount()
            .then(function (data) {
                //success
                $scope.bigTotalItems = data;
            },
            function () {
                //error
            });
        }
    };

}]);