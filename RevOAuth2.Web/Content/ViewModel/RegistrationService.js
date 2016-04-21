/// <reference path="../Scripts/angular.js" />

var registrationApp = angular.module("registrationAppService" , []);
registrationApp.service('signalRSvc', function ($, $rootScope) {
   
    return {
        initialize: initialize       
    };
});



