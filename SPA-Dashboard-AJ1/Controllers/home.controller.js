
module.controller('HomeController', ['$scope', '$window', function ($scope, $window) {

    $scope.title = 'I am Blank';

    $scope.messages = [
                       { id: 1, name: 'Nikhilesh Shinde', date: 'Today', message: 'Hello there!!!' },
                       { id: 1, name: 'Asawari lol', date: 'Yesterday', message: 'Why this kolawari' },
                       { id: 1, name: 'John Smith', date: 'Yesterday', message: 'Sky full of lighters!' }
    ];

    $scope.tasks = [
                   { id: 1, name: 'Task 1', status: '40%', progress: 'success', value: 40 },
                   { id: 1, name: 'Task 2', status: '60%', progress: 'warning', value: 60 },
                   { id: 1, name: 'Task 3', status: '80%', progress: 'danger', value: 80 }
    ];
}]);