## ServiceCollector
ServiceCollector is a .NET library that provides utilities for service discovery and validation within your .NET applications.

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
<br/>
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

## 3. Mock Services (Test)
if you want write any test(UnitTest,IntegrationTest) you need some test-doubles
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


## Nuget Packages : 
<a href="https://www.nuget.org/packages/ServiceCollector.Abstractions/">ServiceCollector.Abstractions</a>
<br>
<a href="https://www.nuget.org/packages/ServiceCollector.Core/">ServiceCollector.Core</a>
<br>
<a href="https://www.nuget.org/packages/ServiceCollector.Validation/">ServiceCollector.Validation</a>
<br>
<a href="https://www.nuget.org/packages/ServiceCollector.Mock/">ServiceCollector.Mock</a>

## License
This project is licensed under the MIT License
