using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;
using XafMcp.Module.BusinessObjects;







var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

XpoTypesInfoHelper.GetXpoTypeInfoSource();
XafTypesInfo.Instance.RegisterEntity(typeof(Customer));
XPObjectSpaceProvider osProvider = new XPObjectSpaceProvider(
@"integrated security=SSPI;pooling=false;data source=(localdb)\MSSQLLocalDB;initial catalog=MainDemo_", null);
IObjectSpace objectSpace = osProvider.CreateObjectSpace();



builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();





