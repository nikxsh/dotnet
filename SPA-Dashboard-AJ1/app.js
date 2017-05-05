//-------------------------------------------------------------------------------------------------------------------------------------|
//  - The angular.module is a global place for creating, registering and retrieving AngularJS modules. 
//  - All modules (AngularJS core or 3rd party) that should be available to an application must be registered using this mechanism
//  - Passing one argument retrieves an existing angular.Module, whereas passing more than one argument creates a new angular.Module
//  - angular.module is used to configure the $injector. 
//-------------------------------------------------------------------------------------------------------------------------------------|
var module = angular.module('SPADashBoardAJ1', ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'ui.router']);


//-------------------------------------------------------------------------------------------------------------------------------------|
//  - A module is a collection of services, directives, controllers, filters, and configuration information. 
//  - All modules (AngularJS core or 3rd party) that should be available to an application must be registered using this mechanism
//  - Usage:  angular.module(name, [requires], [configFn]);
//      name => string (The name of the module to create or retrieve)
//
//      requires  (optional) => !Array.<string>= (If specified then new module is being created. If unspecified then the module is being 
//                                                retrieved for further configuration)
//
//      configFn  (optional) => Function= (Optional configuration function for the module. Same as Module#config())
//-------------------------------------------------------------------------------------------------------------------------------------|

module.constant('Constants', constantsArray);

//-------------------------------------------------------------------------------------------------------------------------------------|
// - Provider-injector
// - Get executed during the provider registrations and configuration phase. 
// - Only providers and constants can be injected into configuration blocks. This is to prevent accidental instantiation of services 
//   before they have been fully configured.
//-------------------------------------------------------------------------------------------------------------------------------------|
fnConfig.$inject = ['$routeProvider', '$controllerProvider', '$httpProvider'];
module.config(fnConfig);


//-------------------------------------------------------------------------------------------------------------------------------------|
//  - Instance-injector
//  - Get executed after the injector is created and are used to kickstart the application. 
//  - Only instances and constants can be injected into run blocks. This is to prevent further system configuration during application 
//    run time.
//  - It is executed after all of the services have been configured and the injector has been created
//-------------------------------------------------------------------------------------------------------------------------------------|
fnAppRun.$inject = ['$rootScope', '$window', '$location', 'sessionStorage']
module.run(fnAppRun);




//------------------------- Service, factory, controller and filter injection ---------------------------------------|
fnUserCtlr.$inject = ['$scope', 'UserDataService', 'Constants'];
fnLoginCtrl.$inject = ['$scope', '$window', 'authentication', 'sessionStorage'];
fnModalCtrl.$inject = ['$scope', '$uibModal', '$document', 'UserDataService'];
fnModalInstanceCtlr.$inject = ['$uibModalInstance', '$scope', '$rootScope', 'UserDataService', 'Constants'];

fnLocalStorage.$inject = ['$window'];
fnSessionStorage.$inject = ['$window'];

fnTimeDir.$inject = ['$interval', 'dateFilter'];

fnAuthSvc.$inject = ['$http', '$q'];
fnUserDataSvc.$inject = ['$http', '$q'];
fnAuthInterceptorSvc.$inject = ['$location', '$q', 'sessionStorage'];

//------------------------- Service, factory, controller and filter intialisation -----------------------------------|
module.controller('UserController', fnUserCtlr);
module.controller('ModalController', fnModalCtrl);
module.controller('ModalInstanceController', fnModalInstanceCtlr);
module.controller('LoginController', fnLoginCtrl);

module.filter('ToUpperCase', fnUpperFltr);

module.directive('currentTime', fnTimeDir);

module.factory('localStorage', fnLocalStorage);
module.factory('sessionStorage', fnSessionStorage);

module.factory('authentication', fnAuthSvc);
module.service('UserDataService', fnUserDataSvc);
module.factory('authInterceptor', fnAuthInterceptorSvc);