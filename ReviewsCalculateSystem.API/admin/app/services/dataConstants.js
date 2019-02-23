
(function () {
  "use strict";

     angular.module("app").constant("dataConstants", {
         DEPARTMENT_URL: baseAPIUrl + 'api/departments/',
         COURSE_URL: baseAPIUrl + 'api/courses/',
         STUDENT_URL: baseAPIUrl + 'api/students/',
         INVOICE_URL: baseAPIUrl + 'api/invoices/',

         REVIEWER_URL: baseAPIUrl + 'api/reviewer/',
         PRODUCT_URL: baseAPIUrl + 'api/products/',
         REVIEWERTASKASIGN_URL: baseAPIUrl + 'api/ReviewerTaskAsign/',
         REVIEW_URL: baseAPIUrl +'api/reviews/'


  });
})();