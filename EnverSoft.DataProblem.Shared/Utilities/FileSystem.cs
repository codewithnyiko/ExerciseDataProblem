namespace EnverSoft.DataProblem.Shared.Utilities
{
    public class FileSystem : IFileSystem
    {

        /// <inheritdoc />
        public string GetProjectRootDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            while (!Directory.GetFiles(currentDirectory).Any(file => file.EndsWith(".csproj")))
            {
                currentDirectory = Directory.GetParent(currentDirectory)!.FullName;
            }

            return currentDirectory;
        }

        /// <inheritdoc />
        public string CombinePath(params string[] paths)
        {
            return Path.Combine(paths);
        }

        /// <inheritdoc />
        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <inheritdoc />
        public IEnumerable<string> ReadAllLines(string filePath)
        {

            return File.ReadAllLines(filePath);
        }

        /// <inheritdoc />
        public void WriteAllLines(string outputPath, IEnumerable<string> reportData)
        {
            File.WriteAllLines(outputPath, reportData);
        }
    }
}