var module = angular.module('myApp', ['ngRoute', 'ngAnimate', 'ngSanitize', 'ui.bootstrap']);

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

module.controller('UserController', ["$scope", "$http", "$window", "UserDataService", function ($scope, $http, $window, UserDataService) {
    $scope.Name = "Users";
    $scope.data = UserDataService;


    UserDataService.GetUsers()
    .then(function (result) {
        //Succes
    },
    function () {
        //error
    });

    $scope.$on("updateList", function (e, a) {

        $scope.data = a;

    });

}]);

module.controller('ModalController', ["$scope", "$uibModal", "$document", "UserDataService", function ($scope,$uibModal, $document, UserDataService) {

    var $ctrl = this;
    $ctrl.animationsEnabled = true;
    $scope.user = UserDataService;

    $ctrl.openAddUserModal = function () {

        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            templateUrl: '/Templates/AddUser.html',
            controller: 'ModalInstanceController',
            controllerAs: '$ctrl',
            resolve: {}
        });

        modalInstance.result.then(function (status) {
            //ok from modal
        }, function () {
            //cancel from modal
        });
    }

    $ctrl.openEditUserModal = function (userId) {

        UserDataService.UserById(userId)
        .then(function (result) {
            //Succes
            $scope.user = result.data;
            $scope.user.dob = new Date($scope.user.dob);
        },
        function () {
            //error
        });

        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            templateUrl: '/Templates/AddUser.html',
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
        }, function () {
            //cancel from modal
        });
    }
}]);

module.controller('ModalInstanceController', function ($uibModalInstance, $scope, UserDataService) {
    var $ctrl = this;
    var result = UserDataService;

    $ctrl.ok = function () {

        UserDataService.AddUser($scope.user)
            .then(function (result) {
                //Success
                $uibModalInstance.close("Done");
                //Update Grid
                UserDataService.GetUsers();
                $rootScope.$broadcast('updateList', { data: result });
            },
            function () {
                //error
                $uibModalInstance.close("Error");
            });
    };

    $ctrl.Reset = function (form) {
        angular.copy({}, form);
    }

    $ctrl.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});


module.factory("UserDataService", ["$http", "$q", function ($http, $q) {

    var _users = [];
    var _user = {};
    var _getUsers = function () {

        var deferred = $q.defer();

        $http.get("http://localhost:5658/api/users")
        .then(function (result) {
            //Succes
            angular.copy(result.data, _users);
            deferred.resolve();
        },
        function () {
            deferred.reject();
        });

        return deferred.promise;
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
            var createdTopic = result.data;
            deferred.resolve(createdTopic);
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
        AddUser: _addUser
    };
}]);

