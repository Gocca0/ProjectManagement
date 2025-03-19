using ProjectManagement.ConfigureApp;



var builder = WebApplication.CreateBuilder(args);
ServiceConfigure.Configure(builder.Services, builder.Configuration);


var app = builder.Build();
AppConfigure.Configure(app, app.Environment);


app.Run();
