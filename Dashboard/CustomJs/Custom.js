var module = angular.module('myApp', ['ngRoute', 'ngAnimate', 'ngSanitize', 'ui.bootstrap']);

module.constant('Constants', {
    itemsPerPage: 5
});

module.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/Templates/Users.html',
            controller: 'UserController'
        })
        .otherwise({
            redirectTo: '/'
        });
});

//he default hash-prefix used for $location hash-bang URLs has changed from the empty string ('') to the bang ('!').
//https://github.com/angular/angular.js/commit/aa077e81129c740041438688dff2e8d20c3d7b52
//module.config(['$locationProvider', function ($locationProvider) {
//    $locationProvider.hashPrefix('');
//}]);

module.controller('UserController', ["$scope", "$rootScope", "$http", "$window", "UserDataService", "Constants", function ($scope, $rootScope, $http, $window, UserDataService, Constants) {
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
        else 
        {

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

module.controller('ModalController', ["$scope", "$uibModal", "$document", "UserDataService", function ($scope, $uibModal, $document, UserDataService) {

    var $ctrl = this;
    $ctrl.animationsEnabled = true;
    $scope.user = UserDataService;

    $ctrl.openAddUserModal = function () {

        $scope.isNew = true;
        $scope.title = 'Add';
        $scope.user = angular.copy({
            email: '',
            dob: ''
        });

        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            templateUrl: '/Templates/AddEditUser.html',
            controller: 'ModalInstanceController',
            controllerAs: '$ctrl',
            scope: $scope,
            resolve: {}
        });

        modalInstance.result.then(function (status) {
            //ok from modal
            //alert("ok");
        }, function () {
            //cancel from modal
            //alert("dismiss");
        });
    }

    $ctrl.openEditUserModal = function (userId) {

        UserDataService.UserById(userId)
        .then(function (result) {
            //Succes
            $scope.user = result.data;
            $scope.user.dob = new Date($scope.user.dob);

            $scope.master = result.data;
            $scope.master.dob = new Date($scope.user.dob);

            $scope.isNew = false;
            $scope.title = 'Edit';
        },
        function () {
            //error
        });

        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            templateUrl: '/Templates/AddEditUser.html',
            controller: 'ModalInstanceController',
            controllerAs: '$ctrl',
            scope: $scope,
            resolve: {
                data: function () {
                    return angular.copy($scope.user);
                }
            }
        });

        modalInstance.result.then(function (response) {
            //ok from modal
            //alert("ok");
        }, function () {
            //cancel from modal
            //alert("dismiss");
        });
    }


    $ctrl.openDeleteUserModal = function (userId, userName) {

        $scope.UserIdToDelete = userId;
        $scope.UserName = userName;

        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            templateUrl: '/Templates/Confirmation.html',
            controller: 'ModalInstanceController',
            controllerAs: '$ctrl',
            scope: $scope,
            resolve: {}
        });

        modalInstance.result.then(function (response) {
            //ok from modal
        }, function () {
            //cancel from modal
        });
    }
}]);

module.controller('ModalInstanceController', function ($uibModalInstance, $scope, $rootScope, UserDataService, Constants) {
    var $ctrl = this;
    var pagingRequest = { PageNumber: 1, PageSize: Constants.itemsPerPage };

    $ctrl.ok = function (isNewUser) {

        if (isNewUser) {

            UserDataService.AddUser($scope.user)
                .then(function (result) {
                    //Success
                    $uibModalInstance.close("Done");
                    //Update Grid
                    UserDataService.GetUsers(pagingRequest)
                        .then(function (result) {
                            //Succes
                            $rootScope.$broadcast('updateList', { data: result });
                        },
                        function () {
                            //error
                        });
                },
                function () {
                    //error
                    $uibModalInstance.close("Error");
                });
        }
        else if (!isNewUser) {

            UserDataService.EditUser($scope.user)
                .then(function (result) {
                    //Success
                    $uibModalInstance.close("Done");
                    //Update Grid
                    UserDataService.GetUsers(pagingRequest)
                        .then(function (result) {
                            //Succes
                            $rootScope.$broadcast('updateList', { data: result });
                        },
                        function () {
                            //error
                        });
                },
                function () {
                    //error
                    $uibModalInstance.close("Error");
                });
        }
        $ctrl.Reset();
    };

    $ctrl.Delete = function (id) {

        UserDataService.DeleteUser(id)
            .then(function (result) {
                //Success
                $uibModalInstance.close("Done");
                //Update Grid
                UserDataService.GetUsers(pagingRequest)
                .then(function (result) {
                    //Succes
                    $rootScope.$broadcast('updateList', { data: result });
                },
                function () {
                    //error
                });
            },
            function () {
                //error
                $uibModalInstance.close("Error");
            });

    }

    $ctrl.Reset = function () {
        $scope.user = angular.copy({
            email: '',
            dob: ''
        });
    }

    $ctrl.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});

module.factory("UserDataService", ["$http", "$q", function ($http, $q) {

    var _users = [];
    var _user = {};

    var _getUsers = function (pagingRequest) {

        var promise = $http.post("http://localhost:5658/api/users", pagingRequest)
                .then(function (result) {
                    //Succes
                    return result.data;
                },
                function () {
                    //Error
                });
        return promise;
    };

    var _getUserCount = function () {
        var promise = $http.get("http://localhost:5658/api/users/count")
                        .then(function (result) {
                            //Succes
                            return result.data;
                        },
                        function () {
                            //Error
                        });
        return promise;
    };

    var _getUserSearch = function (keyword) {
        var promise = $http.get("http://localhost:5658/api/users/" + keyword + "/search")
                        .then(function (result) {
                            //Succes
                            return result.data;
                        },
                        function () {
                            //Error
                        });
        return promise;
    };

    var _getUserById = function (id) {

        var deferred = $q.defer();

        $http.get("http://localhost:5658/api/users/" + id)
        .then(function (result) {
            //Succes
            angular.copy(result.data, _user);
            deferred.resolve(result);
        },
        function () {
            deferred.reject();
        });

        return deferred.promise;
    };

    var _addUser = function (newUser) {
        var deferred = $q.defer();

        $http.post("http://localhost:5658/api/users/add", newUser)
        .then(function (result) {
            //success
            deferred.resolve();
        },
        function () {
            deferred.reject();
        });

        return deferred.promise;
    };

    var _editUser = function (user) {
        var deferred = $q.defer();

        $http.post("http://localhost:5658/api/users/edit", user)
        .then(function (result) {
            //success
            deferred.resolve();
        },
        function () {
            deferred.reject();
        });

        return deferred.promise;
    }

    var _deleteUser = function (userId) {
        var deferred = $q.defer();

        $http.post("http://localhost:5658/api/users/" + userId + "/delete")
        .then(function (result) {
            //success
            deferred.resolve();
        },
        function () {
            deferred.reject();
        });

        return deferred.promise;
    }

    return {
        Users: _users,
        User: _user,
        UserById: _getUserById,
        GetUsers: _getUsers,
        AddUser: _addUser,
        EditUser: _editUser,
        DeleteUser: _deleteUser,
        UserCount: _getUserCount,
        UserSearch: _getUserSearch
    };
}]);

