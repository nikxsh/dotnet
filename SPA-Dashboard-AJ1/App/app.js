var module = angular.module('SPADashBoardAJ1', ['ngRoute', 'ngAnimate', 'ui.bootstrap']);

module.constant('Constants', {
    itemsPerPage: 5
});

module.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/Templates/Blank.html',
            controller: 'HomeController'
        })
        .when('/home', {
            templateUrl: '/Templates/Blank.html',
            controller: 'HomeController'
        })
        .when('/login', {
            templateUrl: '/Templates/Login.html',
            controller: 'LoginController'
        })
        .otherwise({
            redirectTo: '/'
        });
});