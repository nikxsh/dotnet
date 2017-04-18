
module.registerCtrl('UserController', ["$scope", "$rootScope", "$http", "$window", "UserDataService", "Constants", function ($scope, $rootScope, $http, $window, UserDataService, Constants) {
    $scope.Name = "Users";
    $scope.PagingRequest = { PageNumber: 1, PageSize: Constants.PageTabelRowsSize, SearchString: '' };
    $scope.maxSize = 10;
    $scope.BigTotalItems = 0;
    $scope.BigCurrentPage = 1;
    $scope.ItemsPerPage = Constants.PageTabelRowsSize;
    $scope.PagedList = [{ id: 1, name: "5" }, { id: 2, name: "10" }, { id: 3, name: "50" }];
    $scope.FirstTimeLoad = true;
    $scope.UserData = [];

    if ($scope.FirstTimeLoad) {

        UserDataService.UserCount()
        .then(function (data) {
            //success
            $scope.BigTotalItems = data;
        },
        function () {
            //error
        });

        UserDataService.GetUsers($scope.PagingRequest)
        .then(function (result) {
            //Succes
            $scope.UserData = result;
        },
        function () {
            //error
        });

        $scope.FirstTimeLoad = false;
    }

    $scope.$on("updateList", function (e, result) {
        $scope.UserData = result;

        UserDataService.UserCount()
        .then(function (data) {
            //success
            $scope.BigTotalItems = data;
        },
        function () {
            //error
        });

        $scope.BigCurrentPage = 1;
    });

    $scope.PageChanged = function () {
        //what to do on page change
        $scope.PagingRequest.PageNumber = $scope.BigCurrentPage;

        UserDataService.GetUsers($scope.PagingRequest)
        .then(function (result) {
            //Succes
            $scope.UserData = result;
        },
        function () {
            //error
        });
    };

    $scope.CustomRowsSelected = function () {
        
        $scope.PagingRequest.PageSize = $scope.PagedList.name.name;
        $scope.ItemsPerPage = $scope.PagingRequest.PageSize;
        $scope.PagingRequest.PageNumber = 1;

        UserDataService.GetUsers($scope.PagingRequest)
            .then(function (result) {   
                //Succes
                $scope.UserData = result;
            },
            function () {
                //error
            });
    };

    $scope.Search = function () {
        if ($scope.keyword != '' && $scope.keyword != undefined) {

            $scope.PagingRequest.SearchString = $scope.keyword;
            UserDataService.GetUsers($scope.PagingRequest)
                    .then(function (result) {
                        //Succes
                        if (result.length > 0) {
                            $scope.BigTotalItems = result.length;
                            $scope.UserData = result;
                        }
                        else {
                            UserDataService.UserCount()
                                    .then(function (data) {
                                        //success
                                        $scope.BigTotalItems = data;
                                    },
                                    function () {
                                        //error
                                    });

                            $scope.UserData = {};
                        }
                    },
                    function () {
                        //error
                    });
        }
        else {

            $scope.PagingRequest.SearchString = '';

            UserDataService.GetUsers($scope.PagingRequest)
           .then(function (result) {
               //Succes
               $scope.UserData = result;
           },
           function () {
               //error
           });

            UserDataService.UserCount()
            .then(function (data) {
                //success
                $scope.BigTotalItems = data;
            },
            function () {
                //error
            });
        }
    };

}]);