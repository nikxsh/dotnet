
module.factory('localStorage', ['$window', '$rootScope', function ($window, $rootScope) {
  
    return {
        setData: function (val) {
            $window.localStorage && $window.localStorage.setItem('is-logged-in', val);
            return this;
        },
        getData: function () {
            var data = $window.localStorage && $window.localStorage.getItem('is-logged-in');
            return (data == 'true');
        }
    };

}]);