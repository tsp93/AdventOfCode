namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private List<string> SolveDay7(List<string> input)
        {
            return new List<string>
            {
                TotalSizeOfDirectoriesAboveCertainSize(input).ToString(),
                //GetStartMarker(input).ToString(),
            };
        }

        /// <summary>
        /// Calculates and sums the total size of directories according to the input given
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int TotalSizeOfDirectoriesAboveCertainSize(List<string> input)
        {
            Directory dirTree = ExecuteCommands(input);

            
            return 0;
        }

        /// <summary>
        /// Executes the commands listed in the input and creates a directory accordingly
        /// </summary>
        /// <param name="input"></param>
        /// <returns>A directory tree</returns>
        private Directory ExecuteCommands(List<string> input)
        {
            Directory baseDirectory = CreateDir("/", null);
            Directory currentDir = baseDirectory;

            foreach (string inpy in input)
            {
                List<string> cmds = inpy.Split(" ").ToList();
                switch (cmds[0])
                {
                    case "dir":
                        currentDir.ChildDirectories.Add(CreateDir(cmds[1], currentDir));
                        break;
                    case "$":
                        switch (cmds[1])
                        {
                            case "cd":
                                switch (cmds[2])
                                {
                                    case "/":
                                        break;
                                    case "..":
                                        currentDir = currentDir.ParentDirectory;
                                        break;
                                    default:
                                        currentDir = currentDir.ChildDirectories.First(x => x.Name == cmds[2]);
                                        break;
                                }
                                break;
                            case "ls":
                                break;
                        }
                        break;
                    default:
                        currentDir.Files.Add(CreateFile(cmds));
                        break;
                }
            }
            return baseDirectory;
        }

        /// <summary>
        /// Creates a directory
        /// </summary>
        /// <param name="dirName">Directory name</param>
        /// <param name="parent">Parent directory</param>
        /// <returns>Directory</returns>
        private Directory CreateDir(string dirName, Directory parent) =>
            new Directory
            {
                Name = dirName,
                ParentDirectory = parent,
                ChildDirectories = new(),
                Files = new(),
            };

        /// <summary>
        /// Creates a file node
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns>FileNode</returns>
        private FileNode CreateFile(List<string> fileInfo) =>
            new FileNode
            {
                FileSize = fileInfo[0],
                FileName = fileInfo[1],
            };

        /// <summary>
        /// Directory class set up for tree format
        /// </summary>
        private class Directory
        {
            public string Name { get; set; }
            public Directory ParentDirectory { get; set; }
            public List<Directory> ChildDirectories { get; set; }
            public List<FileNode> Files { get; set; }
        }
        
        /// <summary>
        /// FileNode that will appear in a directory tree
        /// </summary>
        private class FileNode
        {
            public string FileName { get; set; }
            public string FileSize { get; set; }
        }
    }
}
