
using vehicle_backup.Entities;
using vehicle_backup.Processes;
using vehicle_backup.Services;

const string FILES_DIRECTORY = "csv";
const int API_MAX_RETRIES = 3;

var logger = new LoggingService();

if (args.Length < 4)
{
    logger.LogText("Configuration not provided.");
    logger.LogText("dotnet run <user> <password> <server> <database> [filepath]");
    return;
}

var user = args[0];
var password = args[1];
var server = args[2];
var database = args[3];
var filesDirectory = args.Length == 5 ? args[4] : FILES_DIRECTORY;

var configuration = new APIConfiguration(user, password, null, database, server, API_MAX_RETRIES);

logger.LogText("\n******************************************************");
logger.LogText($"Receiving data from server {server} and database {database}.");
logger.LogText($"Output files will be saved in directory: {filesDirectory}");
logger.LogText("******************************************************");
logger.LogText("Press any key to quit.\n");

var cancellationToken = new CancellationTokenSource();
var mainProcess = new MainProcess(filesDirectory, logger);

#pragma warning disable CS4014
mainProcess.RunAsync(configuration, cancellationToken);
#pragma warning restore CS4014

if (Console.ReadLine() != null)
{
    logger.LogText("Cancel requested. Waiting for current execution to end.");
    cancellationToken.Cancel();
}

logger.LogText("\n******************************************************");
logger.LogText("Finished receiving data.");
logger.LogText("******************************************************\n");
logger.LogText("Press ENTER to quit.");
Console.ReadLine();
