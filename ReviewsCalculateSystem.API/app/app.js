(function () {
    'use strict';

    var app = angular.module('app', [

        // Angular modules 
        'ngAnimate',
        'ngRoute',
        'ngSanitize',
        'ngResource',
        'ngCookies',
        'ui.router',
        'ui.bootstrap',
        'common',

        // 3rd Party Modules
        'common.services'
    ]);

    app.run(['$rootScope', '$location', '$cookieStore', '$http',
        function ($rootScope, $location, $cookieStore, $http) {
            // keep user logged in after page refresh
            $rootScope.globals = $cookieStore.get('globals') || {};
            if ($rootScope.globals.currentUser) {
                $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata; // jshint ignore:line
            }

            $rootScope.$on('$locationChangeStart', function (event, next, current) {
                // redirect to login page if not logged in
                if ($location.path() !== '/login' && !$rootScope.globals.currentUser) {
                    $location.path('/login');
                }
            });
        }]);

    app.run(['$route', '$rootScope', '$q', function ($route, $rootScope, $q) {
        $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
            if (current.$$route !== undefined) {
                $rootScope.title = current.$$route.title + ' | BPSC ADMIN';
            } else {
                $rootScope.title = 'E-Recruitment System';
            }
        });
    }]);
})();



    