namespace EnverSoft.DataProblem.Shared.Utilities;

/// <summary>
/// Represents a file system utility for performing file-related operations.
/// </summary>
public interface IFileSystem
{
    /// <summary>
    /// Gets the root directory of the project.
    /// </summary>
    /// <returns>The root directory of the project.</returns>
    string GetProjectRootDirectory();

    /// <summary>
    /// Combines multiple strings into a file path.
    /// </summary>
    /// <param name="paths">An array of strings representing parts of the file path.</param>
    /// <returns>The combined file path.</returns>
    string CombinePath(params string[] paths);

    /// <summary>
    /// Checks whether a file exists at the specified file path.
    /// </summary>
    /// <param name="filePath">The path of the file to check.</param>
    /// <returns>True if the file exists; otherwise, false.</returns>
    bool FileExists(string filePath);

    /// <summary>
    /// Reads all lines from a file and returns them as an enumerable of strings.
    /// </summary>
    /// <param name="filePath">The path of the file to read.</param>
    /// <returns>An enumerable of strings representing the lines of the file.</returns>
    IEnumerable<string> ReadAllLines(string filePath);

    /// <summary>
    /// Writes a sequence of strings to a file
    /// </summary>
    /// <param name="outputPath">The path of the file to write to.</param>
    /// <param name="reportData">The sequence of strings to write to the file.</param>
    void WriteAllLines(string outputPath, IEnumerable<string> reportData);
}