﻿(function () {

    'use strict';

    var controllerId = 'currentproductController';
    angular.module('app').controller(controllerId, currentproductController);
    currentproductController.$inject = ['$routeParams', 'taskasignService', 'notificationService', '$location'];

    function currentproductController($routeParams, taskasignService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.currentProductList = [];
        vm.taskAsign = taskAsign;
        vm.addProduct = addProduct;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.pageSize = 4;
        vm.onSearch = onSearch;
        vm.pageNumber = 1;
        vm.total = 0;

        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }
        init();
        function init() {
            taskasignService.GetAllCurrentProductList(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.currentProductList = data.CurrentProducts;
                vm.total = data.Total;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function taskAsign(cp) {
            var url = location.url('/task-asign/' + cp.ProductId);
            location.path(url.$$url);
        }

        function addProduct() {
            var url = location.url('/product-add');
            location.path(url.$$url);
        }
   

        function pageChanged() {
            var url = location.url('/task-asign');
            location.path(url.$$url).search('pn', vm.pageNumber).search('ps', vm.pageSize).search('q', vm.searchText);
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
