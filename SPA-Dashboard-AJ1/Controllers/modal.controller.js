module.registerCtrl('ModalController', ["$scope", "$uibModal", "$document", "UserDataService", function ($scope, $uibModal, $document, UserDataService) {

    var $ctrl = this;
    $ctrl.animationsEnabled = true;
    $scope.user = UserDataService;

    $ctrl.openAddUserModal = function () {

        $scope.isNew = true;
        $scope.title = 'Add';
        $scope.user = angular.copy({
            email: '',
            dob: ''
        });

        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            templateUrl: '/Templates/User/AddEditUser.html',
            controller: 'ModalInstanceController',
            controllerAs: '$ctrl',
            scope: $scope,
            resolve: {}
        });

        modalInstance.result.then(function (status) {
            //ok from modal
            //alert("ok");
        }, function () {
            //cancel from modal
            //alert("dismiss");
        });
    }

    $ctrl.openEditUserModal = function (userId) {

        UserDataService.UserById(userId)
        .then(function (result) {
            //Succes
            $scope.user = result.data;
            $scope.user.dob = new Date($scope.user.dob);

            $scope.master = result.data;
            $scope.master.dob = new Date($scope.user.dob);

            $scope.isNew = false;
            $scope.title = 'Edit';
        },
        function () {
            //error
        });

        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            templateUrl: '/Templates/User/AddEditUser.html',
            controller: 'ModalInstanceController',
            controllerAs: '$ctrl',
            scope: $scope,
            resolve: {
                data: function () {
                    return angular.copy($scope.user);
                }
            }
        });

        modalInstance.result.then(function (response) {
            //ok from modal
            //alert("ok");
        }, function () {
            //cancel from modal
            //alert("dismiss");
        });
    }


    $ctrl.openDeleteUserModal = function (userId, userName) {

        $scope.UserIdToDelete = userId;
        $scope.UserName = userName;

        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            templateUrl: '/Templates/User/Confirmation.html',
            controller: 'ModalInstanceController',
            controllerAs: '$ctrl',
            scope: $scope,
            resolve: {}
        });

        modalInstance.result.then(function (response) {
            //ok from modal
        }, function () {
            //cancel from modal
        });
    }
}]);

module.registerCtrl('ModalInstanceController', function ($uibModalInstance, $scope, $rootScope, UserDataService, Constants) {
    var $ctrl = this;
    var pagingRequest = { PageNumber: 1, PageSize: Constants.itemsPerPage };

    $ctrl.ok = function (isNewUser) {

        if (isNewUser) {

            UserDataService.AddUser($scope.user)
                .then(function (result) {
                    //Success
                    $uibModalInstance.close("Done");
                    //Update Grid
                    UserDataService.GetUsers(pagingRequest)
                        .then(function (result) {
                            //Succes
                            $rootScope.$broadcast('updateList', { data: result });
                        },
                        function () {
                            //error
                        });
                },
                function () {
                    //error
                    $uibModalInstance.close("Error");
                });
        }
        else if (!isNewUser) {

            UserDataService.EditUser($scope.user)
                .then(function (result) {
                    //Success
                    $uibModalInstance.close("Done");
                    //Update Grid
                    UserDataService.GetUsers(pagingRequest)
                        .then(function (result) {
                            //Succes
                            $rootScope.$broadcast('updateList', { data: result });
                        },
                        function () {
                            //error
                        });
                },
                function () {
                    //error
                    $uibModalInstance.close("Error");
                });
        }
        $ctrl.Reset();
    };

    $ctrl.Delete = function (id) {

        UserDataService.DeleteUser(id)
            .then(function (result) {
                //Success
                $uibModalInstance.close("Done");
                //Update Grid
                UserDataService.GetUsers(pagingRequest)
                .then(function (result) {
                    //Succes
                    $rootScope.$broadcast('updateList', { data: result });
                },
                function () {
                    //error
                });
            },
            function () {
                //error
                $uibModalInstance.close("Error");
            });

    }

    $ctrl.Reset = function () {
        $scope.user = angular.copy({
            email: '',
            dob: ''
        });
    }

    $ctrl.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});