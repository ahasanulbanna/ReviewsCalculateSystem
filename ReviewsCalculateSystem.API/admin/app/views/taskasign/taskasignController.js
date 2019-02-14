(function () {

    'use strict';

    var controllerId = 'taskasignController';
    angular.module('app').controller(controllerId, taskasignController);
    taskasignController.$inject = ['$routeParams', 'taskasignService', 'notificationService', '$location'];

    function taskasignController(routeParams, taskasignService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.ProductId = 0;
        vm.productDetails = {};
        vm.reviewerList = [];
        vm.taskAsign = taskAsign;
        vm.updateInvoice = updateInvoice;
        vm.deleteInvoice = deleteInvoice;
        vm.invoiceView = invoiceView;
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


        if (routeParams.ProductId !== undefined && routeParams.ProductId !== '') {
            vm.ProductId = routeParams.ProductId;
        }

        init();
        function init() {
            taskasignService.reviewerDetailsInfoList().then(function (data) {
                vm.reviewerList = data;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            taskasignService.GetProductById(vm.ProductId).then(function (data) {
                vm.productDetails = data;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function taskAsign(cp) {
            var url = location.url('/task-asign/' + cp.ProductId);
            location.path(url.$$url);
        }

        function updateInvoice(invoice) {
            var url = location.url('/invoice-modify/' + invoice.invoiceId);
            location.path(url.$$url);
        }
        function invoiceView(invoice) {
            var url = location.url('/invoice-view/' + invoice.invoiceId);
            location.path(url.$$url);
        }
        function deleteInvoice(invoice) {
            invoiceService.deleteInvoice(invoice.invoiceId).then(function (data) {
                init();
            });
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
