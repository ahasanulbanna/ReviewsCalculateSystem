(function () {
    'use strict';
    angular.module('app').service('reviewerService', ['dataConstants', 'apiHttpService', reviewerService]);

    function reviewerService(dataConstants, apiHttpService) {
        var service = {
            CreateReviewer: CreateReviewer,
            reviewerReviewAndProductInfoById: reviewerReviewAndProductInfoById,
            saveCourse: saveCourse,
            updateCourse: updateCourse,
            deleteCourse: deleteCourse,
            reviewerLogin: reviewerLogin
        };

        return service;

        function reviewerLogin(username, password) {
            var url = dataConstants.LOGIN;
            return apiHttpService.LOGIN(url, username, password);
        }

        function CreateReviewer(data) {
            var url = dataConstants.REVIEWER + 'CreateReviewer';
            return apiHttpService.POST(url,data);
        }

        function reviewerReviewAndProductInfoById(reviewerId, productId) {
            var url = dataConstants.REVIEWERTASKASIGN_URL + 'reviewerReviewForEachProductById?reviewerId=' + reviewerId + '&productId=' + productId;
            return apiHttpService.GET(url);
        }

        function saveCourse(data) {
            var url = dataConstants.COURSE_URL + 'save-course';
            return apiHttpService.POST(url, data);
        }

        function updateCourse(courseId, data) {
            var url = dataConstants.COURSE_URL + 'update-course/' + courseId;
            return apiHttpService.PUT(url, data);
        }

        function deleteCourse(courseId) {
            var url = dataConstants.COURSE_URL + 'delete-course/' + courseId;
            return apiHttpService.DELETE(url);
        }


    }
})();