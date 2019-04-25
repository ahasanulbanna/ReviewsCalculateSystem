(function () {

    'use strict';

    var controllerId = 'paymentController';
    angular.module('app').controller(controllerId, paymentController);
    paymentController.$inject = ['$routeParams', 'paymentService', 'notificationService', '$location', '$scope','$rootScope'];

    function paymentController(routeParams, paymentService, notificationService, location, $scope, $rootScope) {

        /* jshint validthis:true */
        var vm = this;
        vm.ReviewerId = 0;
        vm.loggedIn = {};
        vm.PaymentLog = {};
        vm.ReviewerPayment = [];
        vm.ReviewerPaymentDetails = [];
        vm.Payment = Payment;
        vm.reviewerDetails = reviewerDetails;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.pageSize = 10;
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
            vm.loggedIn = $rootScope.admin.currentUser;
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

        function Payment(rp) {
            vm.PaymentLog.ReviewerId = rp.ReviewerId;
            vm.PaymentLog.ReviewerName = rp.ReviewerName;
            vm.PaymentLog.TotalPaymentAmount = rp.TotalPaymentAmount;
            vm.PaymentLog.AdminId = vm.loggedIn.AdminId;
            paymentService.paymentLog(vm.PaymentLog).then(function (data) {
                notificationService.displaySuccess("Payment Ok");
                init();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);

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
