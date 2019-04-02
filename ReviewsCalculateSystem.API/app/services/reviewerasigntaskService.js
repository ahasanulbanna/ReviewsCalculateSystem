(function () {
    'use strict';
    angular.module('app').service('reviewerasigntaskService', ['dataConstants', 'apiHttpService', reviewerasigntaskService]);

    function reviewerasigntaskService(dataConstants, apiHttpService) {
        var service = {
            getCurrentAsingTaskById: getCurrentAsingTaskById,
            reviewerReviewAndProductInfoById: reviewerReviewAndProductInfoById,
            SubmitProductReview: SubmitProductReview,
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

        function SubmitProductReview(data) {
            var url = dataConstants.REVIEW + 'SubmitProductReview';
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