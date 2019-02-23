(function () {
    'use strict';
    angular.module('app').service('reviewcheckService', ['dataConstants', 'apiHttpService', reviewcheckService]);

    function reviewcheckService(dataConstants, apiHttpService) {
        var service = {
            GetAllProductList: GetAllProductList,
            reviewerDetailsInfoList: reviewerDetailsInfoList,
            GetReviewByProductId: GetReviewByProductId,
            AddProduct: AddProduct,
            deleteStudent: deleteStudent
        };

        return service;
        function GetAllProductList(pageSize, pageNumber, searchText) {
            var url = dataConstants.PRODUCT_URL + 'GetAllProductList?pageSize=' + pageSize + "&pageNumber=" + pageNumber + "&searchText=" + searchText;
            return apiHttpService.GET(url);
        }

        function reviewerDetailsInfoList(pageSize, pageNumber, searchText) {
            var url = dataConstants.REVIEWERTASKASIGN_URL + 'reviewerDetailsInfoList?pageSize=' + pageSize + "&pageNumber=" + pageNumber + "&searchText=" + searchText;
            return apiHttpService.GET(url);
        }

        function GetReviewByProductId(ProductId) {
            var url = dataConstants.REVIEW_URL + 'GetReviewByProductId/' + ProductId;
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