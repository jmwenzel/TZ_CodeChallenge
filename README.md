# SearchFight
Console application to determine the popularity of programming languages on the internet.
Run the console application specifying the search terms you want to look for. It supports quotation marks to allow searching for terms with spaces (e.g. searchfight.exe “java script”).
It's built in NET Core, in order to create an exe file first publish the console application into a folder.

```sh
C:\SearchFight.exe .net java
.net: Google: 4450000000 MSN Search: 12354420 
java: Google: 966000000 MSN Search: 94381485 
Google winner: .net 
MSN Search winner: java 
Total winner: .net 
```

# API Keys
Replace values in the App config file:

    <!-- Google Settings -->    
    <add key="Google.ApiKey" value="ADD_YOUR_GOOGLE_API_KEY_HERE" />
    <add key="Google.ContextId" value="ADD_YOUR_GOOGLE_API_CONTEXT_ID_HERE" />

    <!-- Bing Settings -->    
    <add key="Bing.ApiKey" value="ADD_YOUR_BING_API_KEY_HERE" />
