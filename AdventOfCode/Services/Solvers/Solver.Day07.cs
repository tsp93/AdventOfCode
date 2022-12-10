namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private Task<List<string>> SolveDay7(List<string> input) =>
            Task.FromResult(new List<string>
            {
                TotalSizeOfDirectoriesAboveCertainSize(input).ToString(),
                RemoveSmallestDirectoryToFreeUpSpace(input).ToString(),
            });

        /// <summary>
        /// Calculates and sums the total size of directories according to the input given
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int TotalSizeOfDirectoriesAboveCertainSize(List<string> input)
        {
            Directory dirTree = ExecuteCommands(input);
            dirTree = SetDirSizes(dirTree);
            int maxFileSize = 100000;
            return SumDirSizes(dirTree, maxFileSize);
        }

        /// <summary>
        /// Removes the smallest directory from directories according to the input given
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int RemoveSmallestDirectoryToFreeUpSpace(List<string> input)
        {
            Directory dirTree = ExecuteCommands(input);
            dirTree = SetDirSizes(dirTree);

            int totalDiskSize = 70000000;
            int requiredUnusedSpace = 30000000;
            int maxSpaceAllowed = totalDiskSize - requiredUnusedSpace;
            int totalDirSize = dirTree.DirSize;

            Directory delDir = CreateDir("Test", null);
            delDir.DirSize = 0;
            if (totalDirSize > maxSpaceAllowed)
            {
                delDir = FindSmallestDirToDelete(dirTree, totalDirSize - maxSpaceAllowed);
            }

            return delDir.DirSize;
        }

        /// <summary>
        /// Finds smallest directory that is closest to the amount of space to get rid of,
        /// without going under it
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="spaceToGetRidOf"></param>
        /// <returns></returns>
        private Directory FindSmallestDirToDelete(Directory dir, int spaceToGetRidOf)
        {
            Directory smallest = dir;
            if (dir.ChildDirectories.Any())
            {
                foreach (Directory child in dir.ChildDirectories)
                {
                    int diff = smallest.DirSize - spaceToGetRidOf;
                    int childDiff = child.DirSize - spaceToGetRidOf;

                    if (diff >= 0 && childDiff >= 0 && diff > childDiff)
                    {
                        smallest = FindSmallestDirToDelete(child, spaceToGetRidOf);
                    }
                }
            }
            return smallest;
        }

        /// <summary>
        /// Sums the directory sizes if their size is <= 100.000
        /// </summary>
        /// <param name="dir"></param>
        /// <returns>Sum of directory sizes</returns>
        private int SumDirSizes(Directory dir, int maxFileSize)
        {
            int sum = dir.DirSize <= maxFileSize ? dir.DirSize : 0;
            if (dir.ChildDirectories.Count > 0)
            {
                foreach (Directory child in dir.ChildDirectories)
                {
                    sum += SumDirSizes(child, maxFileSize);
                }
            }
            return sum;
        }

        /// <summary>
        /// Sets the sizes of each directory in directory tree
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private Directory SetDirSizes(Directory dir)
        {
            int thisDirFileSize = dir.Files.Select(x => x.FileSize).Sum();
            if (dir.ChildDirectories.Count > 0)
            {
                foreach (Directory child in dir.ChildDirectories)
                {
                    thisDirFileSize += SetDirSizes(child).DirSize;
                }
            }
            dir.DirSize = thisDirFileSize;
            return dir;
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
                DirSize = 0,
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
                FileSize = Util.StringToInt(fileInfo[0]),
                FileName = fileInfo[1],
            };

        /// <summary>
        /// Directory class set up for tree format
        /// </summary>
        private class Directory
        {
            public string Name { get; set; }
            public int DirSize { get; set; }
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
            public int FileSize { get; set; }
        }
    }
}
