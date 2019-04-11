(function () {

    'use strict';

    var controllerId = 'paymentController';
    angular.module('app').controller(controllerId, paymentController);
    paymentController.$inject = ['$routeParams', 'paymentService', 'notificationService', '$location', '$scope'];

    function paymentController(routeParams, paymentService, notificationService, location, $scope) {

        /* jshint validthis:true */
        var vm = this;
        vm.ReviewerId = 0;
        vm.ReviewerPayment = [];
        vm.ReviewerPaymentDetails = [];
        vm.reviewerDetails = reviewerDetails;
        vm.addProduct = addProduct;
        vm.updateInvoice = updateInvoice;
        vm.deleteInvoice = deleteInvoice;
        vm.invoiceView = invoiceView;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.pageSize = 5;
        vm.onSearch = onSearch;
        vm.close = close;
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


        if (routeParams.ReviewerId !== undefined && routeParams.ReviewerId !== '') {
            vm.ReviewerId = routeParams.ReviewerId;
        }

        init();
        function init() {
            paymentService.reviewerPayment().then(function (data) {
                vm.ReviewerPayment = data;
                console.log(vm.ReviewerPayment);

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            if (vm.ReviewerId > 0) {
                paymentService.reviewerDetails(vm.ReviewerId).then(function (data) {
                    vm.ReviewerPaymentDetails = data;

                },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
            }



        }

        function reviewerDetails(ReviewerId) {
            var url = location.url('/reviewer-payment-details/' + ReviewerId);
            location.path(url.$$url);
        }

        function addProduct() {
            var url = location.url('/product-add');
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
            invoiceService.taskAsign(invoice.invoiceId).then(function (data) {
                init();
            });
        }

        function close() {
            var url = "/task-asign";
            location.path(url);
        }

        function pageChanged() {
            var url = location.url('/task-asign/' + vm.ProductId);
            location.path(url.$$url).search('pn', vm.pageNumber).search('ps', vm.pageSize).search('q', vm.searchText);
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
