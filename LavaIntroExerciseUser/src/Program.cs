using System.Threading.Tasks;
using Grpc.Net.Client;
using LavaIntroExerciseUser;
using LavaIntroExerciseUser.src.UserBusinessLogic;
using LavaIntroExerciseUser.src.UserDirectAccessLayer;
using Microsoft.Extensions.Logging;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:5078");
var client = new ProxyServerService.ProxyServerServiceClient(channel);

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddFilter("Microsoft", LogLevel.Warning)
        .AddFilter("System", LogLevel.Warning)
        .AddFilter("NonHostConsoleApp.Program", LogLevel.Debug);
});
var blLogger = loggerFactory.CreateLogger<UserBL>();
var dalLogger = loggerFactory.CreateLogger<UserDal>();

var userDal = new UserDal(dalLogger, client);
var userBl = new UserBL(blLogger, userDal);

while (true)
{
    await userBl.StateTrackerIteration();
}
