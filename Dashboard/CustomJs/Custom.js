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

module.controller('ModalController', ["$uibModal", "$document", function ($uibModal, $document) {

    var $ctrl = this;

    $ctrl.animationsEnabled = true;

    $ctrl.open = function (size, parentSelector) {
        
        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Templates/AddUser.html',
            controller: 'ModalInstanceController',
            controllerAs: '$ctrl',
            size: size,
            resolve: {}
        });

        modalInstance.result.then(function (status) {
            //ok from modal
        }, function () {
            //cancel from modal
        });
    }
}]);

module.controller('ModalInstanceController', function ($uibModalInstance, $scope, UserDataService) {
    var $ctrl = this;
    var result = UserDataService;
    $scope.newUser = {};

    $ctrl.ok = function () {
        UserDataService.AddUser($scope.newUser)
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
}]);