﻿(function () {
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
            $rootScope.admin = $cookieStore.get('admin') || {};
            if ($rootScope.admin.currentUser) {
                $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.admin.currentUser.authdata; // jshint ignore:line
            }

            $rootScope.$on('$locationChangeStart', function (event, next, current) {

                var restrictedPage = $.inArray($location.path(), ['/login']) === -1;
                var loggedIn = $rootScope.admin.currentUser;
                // redirect to login page if not logged in
                if (restrictedPage && !loggedIn) {
                    $location.path('/login');
                }
            });
        }]);

    app.run(['$route', '$rootScope', '$q', function ($route, $rootScope, $q) {
        $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
            if (current.$$route !== undefined) {
                $rootScope.title = current.$$route.title + ' | RCS ADMIN';
            } else {
                $rootScope.title = 'Review Calculate Sytem';
            }
        });
    }]);
})();