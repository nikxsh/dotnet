var module = angular.module('myApp', ['ngRoute']);

module.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/Templates/Users.html',
            controller: 'userController'
        })
        .when('/users', {
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

module.controller('userController', function ($scope, $http) {
    $scope.count = 0;
    $scope.Name = "Users";
    $scope.data = [];

    $http.get("/api/Dhashboard/users")
    .then(function (result) {
        //Succes
        angular.copy(result.data, $scope.data);
        $scope.count = result.data.length;
    },
    function () {
        //error
        alert("Error Occured")
    });
});