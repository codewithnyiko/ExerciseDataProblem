using EnverSoft.DataProblem.Shared.Models;

namespace EnverSoft.DataProblem.Shared.Services
{
    /// <summary>
    /// Represents an interface for a data service responsible for reading data from a CSV file.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Reads data from a CSV file located at the specified file path.
        /// </summary>
        /// <param name="filePath">The file path of the CSV file.</param>
        /// <returns>An enumerable collection of Person objects.</returns>
        IEnumerable<Person> ReadPeopleFromCsv(string filePath);
    }

}
