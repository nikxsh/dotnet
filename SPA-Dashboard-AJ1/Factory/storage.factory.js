
module.factory('localStorage', ['$window', '$rootScope', function ($window, $rootScope) {
  
    return {
        SetAuthData: function (val) {
            $window.localStorage && $window.localStorage.setItem('is-logged-in', val);
            return this;
        },
        GetAuthData: function () {
            var data = $window.localStorage && $window.localStorage.getItem('is-logged-in');
            return (data != undefined && data != null && data == 'true');
        }
    };

}]);