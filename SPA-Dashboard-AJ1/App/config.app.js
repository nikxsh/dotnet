var fnConfig = function (routeProvider, controllerProvider, httpProvider) {

    module.registerCtrl = controllerProvider.register;

    httpProvider.interceptors.push('authInterceptor');

    routeProvider
        .when('/', {
            templateUrl: '/Templates/Common/Blank.html'
        })
        .when('/login', {
            templateUrl: '/Templates/Common/Login.html'
        })
        .when('/logout', {
            templateUrl: '/Templates/Common/Logout.html'
        })
        .when('/index', {
            templateUrl: '/Templates/Common/Blank.html'
        })
        .when('/users', {
            templateUrl: '/Templates/User/Users.html'
        })
        .when('/dashboard', {
            templateUrl: '/Templates/Dashboard/Dashboard.html'
        })
        .when('/flotchart', {
            templateUrl: '/Templates/Dashboard/Flot.html'
        })
        .when('/morrischart', {
            templateUrl: '/Templates/Dashboard/Morris.html'
        })
        .when('/tables', {
            templateUrl: '/Templates/Dashboard/Table.html'
        })
        .when('/forms', {
            templateUrl: '/Templates/Dashboard/Forms.html'
        })
        .when('/panels', {
            templateUrl: '/Templates/Dashboard/Panels.html'
        })
        .when('/buttons', {
            templateUrl: '/Templates/Dashboard/Buttons.html'
        })
        .when('/notifications', {
            templateUrl: '/Templates/Dashboard/Notifications.html'
        })
        .when('/typography', {
            templateUrl: '/Templates/Dashboard/Typography.html'
        })
        .when('/icons', {
            templateUrl: '/Templates/Dashboard/Icons.html'
        })
        .when('/grid', {
            templateUrl: '/Templates/Dashboard/Grid.html'
        })
        .otherwise({
            redirectTo: '/'
        });
};