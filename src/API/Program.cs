using API.Configuration.Startup;

[assembly: ApiController]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
WebApplication.CreateBuilder(args)
              .ConfigureService()
              .Configure();

