using EnverSoft.DataProblem.Shared.Models;
using EnverSoft.DataProblem.Shared.Services;
using EnverSoft.DataProblem.Shared.Utilities;
using Moq;

namespace EnverSoft.DataProblem.Tests
{
    public class ReportServiceTests
    {
        [Fact]
        public void SaveNameFrequencyReport_NullPeople_ThrowsArgumentNullException()
        {
            // Arrange
            var fileSystemMock = new Mock<IFileSystem>();
            var reportService = new ReportService(fileSystemMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => reportService.SaveNameFrequencyReport(null, It.IsAny<string>()));
        }

        [Fact]
        public void SaveNameFrequencyReport_ValidInput_SavesReport()
        {
            // Arrange
            var fileSystemMock = new Mock<IFileSystem>();
            var reportService = new ReportService(fileSystemMock.Object);
            var people = new List<Person>
            {
                new Person { FirstName = "John", LastName = "Doe", Address = "123 Main St" },
                new Person { FirstName = "Jane", LastName = "Smith", Address = "456 Elm St" }
            };

            // Act
            reportService.SaveNameFrequencyReport(people, "projectDirectory");

            // Assert
            fileSystemMock.Verify(fs => fs.CombinePath(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            fileSystemMock.Verify(fs => fs.WriteAllLines(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()), Times.Once);
        }

        [Fact]
        public void SaveAddressFrequencyReport_NullPeople_ThrowsArgumentNullException()
        {
            // Arrange
            var fileSystemMock = new Mock<IFileSystem>();
            var reportService = new ReportService(fileSystemMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => reportService.SaveAddressFrequencyReport(null, It.IsAny<string>()));
        }

        [Fact]
        public void SaveAddressFrequencyReport_ValidInput_SavesReport()
        {
            // Arrange
            var fileSystemMock = new Mock<IFileSystem>();
            var reportService = new ReportService(fileSystemMock.Object);
            var people = new List<Person>
            {
                new Person { FirstName = "John", LastName = "Doe", Address = "123 Main St" },
                new Person { FirstName = "Jane", LastName = "Smith", Address = "456 Elm St" }
            };

            // Act
            reportService.SaveAddressFrequencyReport(people, "projectDirectory");

            // Assert
            fileSystemMock.Verify(fs => fs.CombinePath(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            fileSystemMock.Verify(fs => fs.WriteAllLines(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()), Times.Once);
        }
    }
}
