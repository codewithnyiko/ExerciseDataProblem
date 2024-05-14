using EnverSoft.DataProblem.Shared.Models;

namespace EnverSoft.DataProblem.Shared.Services
{
    /// <summary>
    /// Represents an interface for a report service responsible for saving name and address frequency reports.
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Saves a name frequency report based on the provided data and project directory.
        /// </summary>
        /// <param name="people">The collection of Person objects.</param>
        /// <param name="projectDirectory">The project directory where the report will be saved.</param>
        void SaveNameFrequencyReport(IEnumerable<Person> people, string projectDirectory);

        /// <summary>
        /// Saves an address frequency report based on the provided data and project directory.
        /// </summary>
        /// <param name="people">The collection of Person objects.</param>
        /// <param name="projectDirectory">The project directory where the report will be saved.</param>
        void SaveAddressFrequencyReport(IEnumerable<Person> people, string projectDirectory);
    }

}