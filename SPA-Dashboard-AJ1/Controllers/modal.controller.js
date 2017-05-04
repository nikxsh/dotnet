var fnModalCtrl = function ($scope, $uibModal, $document, UserDataService) {

    var $ctrl = this;
    $ctrl.animationsEnabled = true;
    $scope.user = UserDataService;

    $ctrl.OpenAddUserModal = function () {

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

    $ctrl.OpenEditUserModal = function (userId) {

        UserDataService.UserById(userId)
        .then(function (result) {
            //Succes
            $scope.user = result.data.responseData;
            $scope.user.dob = new Date($scope.user.dob);

            $scope.master = result.data.responseData;
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


    $ctrl.OpenDeleteUserModal = function (userId, userName) {

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
};