using EnverSoft.DataProblem.Services;
using EnverSoft.DataProblem.Shared.Models;
using EnverSoft.DataProblem.Shared.Services;
using EnverSoft.DataProblem.Shared.Utilities;
using Moq;

namespace EnverSoft.DataProblem.Tests
{
    public class ApplicationServiceTests
    {
        [Fact]
        public void Run_ValidData_ProducesReports()
        {
            // Arrange
            var mockDataService = new Mock<IDataService>();
            mockDataService.Setup(ds => ds.ReadPeopleFromCsv(It.IsAny<string>())).Returns(new List<Person>
            {
                new Person { FirstName = "John", LastName = "Doe", Address = "123 Main St", PhoneNumber = "555-1234" },
                new Person { FirstName = "Jane", LastName = "Smith", Address = "456 Elm St", PhoneNumber = "555-5678" }
            });

            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem.Setup(fs => fs.GetProjectRootDirectory()).Returns("C:\\ProjectDirectory");
            mockFileSystem.Setup(fs => fs.CombinePath(It.IsAny<string>(), It.IsAny<string>())).Returns("C:\\ProjectDirectory\\data.csv");

            var mockReportService = new Mock<IReportService>();

            var applicationService = new ApplicationService(mockDataService.Object, mockFileSystem.Object, mockReportService.Object);

            // Act
            applicationService.ProcessData();

            // Assert
            mockReportService.Verify(rs => rs.SaveNameFrequencyReport(It.IsAny<IEnumerable<Person>>(), It.IsAny<string>()), Times.Once);
            mockReportService.Verify(rs => rs.SaveAddressFrequencyReport(It.IsAny<IEnumerable<Person>>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Run_EmptyData_PrintsMessage()
        {
            // Arrange
            var mockDataService = new Mock<IDataService>();
            mockDataService.Setup(ds => ds.ReadPeopleFromCsv(It.IsAny<string>())).Returns(new List<Person>());

            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem.Setup(fs => fs.GetProjectRootDirectory()).Returns("C:\\ProjectDirectory");
            mockFileSystem.Setup(fs => fs.CombinePath(It.IsAny<string>(), It.IsAny<string>())).Returns("C:\\ProjectDirectory\\data.csv");

            var mockReportService = new Mock<IReportService>();

            var applicationService = new ApplicationService(mockDataService.Object, mockFileSystem.Object, mockReportService.Object);

            // Act
            applicationService.ProcessData();

            // Assert
            mockReportService.Verify(rs => rs.SaveNameFrequencyReport(It.IsAny<IEnumerable<Person>>(), It.IsAny<string>()), Times.Never);
            mockReportService.Verify(rs => rs.SaveAddressFrequencyReport(It.IsAny<IEnumerable<Person>>(), It.IsAny<string>()), Times.Never);
        }
    }
}
