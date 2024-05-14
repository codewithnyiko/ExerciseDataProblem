using EnverSoft.DataProblem.Shared.Models;
using EnverSoft.DataProblem.Shared.Services;
using EnverSoft.DataProblem.Shared.Utilities;
using FluentAssertions;
using Moq;

namespace EnverSoft.DataProblem.Tests
{
    public class DataServiceTests
    {
        
        [Fact]
        public void ReadPeopleFromCsv_FileExists_ReturnsListOfPeople()
        {
            // Arrange
            var fileSystemMock = new Mock<IFileSystem>();
            var dataService = new DataService(fileSystemMock.Object);

            string csvData = @"
            FirstName,LastName,Address,PhoneNumber
            John,Doe,123 Main St,555-1234
            Jane,Smith,456 Elm St,555-5678
            Michael,Johnson,789 Oak St,555-9012
            Emily,Williams,101 Pine St,555-3456
            Christopher,Brown,222 Maple St,555-7890
            ";

            // Simulate File.ReadAllLines() by splitting the string into lines
            string[] lines = csvData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            fileSystemMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
            fileSystemMock.Setup(fs => fs.ReadAllLines(It.IsAny<string>())).Returns(lines);

            // Act
            var result = dataService.ReadPeopleFromCsv(It.IsAny<string>());

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCountGreaterThan(0);
            result.Should().AllBeOfType<Person>();
            fileSystemMock.Verify(f=>f.FileExists(It.IsAny<string>()),Times.Once);
            fileSystemMock.Verify(f => f.ReadAllLines(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ReadPeopleFromCsv_FileDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            var fileSystemMock = new Mock<IFileSystem>();
            var dataService = new DataService(fileSystemMock.Object);

            var filePath = "nonexistent.csv";

            fileSystemMock.Setup(fs => fs.FileExists(filePath)).Returns(false);

            // Act
            var people = dataService.ReadPeopleFromCsv(filePath);

            // Assert
            people.Should().NotBeNull();
            people.Should().BeEmpty();
            fileSystemMock.Verify(f => f.FileExists(It.IsAny<string>()), Times.Once);
            fileSystemMock.Verify(f => f.ReadAllLines(It.IsAny<string>()), Times.Never);
        }
    }
}
