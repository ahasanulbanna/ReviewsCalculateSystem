(function () {
    'use strict';
    angular.module('app').service('paymentService', ['dataConstants', 'apiHttpService', paymentService]);

    function paymentService(dataConstants, apiHttpService) {
        var service = {
            reviewerPayment: reviewerPayment,
            reviewerDetails: unpaidReviewCalculateByReviewerId,
            GetProductById: GetProductById,
            AddProduct: AddProduct,
            deleteStudent: deleteStudent
        };

        return service;
        //function reviewerPayment(pageSize, pageNumber, searchText) {
        //    var url = dataConstants.PRODUCT_URL + 'GetAllProductList?pageSize=' + pageSize + "&pageNumber=" + pageNumber + "&searchText=" + searchText;
        //    return apiHttpService.GET(url);
        //}

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

        function AddProduct(data) {
            var url = dataConstants.PRODUCT_URL + 'AddProduct';
            return apiHttpService.POST(url, data);
        }

        function deleteStudent(studentId) {
            var url = dataConstants.STUDENT_URL + 'delete-student/' + studentId;
            return apiHttpService.DELETE(url);
        }


    }
})();