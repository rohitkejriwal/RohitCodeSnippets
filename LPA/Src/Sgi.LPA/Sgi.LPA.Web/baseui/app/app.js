var app = angular.module('myApp', ['ngRoute']);

app.config(['$routeProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.
    when('/home', {
        templateUrl: '../views/homepage/homepage.html',
        controller: 'homepage'
    }).
    otherwise({
        redirectTo: '/home'
    });
}]);