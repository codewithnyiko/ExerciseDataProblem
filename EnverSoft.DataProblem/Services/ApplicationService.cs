using EnverSoft.DataProblem.Shared.Services;
using EnverSoft.DataProblem.Shared.Utilities;

namespace EnverSoft.DataProblem.Services
{
    public class ApplicationService : IApplicatonService
    {
        private readonly IDataService _dataService;
        private readonly IFileSystem _fileSystem;
        private readonly IReportService _reportService;
        public ApplicationService(IDataService dataService, IFileSystem fileSystem, IReportService reportService)
        {
            _dataService = dataService;
            _fileSystem = fileSystem;
            _reportService = reportService;
        }

        /// <inheritdoc />
        public void ProcessData()
        {
            try
            {
                string fileName = "data.csv";
                string projectDirectory = _fileSystem!.GetProjectRootDirectory();

                // Combine the project directory with the file name to get the full file path
                string filePath = _fileSystem.CombinePath(projectDirectory, fileName);

                // Read data from CSV file
                var people = _dataService.ReadPeopleFromCsv(filePath).ToList();

                // Process data if valid data found
                if (people.Count != 0)
                {
                    _reportService.SaveNameFrequencyReport(people, projectDirectory);

                    _reportService.SaveAddressFrequencyReport(people, projectDirectory);
                }
                else
                {
                    Console.WriteLine("No valid data found, the file might not have data or it's not supported in the CSV file.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while processing the data: " + e.Message);
            }
        }
    }
}
