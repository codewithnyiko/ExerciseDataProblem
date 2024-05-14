using EnverSoft.DataProblem.Shared.Enums;
using EnverSoft.DataProblem.Shared.Models;
using EnverSoft.DataProblem.Shared.Utilities;

namespace EnverSoft.DataProblem.Shared.Services
{
    public class ReportService : IReportService
    {
        private readonly IFileSystem _fileSystem;

        public ReportService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        /// <inheritdoc />
        public void SaveNameFrequencyReport(IEnumerable<Person> people, string projectDirectory)
        {
            if (people == null)
            {
                throw new ArgumentNullException(nameof(people));
            }

            string reportName = GetReportName(ReportType.NameFrequency);
            string reportPath = GetReportPath(projectDirectory, reportName);

            var reportData = people.SelectMany(p => new[] { p.FirstName, p.LastName })
                .GroupBy(name => name)
                .OrderByDescending(group => group.Count())
                .ThenBy(group => group.Key)
                .Select(group => $"{group.Key}, {group.Count()}");

            SaveReport(reportData, reportPath);
        }

        /// <inheritdoc />
        public void SaveAddressFrequencyReport(IEnumerable<Person> people, string projectDirectory)
        {
            if (people == null)
            {
                throw new ArgumentNullException(nameof(people));
            }

            string reportName = GetReportName(ReportType.AddressFrequency);
            string reportPath = GetReportPath(projectDirectory, reportName);

            var reportData = people
                .Select(person =>
                {
                    var addressParts = person.Address.Split(' ');
                    return new
                    {
                        StreetNumber = int.Parse(addressParts[0]),
                        StreetName = string.Join(" ", addressParts.Skip(1))
                    };
                })
                .OrderBy(address => address.StreetName)
                .Select(address => $"{address.StreetNumber} {address.StreetName}");

            SaveReport(reportData, reportPath);
        }

        private string GetReportPath(string projectDirectory, string reportName)
        {
            return _fileSystem.CombinePath(projectDirectory, "Reports", reportName);
        }

        private string GetReportName(ReportType reportType)
        {
            string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            return $"{reportType}_Report_{dateTime}.txt";
        }

        private void SaveReport(IEnumerable<string> reportData, string outputPath)
        {
            try
            {
                _fileSystem.WriteAllLines(outputPath, reportData);
                Console.WriteLine($"The report has been saved successfully at: {outputPath}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: The report could not be saved. File '{outputPath}'");
            }
        }
    }
}
