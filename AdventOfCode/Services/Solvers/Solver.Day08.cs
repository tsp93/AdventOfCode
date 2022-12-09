namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private List<string> SolveDay8(List<string> input)
        {
            return new List<string>
            {
                TreesVisibleFromOutsideGrid(input).ToString(),
                //RemoveSmallestDirectoryToFreeUpSpace(input).ToString(),
            };
        }

        private int TreesVisibleFromOutsideGrid(List<string> input)
        {
            int[,] trees = CreateTreeMatrix(input);
            return AmountOfTreesVisibleFromOutside(trees);
        }

        /// <summary>
        /// Sums ints that are higher than all numbers above, below, right, or left of them in a matrix
        /// </summary>
        /// <param name="trees"></param>
        /// <returns>Amount of visible trees</returns>
        private int AmountOfTreesVisibleFromOutside(int[,] trees)
        {
            int rowCount = trees.GetLength(0);
            int columnCount = trees.GetLength(1);
            // Outer edge trees
            int counter = rowCount * 2 + columnCount * 2 - 4;
            // Inner trees
            for (int i = 1; i < rowCount - 1; i++)
            {
                int[] currentRow = GetRow(trees, i);
                for (int j = 1; j < columnCount - 1; j++)
                {
                    int[] currentCol = GetColumn(trees, j);
                    int currentTree = trees[i, j];

                    // Check sides
                    if (CheckLeft(currentRow, j, currentTree) || CheckRight(currentRow, j, currentTree)
                        || CheckLeft(currentCol, i, currentTree) || CheckRight(currentCol, i, currentTree))
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        private bool CheckLeft(int[] row, int index, int currentTree) =>
            row.Take(index).Where(x => x >= currentTree).Count() == 0;

        private bool CheckRight(int[] row, int index, int currentTree) =>
            row.Skip(index + 1).Take(row.Length - index - 1).Where(x => x >= currentTree).Count() == 0;

        /// <summary>
        /// Gets the entire row of a matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private int[] GetRow(int[,] matrix, int rowNumber) =>
            Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();

        /// <summary>
        /// Gets the entire column of a matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private int[] GetColumn(int[,] matrix, int columnNumber) =>
            Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();

        /// <summary>
        /// Creates a matrix of ints from a list of equal length strings
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int[,] CreateTreeMatrix(List<string> input)
        {
            int rowCount = input.Count();
            int columnCount = input[0].Count();
            int[,] treeMatrix = new int[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                List<int> inputStringToInt = input[i].Select(Util.CharToInt).ToList();
                for (int j = 0; j < columnCount; j++)
                {
                    treeMatrix[i, j] = inputStringToInt[j];
                }
            }
            return treeMatrix;
        }
    }
}
