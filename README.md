1. Try to use MyOwnCertificate.pfx (Password: 1111), if it doesn't work, please, create your certificate and put imprint into ConsoleHostApp/App.config
2. Build the solution
3. Publish DatabaseForTest with name 'DbTest'
4. Run ConsoleHostApp and update all the references in Connected Services in the WebUI and WpfApp (if MyOwnCertificate.pfx didn't work)
5. Change data source in connection string in WebUI\Web.Config, ConsoleHostApp\App.config, WebAPICore\App.config, WcfServiceLibrary\App.config on your data source
6. Run ConsoleHostApp, WebAPICore and WebUI or WebApp

If you don't have .NET Core 2.1, please install it. (https://www.microsoft.com/net/download/dotnet-core/2.1)
Username and password for Event manager - manager, for User - user, for Venue manager - anna