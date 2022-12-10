namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private Task<List<string>> SolveDay8(List<string> input) =>
            Task.FromResult(new List<string>
            {
                TreesVisibleFromOutsideGrid(input).ToString(),
                FindHighestScenicScore(input).ToString(),
            });

        /// <summary>
        /// Finds trees in a grid that are visible from the outside
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int TreesVisibleFromOutsideGrid(List<string> input)
        {
            int[,] trees = CreateTreeMatrix(input);
            return AmountOfTreesVisibleFromOutside(trees);
        }

        /// <summary>
        /// Finds highest screen score, trees in each direction that are lower than the tree itself
        /// multiplied with each other
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int FindHighestScenicScore(List<string> input)
        {
            int[,] trees = CreateTreeMatrix(input);
            return HighestSceneScore(trees);
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

        /// <summary>
        /// Checks left of int array if any are >= index
        /// </summary>
        /// <param name="row"></param>
        /// <param name="index"></param>
        /// <param name="currentTree"></param>
        /// <returns></returns>
        private bool CheckLeft(int[] row, int index, int currentTree) =>
            row.Take(index).Where(x => x >= currentTree).Count() == 0;

        /// <summary>
        /// Checks right of int array if any are >= index
        /// </summary>
        /// <param name="row"></param>
        /// <param name="index"></param>
        /// <param name="currentTree"></param>
        /// <returns></returns>
        private bool CheckRight(int[] row, int index, int currentTree) =>
            row.Skip(index + 1).Take(row.Length - index - 1).Where(x => x >= currentTree).Count() == 0;

        /// <summary>
        /// Finds highest scene score
        /// </summary>
        /// <param name="trees"></param>
        /// <returns></returns>
        private int HighestSceneScore(int[,] trees)
        {
            int rowCount = trees.GetLength(0);
            int columnCount = trees.GetLength(1);

            int currentHighestSceneScore = 0;
            for (int i = 1; i < rowCount; i++)
            {
                int[] currentRow = GetRow(trees, i);
                for (int j = 1; j < columnCount; j++)
                {
                    int[] currentCol = GetColumn(trees, j);
                    int currentTree = trees[i, j];

                    int treeSceneScore = TreeSceneScore(currentRow, currentCol, j, i, currentTree);
                    if (treeSceneScore > currentHighestSceneScore)
                    {
                        currentHighestSceneScore = treeSceneScore;
                    }
                }
            }
            return currentHighestSceneScore;
        }

        /// <summary>
        /// Calculates a tree's total scene score
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="currentTree"></param>
        /// <returns></returns>
        private int TreeSceneScore(int[] rows, int[] columns, int rowIndex, int colIndex, int currentTree)
        {
            List<int> left = rows.Take(rowIndex).Reverse().ToList();
            List<int> right = rows.Skip(rowIndex + 1).Take(rows.Length - rowIndex - 1).ToList();
            List<int> top = columns.Take(colIndex).Reverse().ToList();
            List<int> bottom = columns.Skip(colIndex + 1).Take(columns.Length - colIndex - 1).ToList();

            int leftScore = DirectionSceneScore(left, currentTree);
            int rightScore = DirectionSceneScore(right, currentTree);
            int topScore = DirectionSceneScore(top, currentTree);
            int bottomScore = DirectionSceneScore(bottom, currentTree);

            return leftScore * rightScore * topScore * bottomScore;
        }

        /// <summary>
        /// Calculates a direction's scene score
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="tree"></param>
        /// <returns></returns>
        private int DirectionSceneScore(List<int> direction, int tree)
        {
            int counter = 0;
            for (int i = 0; i < direction.Count(); i++)
            {
                counter++;
                if (direction[i] >= tree)
                {
                    break;
                }
            }
            return counter;
        }

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
