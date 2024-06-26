<img alt="ServiceCollector" src="https://github.com/Ershad95/ServiceCollector/blob/master/ServiceCollector.Core/icon.png" style='Width:50px'/>

## ServiceCollector
<img alt="Static Badge" src="https://img.shields.io/badge/nuget-ServiceCollector.Core-blue?style=flat&logo=nuget&color=blue&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FServiceCollector.Core%2F"> <img alt="Static Badge" src="https://img.shields.io/badge/nuget-ServiceCollector.Validation-blue?style=flat&logo=nuget&color=blue&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FServiceCollector.Validation"> <img alt="Static Badge" src="https://img.shields.io/badge/nuget-ServiceCollector.Abstractions-blue?style=flat&logo=nuget&color=blue&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FServiceCollector.Abstractions%2F"> <img alt="Static Badge" src="https://img.shields.io/badge/nuget-ServiceCollector.Mock%20-blue?style=flat&logo=nuget&color=blue&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FServiceCollector.Mock%2F"> <img alt="Static Badge" src="https://img.shields.io/badge/nuget-ServiceCollector.Fake%20-blue?style=flat&logo=nuget&color=blue&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FServiceCollector.Fake%2F"> <img alt="Static Badge" src="https://img.shields.io/badge/nuget-ServiceCollector.Fake.Configuration%20-blue?style=flat&logo=nuget&color=blue&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FServiceCollector.Fake.Configuration%2F">


ServiceCollector is a .NET library that provides utilities for service discovery and validation within your .NET applications.

[![build](https://github.com/Ershad95/ServiceCollector/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Ershad95/ServiceCollector/actions/workflows/dotnet.yml) [![test](https://github.com/Ershad95/ServiceCollector/actions/workflows/test.yml/badge.svg)](https://github.com/Ershad95/ServiceCollector/actions/workflows/test.yml) <img src="https://img.shields.io/badge/.net6-compatible-green?style=flat&label=.net6&color=darkGreen" /> <img src="https://img.shields.io/badge/.net6-compatible-green?style=flat&label=.netStandard&color=darkGreen" />

<img  alt="NuGet Downloads" src="https://img.shields.io/nuget/dt/ServiceCollector.Core" /> <img alt="NuGet Version" src="https://img.shields.io/nuget/v/ServiceCollector.Core" /> <img alt="GitHub commit activity" src="https://img.shields.io/github/commit-activity/m/ershad95/ServiceCollector" /> <img alt="Libraries.io dependency status for GitHub repo" src="https://img.shields.io/librariesio/github/ershad95/ServiceCollector" /> <img alt="GitHub repo size" src="https://img.shields.io/github/repo-size/ershad95/ServiceCollector" />


## Installation
You can install the ServiceCollector package via NuGet Package Manager Console or through the NuGet Package Manager UI in Visual Studio.

## NuGet Package Manager Console
<pre>Install-Package ServiceCollector.Core</pre>
## NuGet Package Manager UI
Search for "ServiceCollector.Core" in the NuGet Package Manager UI, then click "Install."

## Usage

## 1. Add Services
create one class that implemented IServiceDiscovery(class name is not important) :

<pre>
   public class ServiceManager : IServiceDiscovery
    {
        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<TransactionService>();
            serviceCollection.AddTransient<TransactionService>();
            serviceCollection.AddSingleton<TransactionService>();
              
            // add other services
        }
    }
</pre>
you can create many classes that implemented IServiceDiscovery all of them detected and apply services in program.

## 2. Service Discovery
Service discovery allows you to automatically discover and add services to the service collection from specified assemblies.
<pre>
using ServiceCollector.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceDiscovery();

var app = builder.Build();
app.Run();
</pre>

## 3. Service Validation (optional)
Service validation allows you to validate services based on certain conditions, such as naming conventions.

install package :
<pre>Install-Package ServiceCollector.Validation</pre>
<br>
this validator check that all of the services register in DI,if one service meet the condition but not register validator detect it and throw exception

<pre>
using ServiceCollector.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceDiscovery()
             .ValidationSetting()
             .WithStartName("Prefix")
             .WithEndName("Suffix")
             .Validate();

var app = builder.Build();
app.Run();
</pre>

## 4. Mock Services (Test)
if you want write any test(UnitTest,IntegrationTest) you need some test-doubles
<br>
install package :
<pre>Install-Package ServiceCollector.Mock</pre>
<br>
you can easily mock all services and use it in your tests:
<pre>
  public void X_X_X()
  {
    // Arrange
  
    // you can set any classes that implemented IServiceDiscovery
    var serviceType = typeof(ServiceManager); 

    // create a mock for each service that defined in ServiceManager
    var mockResult = Mock.MockCollection(serviceType);
  
    // Act
  
    // Assert
  }
</pre>

## 5. Fake Services (Production code)
Debugging without relying on external services and facilitating rapid service delivery to front-end developers without the need for implementation.
<br>

install packages :
<pre>Install-Package ServiceCollector.Fake</pre>

<br>

## Nuget Packages : 
<a href="https://www.nuget.org/packages/ServiceCollector.Abstractions/">ServiceCollector.Abstractions</a>
<br>
<a href="https://www.nuget.org/packages/ServiceCollector.Core/">ServiceCollector.Core</a>
<br>
<a href="https://www.nuget.org/packages/ServiceCollector.Validation/">ServiceCollector.Validation</a>
<br>
<a href="https://www.nuget.org/packages/ServiceCollector.Mock/">ServiceCollector.Mock</a>
<br>
<a href="https://www.nuget.org/packages/ServiceCollector.Fake/">ServiceCollector.Fake</a>
<br>
<a href="https://www.nuget.org/packages/ServiceCollector.Fake.Configuration/">ServiceCollector.Fake.Configuration</a>
## License
This project is licensed under the MIT License
