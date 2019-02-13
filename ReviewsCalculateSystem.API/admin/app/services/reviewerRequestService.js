(function () {
    'use strict';
    angular.module('app').service('reviewerRequestService', ['dataConstants', 'apiHttpService', reviewerRequestService]);

    function reviewerRequestService(dataConstants, apiHttpService) {
        var service = {
            GetAllReviewerRequest: GetAllReviewerRequest,
            getStudent: getStudent,
            saveStudent: saveStudent,
            updateStudent: updateStudent,
            deleteStudent: deleteStudent
        };

        return service;
        function GetAllReviewerRequest() {
            var url = dataConstants.REVIEWER_URL + 'GetAllReviewerRequest';
            return apiHttpService.GET(url);
        }

        function getStudent(studentId) {
            var url = dataConstants.STUDENT_URL + 'get-student?studentId=' + studentId;
            return apiHttpService.GET(url);
        }

        function saveStudent(data) {
            var url = dataConstants.STUDENT_URL + 'save-student';
            return apiHttpService.POST(url, data);
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