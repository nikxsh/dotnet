var module = angular.module('myApp', ['ngRoute']);

module.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/Templates/Users.html',
            controller: 'userController'
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

module.controller('userController', ["$scope", "$http", "$window", "userDataService", function ($scope, $http, $window, userDataService) {
    $scope.Name = "Users";
    $scope.data = userDataService;
    $scope.newUser = {};

    userDataService.GetUsers()
    .then(function (result) {
        //Succes
    },
    function () {
        //error
    });

    $scope.save = function () {        
        userDataService.AddUser($scope.newUser)
            .then(function (result) {
                //Succes
                $window.location.href = "/";
            },
            function () {
                //error
            });
    };

    $scope.resetForm = function (form) {
        angular.copy({}, form);
    }
}]);

module.factory("userDataService", function ($http,$q) {

    var _users = [];
    var _getUsers = function () {

        var deferred = $q.defer();

        $http.get("/api/dashboard/users")
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


    var _addUser = function (newUser) {
        var deferred = $q.defer();

        $http.post("api/dashboard/saveuser", newUser)
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
        GetUsers: _getUsers,
        AddUser: _addUser
    };
});