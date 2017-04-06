
module.controller('LoginController', ["$scope", '$window', function ($scope, $window) {

    $scope.title = 'Success';

    $scope.login = function () {
        alert("Success");
    };
}]);