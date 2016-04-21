


angular.module("revAuthApp", ['ngCookies'])
    .controller("RevOAuthController", ['$scope', '$http', '$cookieStore', function ($scope, $http, $cookieStore) {

    	$scope.loading = false;
    	$scope.connectionId = '';
    	$scope.revinfo = {};   	
    	//$scope.revinfo.Apikey = "12345";
    	//$scope.revinfo.Apisecret = "aBeXJ5cnHAycNZmDpzJb2MdiOxRDti4v8/UsCjGmenEeKe1jluHg3Zyq/vbwTzrsW3c4a+u8Q8WlgG7lNldBhbJwuMfrxlQLzuRm/ICyfO+g3L2JVTHa92B8kDSHBA/D1iXZcfLLVf4JKW5zloRqlfguVOob2Z4pKZEq9Uhwcpg=";
    	$scope.revinfo.Apikey = "";
    	$scope.revinfo.Apisecret = "";

    	$scope.IsCodeButtonHide = false;
    	$scope.revToken = {};
    	$scope.hosturl = '';

    	//Starting hub connection
    	$scope.startHub = function () {
    		var hub = $.hubConnection();

    		var proxy = hub.createHubProxy('pushHub');


    		proxy.on('revOAuthError', function (message) {
    			console.log(message);
    			$scope.ShowErrorMessage(message)
    			$scope.$apply();
    		});

    		proxy.on('revOAuthConfirmation', function (message) {
    			console.log(message);
    			$scope.showConfirmationMessage(message);
    			$scope.$apply();
    		});


    		hub.start().done(function () {
    			console.log("ConnectionID:" + hub.id);
    			$scope.connectionId = hub.id;
    			$scope.setConnectionID(hub.id);
    			$scope.getHostName();    			
    		});

    	};

    	$scope.startHub();

    	//to show error message from server
    	$scope.getQueryVariable = function (variable) {
    		var query = window.location.search.substring(1);
    		var vars = query.split("&");
    		for (var i = 0; i < vars.length; i++) {
    			var pair = vars[i].split("auth_code=");
    			if (pair[1]) {
    				$scope.code = pair[1];
    				$scope.IsCodeButtonHide = true;
    				$scope.revinfo = $cookieStore.get('revInfo');
    			}
    		}
    	};

    	$scope.revOAuthToken = function () {
    		$scope.loading = true;
    		$scope.HideConfirmationMessage();
    		$scope.HideErrorMessage();
    		$http({
    			method: 'POST',
    			url: '/RevOAuth/RevToken',
    			data: {
    				"revurl": $scope.revinfo.Revurl,
    				"apikey": $scope.revinfo.Apikey,
    				"code": $scope.code,
    				"connectionId": $scope.connectionId,
    				"RedirectUri": $scope.revinfo.RedirectUri
    			}
    		}).success(function (data, status) {
    			$scope.showConfirmationMessage('');
    			$scope.revToken.accessToken = data.accessToken;
    			$scope.revToken.refreshToken = data.refreshToken;
    			$scope.revToken.expiration = data.expiration;
    			$scope.revToken.issuedBy = data.issuedBy;

    			console.log('All ok : ' + data);

    		})
					.error(function (data, status) {
						console.log('Oops : ' + data);
					});
    		$scope.loading = false;
    	};


    	//to show error message from server
    	$scope.ShowErrorMessage = function (message) {
    		$scope.revOAuthFailure = true;
    		$scope.ErrorMessage = message;
    	};

    	$scope.HideErrorMessage = function () {
    		$scope.revOAuthFailure = false;
    	};

    	$scope.HideConfirmationMessage = function () {
    		$scope.revOAuthComplete = false;
    	};


    	$scope.getHostName = function () {
    		$http({
    			method: 'GET',
    			url: '/RevOAuth/HostUrl'

    		}).success(function (data, status) {
    			$scope.hosturl = data;
    			$scope.revinfo.Revurl = data;
    			$scope.revinfo.RedirectUri = data + "/oauth";
    			$scope.getQueryVariable("auth_code");
    			console.log('All ok : ' + data);

    		}).error(function (data, status) {
    			console.log('Oops : ' + data);
    		});    		
    	};

    	$scope.refreshPage = function () {
    		$scope.HideConfirmationMessage();
    		$scope.HideErrorMessage();
    		$cookieStore.put('revInfo', null);
    		//$scope.IsCodeButtonHide = false;
    		//$scope.revToken = {};
    		//$http({
    		//	method: 'GET',
    		//	url: '/RevOAuth/RefreshPage'

    		//}).success(function (data, status) {
    		//	window.location.href = $scope.hosturl;
    		//	console.log('All ok : ' + data);

    		//}).error(function (data, status) {
    		//	console.log('Oops : ' + data);
    		//});
    		window.location.href = $scope.hosturl;
    	};

    	//to show confirmation messsage from server
    	$scope.showConfirmationMessage = function (message) {
    		$scope.revOAuthComplete = true;
    		$scope.ConfirmationMessage = message;
    	};

    	$scope.setConnectionID = function (connection) {
    		$http({
    			method: 'POST',
    			url: '/RevOAuth/ConnectionID',
    			data:
                    {
                    	"connection": connection
                    }
    		}).success(function (data, status) {
    			console.log('All ok : ' + data);

    		}).error(function (data, status) {
    			console.log('Oops : ' + data);
    		});
    	};

    	$scope.isNullOrEmptyOrUndefined = function (value) {
    		return !value;
    	}

    	//to register user
    	$scope.revOAuthCode = function () {
    		$scope.loading = true;
    		$scope.HideConfirmationMessage();
    		$scope.HideErrorMessage();

    		////check for validation
    		if ($scope.isNullOrEmptyOrUndefined($("#RevUrl").val()) || $scope.isNullOrEmptyOrUndefined($("#ApiKey").val()) || $scope.isNullOrEmptyOrUndefined($("#ApiSecret").val()) || $scope.isNullOrEmptyOrUndefined($("#RedirectUri").val())) {
    			$scope.ShowErrorMessage("Please fill the details.")
    			return;
    		}

    		$scope.revinfo.Revurl = $("#RevUrl").val();
    		$scope.revinfo.Apikey = $("#ApiKey").val();
    		$scope.revinfo.RedirectUri = $("#RedirectUri").val();
    		$scope.revinfo.Apisecret = $("#ApiSecret").val();

    		$cookieStore.put('revInfo', $scope.revinfo);



    		$http({
    			method: 'POST',
    			url: '/RevOAuth',
    			data: {
    				"revurl": $scope.revinfo.Revurl,
    				"apikey": $scope.revinfo.Apikey,
    				"apisecret": $scope.revinfo.Apisecret,
    				"connectionId": $scope.connectionId,
    				"RedirectUri": $scope.revinfo.RedirectUri

    			}
    		}).success(function (data, status) {
    			window.location.href = data;
    			//console.log('All ok : ' + data);

    		})
                .error(function (data, status) {
                	console.log('Oops : ' + data);
                });

    		$scope.loading = false;
    	}  	

    }]);