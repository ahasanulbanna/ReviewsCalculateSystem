(function () {

    'use strict';

    var controllerId = 'reviewcheckController';
    angular.module('app').controller(controllerId, reviewcheckController);
    reviewcheckController.$inject = ['$routeParams', 'reviewcheckService', 'notificationService', '$location', '$scope'];

    function reviewcheckController(routeParams, reviewcheckService, notificationService, location, $scope) {

        /* jshint validthis:true */
        var vm = this;
        vm.AdminId = 3;
        vm.ProductId = 0;
        vm.totalMargin = 0;
        vm.productDetails = {};
        vm.currentReviewerInfo = [];
        vm.productList = [];
        vm.selectedReviewer = [];
        vm.reviewCheck = reviewCheck;
        vm.productReviewCheck = productReviewCheck;
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

        if (routeParams.ProductId !== undefined && routeParams.ProductId !== '') {
            vm.ProductId = routeParams.ProductId;
        }

        init();
        function init() {
            reviewcheckService.GetReviewByProductId(vm.ProductId).then(function (data) {
                vm.producReviewtList = data;
                console.log(vm.producReviewtList);
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });


        }


        
        function reviewCheck(review) {
            reviewcheckService.AdminReviewUpdateByChecking(review).then(function () {
                
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function productReviewCheck(ProductId) {
            var url = location.url('/review-check/Product/' + ProductId);
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
