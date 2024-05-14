using EnverSoft.DataProblem.Services;
using EnverSoft.DataProblem.Shared.Services;
using EnverSoft.DataProblem.Shared.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddTransient<IDataService, DataService>();
        services.AddTransient<IFileSystem, FileSystem>();
        services.AddTransient<IReportService, ReportService>();
        services.AddSingleton<IApplicatonService, ApplicationService>();
    }).Build();

var app = host.Services.GetRequiredService<IApplicatonService>();
app.ProcessData();