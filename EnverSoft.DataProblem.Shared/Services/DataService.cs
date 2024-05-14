using EnverSoft.DataProblem.Shared.Models;
using EnverSoft.DataProblem.Shared.Utilities;

namespace EnverSoft.DataProblem.Shared.Services
{
    public class DataService : IDataService
    {
        private readonly IFileSystem _fileSystem;

        public DataService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        /// <inheritdoc />
        public IEnumerable<Person> ReadPeopleFromCsv(string filePath)
        {
            if (_fileSystem.FileExists(filePath))
            {
                var lines = _fileSystem.ReadAllLines(filePath);
                return lines
                    .Skip(1)
                    .Select(line => line.Split(','))
                    .Where(row => row.Length >= 4 && !row.Any(string.IsNullOrWhiteSpace))
                    .Select(dataRow => new Person
                    {
                        FirstName = dataRow[0],
                        LastName = dataRow[1],
                        Address = dataRow[2],
                        PhoneNumber = dataRow[3]
                    }).ToList();
            }

            return Enumerable.Empty<Person>();
        }
    }
}