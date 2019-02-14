(function () {
    'use strict';
    angular.module('app').service('taskasignService', ['dataConstants', 'apiHttpService', taskasignService]);

    function taskasignService(dataConstants, apiHttpService) {
        var service = {
            GetAllCurrProductList: GetAllCurrentProductList,
            reviewerDetailsInfoList: reviewerDetailsInfoList,
            GetProductById: GetProductById,
            updateStudent: updateStudent,
            deleteStudent: deleteStudent
        };

        return service;
        function GetAllCurrentProductList() {
            var url = dataConstants.PRODUCT_URL + 'GetAllCurrentProductList';
            return apiHttpService.GET(url);
        }

        function reviewerDetailsInfoList() {
            var url = dataConstants.REVIEWERTASKASIGN_URL + 'reviewerDetailsInfoList';
            return apiHttpService.GET(url);
        }

        function GetProductById(ProductId) {
            var url = dataConstants.PRODUCT_URL + 'GetProductById/'+ProductId;
            return apiHttpService.GET(url);
        }

        function updateStudent(studentId, data) {
            var url = dataConstants.STUDENT_URL + 'update-student/' + studentId;
            return apiHttpService.PUT(url, data);
        }

        function deleteStudent(studentId) {
            var url = dataConstants.STUDENT_URL + 'delete-student/' + studentId;
            return apiHttpService.DELETE(url);
        }


    }
})();