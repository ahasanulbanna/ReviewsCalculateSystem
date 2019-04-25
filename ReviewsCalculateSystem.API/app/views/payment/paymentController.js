(function () {

    'use strict';

    var controllerId = 'paymentController';
    angular.module('app').controller(controllerId, paymentController);
    paymentController.$inject = ['$routeParams', 'notificationService', '$location'];

    function paymentController($routeParams, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
     
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.pageSize = 10;
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
           
        }

     

        function pageChanged() {
            var url = location.url('/invoices');
            location.path(url.$$url).search('pn', vm.pageNumber).search('ps', vm.pageSize).search('q', vm.searchText);
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
