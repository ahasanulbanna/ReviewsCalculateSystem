﻿(function () {

    'use strict';

    var controllerId = 'dashboardController';
    angular.module('app').controller(controllerId, dashboardController);
    dashboardController.$inject = ['$routeParams', 'notificationService','productService', '$location', '$rootScope'];

    function dashboardController($routeParams, notificationService, productService, location, $rootScope) {

        /* jshint validthis:true */
        var vm = this;
        vm.loggedIn = {};
        vm.DashbordData = {};

        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }
        Init();
        function Init() {
            vm.loggedIn = $rootScope.admin;
            productService.AdminDashbord().then(function (data) {
                vm.DashbordData = data;             
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }    
    }

})();
