/// <reference path="../Scripts/angular.min.js" />
angular.module("adminApp", [])
   .controller("AdminController", ['$scope', '$http', function ($scope, $http) {
           $scope.LogOnCommand = function () {
               $http({
                   method: 'POST',
                   url: '/Admin/LogOn'
               }).success(function (data, status) {
                   console.log('All ok : ' + data);
                   $scope.statusValue = "Log On Success!"
                   //$scope.ClearControl();
                   //$scope.loading = false;

               }).error(function (data, status) {
                   console.log('Oops : ' + data);
                   $scope.statusValue = data;
               });

           };

           $scope.LogOffCommand = function () {
               $http({
                   method: 'POST',
                   url: '/Admin/LogOff'
               }).success(function (data, status) {
                   console.log('All ok : ' + data);
                   $scope.statusValue = "Log Off Success!"
                   //$scope.ClearControl();
                   //$scope.loading = false;

               }).error(function (data, status) {
                   console.log('Oops : ' + data);
                   $scope.statusValue = data;
               });
           };


           $scope.statusValue = "";
          
       
       
       }]);