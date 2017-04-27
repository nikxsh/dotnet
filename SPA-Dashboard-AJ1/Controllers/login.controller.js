
module.controller('LoginController', ['$scope', '$window', 'svcAuthentication', function ($scope, $window, svcAuthentication) {

    $scope.title = 'Success';
    $scope.IsValidSession = false;
    $scope.credentials = {};

    $scope.$on("setUserSession", function (e, result) {
        alert(result.Data)
        $scope.IsValidSession = result.Data;
    });

    $scope.SetDefault = function () {
        $scope.IsValidSession = false;
    };

    $scope.login = function () {

        //Check if already authenticated
        if ($scope.IsValidSession)
            $window.location.href = "#!/index";

        //If not do it
        if (svcAuthentication) {
            $scope.IsValidSession = true;
            $window.location.href = "#!/index";
        }
        else
            $window.location.href = "#!/login";
    };

    $scope.logout = function () {

        if ($scope.IsValidSession)
            $scope.IsValidSession = false;

        $window.location.href = "#!/login";
    };

}]);