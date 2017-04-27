
module.controller('LoginController', ['$scope', '$rootScope', '$window', 'svcAuthentication', function ($scope, $rootScope, $window, svcAuthentication) {

    $scope.title = 'Success';
    $scope.credentials = {};
    $scope.ShowDiv = false;

    $scope.login = function () {

        //Check if already authenticated
        if ($rootScope.IsValidSession !== undefined && $rootScope.IsValidSession) {
            $window.location.href = "#!/index";
        }
        else {
            //If not do it
            if (svcAuthentication) {
                $rootScope.IsValidSession = true;
                $scope.ShowDiv = true;
                $window.location.href = "#!/index";
            }
            else
                $window.location.href = "#!/login";
        }

    };

    $scope.logout = function () {
        $rootScope.IsValidSession = false;
        $scope.ShowDiv = false;
        $window.location.href = "#!/login";
    };

}]);