
module.controller('LoginController', ['$scope', '$rootScope', '$window', '$routeParams', 'svcAuthentication', 'localStorage', function ($scope, $rootScope, $window, $routeParams, svcAuthentication, localStorage) {

    $scope.Credentials = {};
    $scope.ShowDiv = localStorage.getData();
    $scope.Message = "";

    $scope.login = function () {

        //Check if already authenticated
        if ($rootScope.IsValidSession !== undefined && $rootScope.IsValidSession) {
            $window.location.href = "#!/index";
        }
        else {
            //If not do it
            if (svcAuthentication.validateUser($scope.Credentials)) {
                $rootScope.IsValidSession = true;
                $scope.ShowDiv = true;
                localStorage.setData(true);
                $scope.Credentials = {};
                $window.location.href = "#!/index";
            }
            else {
                localStorage.setData(false);
                $scope.Credentials = {};
                $scope.Message = "Invalid Credentials!"
            }
        }

    };

    $scope.logout = function () {
        $rootScope.IsValidSession = false;
        $scope.ShowDiv = false;
        localStorage.setData(false);
        $window.location.href = "#!/login";
    };

}]);