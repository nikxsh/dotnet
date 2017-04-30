var module = angular.module('SPADashBoardAJ1', ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'ui.router']);

module.constant('Constants', {
    PageTabelRowsSize: 5
});

module.config(['$routeProvider', '$controllerProvider', function ($routeProvider, $controllerProvider) {

    module.registerCtrl = $controllerProvider.register;

    $routeProvider
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
}]);

module.run(['$rootScope', '$window', '$location', 'localStorage', function ($rootScope, $window, $location, localStorage) {

    $rootScope.$watch(function () {

        var isLoggedIn = localStorage.GetAuthData();

        //if not logged in
        if (!isLoggedIn)
            $window.location.href = "#!/login";

        var currentPath = $location.path().split("/")[1] || "Unknown";

        //If state is login page but you're logged in already
        if (isLoggedIn && currentPath === 'login')
            $location.path('index');

        return;
    });

    var isLoggedIn = localStorage.GetAuthData();

    //NOT authenticated 
    if (!isLoggedIn) {

        $window.location.href = "#!/login";
        return;
    }

    //authenticated already
    if (isLoggedIn) {

        $window.location.href = "#!/index";
        return;
    }

}]);
