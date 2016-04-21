## Rev Authentication using oAuth 
An implementation of integrating with Rev OAuth

There are bascially two projects in the solution
1.  **RevOAuth2.Runtime** - This project works as a host where We have actual code that interacts with Rev System to get Tokens etc.
2.  **RevOAuth2.Web** - This is a User interface wherein a user can run and see it working 


# Steps to follow for running this code
1. Open the solution file in **Visual Studio 2013** 
2. Right click on RevOAuth2.Runtime project in solution explorer and select Properties 
3. Under Properties go to Debug Tab and set commnad line options as __/console__ . This will prompt the runtime to run as a console application.
4. Under app.config change the URl(with Port) as per your environment like this 
```<revOAuthConfigurationSettings utilityName="Rev OAuth2 Testing App" logLevel="Verbose" url="http://localhost:8081" logConfigurationFile="./log4netConfig.xml" />```
5. Set RevOAuth2.Runtime as yout startup project
6. Build and Run - It will run as a console application
7. Now You can access the user interface(Web application) using from your browser and request for oAuth code and Tokens



