(function () {
    'use strict';
    angular.module('app').service('paymentService', ['dataConstants', 'apiHttpService', paymentService]);

    function paymentService(dataConstants, apiHttpService) {
        var service = {
            reviewerPayment: reviewerPayment,
            reviewerDetails: unpaidReviewCalculateByReviewerId,
            GetProductById: GetProductById,
            paymentLog: paymentLog,
            paymentDetalsByReviewerId: paymentDetalsByReviewerId
        };

        return service;
       
        function reviewerPayment() {
            var url = dataConstants.PAYMENT_URL + 'reviewerPayment';
            return apiHttpService.GET(url);
        }

        function unpaidReviewCalculateByReviewerId(Id) {
            var url = dataConstants.PAYMENT_URL + 'unpaidReviewCalculateByReviewerId/'+Id;
            return apiHttpService.GET(url);
        }

        function GetProductById(ProductId) {
            var url = dataConstants.PRODUCT_URL + 'GetProductById/' + ProductId;
            return apiHttpService.GET(url);
        }
        function paymentLog(Data) {
            var url = dataConstants.PAYMENT_URL +'payAmountLog';
            return apiHttpService.POST(url, Data);
        }

        function paymentDetalsByReviewerId(reviewerId) {
            var url = dataConstants.PAYMENT_URL + 'payment-details-by-reviewer-id?reviewerId=' + reviewerId;
            return apiHttpService.GET(url);
        }
    }
})();