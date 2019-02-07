(function () {
    'use strict';
    angular.module('app').service('reviewerasigntaskService', ['dataConstants', 'apiHttpService', reviewerasigntaskService]);

    function reviewerasigntaskService(dataConstants, apiHttpService) {
        var service = {
            getCurrentAsingTaskById: getCurrentAsingTaskById,
            reviewerReviewAndProductInfoById: reviewerReviewAndProductInfoById,
            saveCourse: saveCourse,
            updateCourse: updateCourse,
            deleteCourse: deleteCourse
        };

        return service;
        function getCurrentAsingTaskById(Id) {
            var url = dataConstants.REVIEWERTASKASIGN_URL + 'getCurrentAsingTaskById/' + Id;
            return apiHttpService.GET(url);
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