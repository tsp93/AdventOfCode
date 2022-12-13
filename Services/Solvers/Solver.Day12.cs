using AdventOfCode.Enums;

namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private Task<List<string>> SolveDay12(List<string> input) =>
            Task.FromResult(new List<string>
            {
                GetShortestRouteOnMap(input).ToString(),
                //TopTwoMonkeyBusiness(input, 10000, 1).ToString(),
            });

        private int GetShortestRouteOnMap(List<string> input)
        {
            shortest = -1;
            (char[,] heightMap, int startRow, int startCol, int endRow, int endCol) = CreateHeightMap(input);
            int[,] trackMap = new int[heightMap.GetLength(0), heightMap.GetLength(1)];
            heightMap[startRow, startCol] = 'a';
            heightMap[endRow, endCol] = (char)('z' + 1);
            FindShortestRoute(heightMap, trackMap, startRow, startCol, -1, endRow, endCol, 1);
            return this.shortest;
        }

        int shortest = -1;

        private void FindShortestRoute(char[,] heightMap, int[,] trackMap, int currentRow, int currentCol, int currentSteps, int endRow, int endCol, int counter)
        {
            currentSteps++;
            if (shortest != -1 && currentSteps > shortest)
            {
                return;
            }
            else if (currentRow == endRow && currentCol == endCol)
            {
                if (shortest == -1 || currentSteps < shortest)
                {
                    shortest = currentSteps;
                    List<string> path = DrawPath(trackMap);
                }
                return;
            }
            trackMap[currentRow, currentCol] = counter;
            counter++;

            try
            {
                bool upViable = currentRow > 0 && trackMap[currentRow - 1, currentCol] == 0 && heightMap[currentRow - 1, currentCol] - heightMap[currentRow, currentCol] <= 1;
                bool downViable = currentRow < heightMap.GetLength(0) - 1 && trackMap[currentRow + 1, currentCol] == 0 && heightMap[currentRow + 1, currentCol] - heightMap[currentRow, currentCol] <= 1;
                bool leftViable = currentCol > 0 && trackMap[currentRow, currentCol - 1] == 0 && heightMap[currentRow, currentCol - 1] - heightMap[currentRow, currentCol] <= 1;
                bool rightViable = currentCol < heightMap.GetLength(1) - 1 && trackMap[currentRow, currentCol + 1] == 0 && heightMap[currentRow, currentCol + 1] - heightMap[currentRow, currentCol] <= 1;

                if (upViable)
                {
                    FindShortestRoute(heightMap, trackMap, currentRow - 1, currentCol, currentSteps, endRow, endCol, counter);
                }
                if (downViable)
                {
                    FindShortestRoute(heightMap, trackMap, currentRow + 1, currentCol, currentSteps, endRow, endCol, counter);
                }
                if (leftViable)
                {
                    FindShortestRoute(heightMap, trackMap, currentRow, currentCol - 1, currentSteps, endRow, endCol, counter);
                }
                if (rightViable)
                {
                    FindShortestRoute(heightMap, trackMap, currentRow, currentCol + 1, currentSteps, endRow, endCol, counter);
                }
                trackMap[currentRow, currentCol] = 0;
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }

        private List<string> DrawPath(int[,] trackMap)
        {
            int[,] changedTrackMap = trackMap;

            List<string> path = new List<string>();
            for (int i = 0; i < changedTrackMap.GetLength(0); i++)
            {
                string row = "";
                for (int j = 0; j < changedTrackMap.GetLength(1); j++)
                {
                    row += " " + changedTrackMap[i, j].ToString();
                }
                path.Add(row);
            }
            return path;
        }

        private (char[,], int startX, int startY, int endX, int endY) CreateHeightMap(List<string> input)
        {
            int rowCount = input.Count();
            int colCount = input[0].Length;
            (int startX, int startY) = (0, 0);
            (int endX, int endY) = (0, 0);
            char[,] heightMap = new char[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    heightMap[i, j] = input[i][j];
                    if (heightMap[i, j] == 'S')
                    {
                        (startX, startY) = (i, j);
                    }
                    else if (heightMap[i, j] == 'E')
                    {
                        (endX, endY) = (i, j);
                    }
                }
            }
            return (heightMap, startX, startY, endX, endY);
        }
    }
}
