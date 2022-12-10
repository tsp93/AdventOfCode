namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private Task<List<string>> SolveDay9(List<string> input) =>
            Task.FromResult(new List<string>
            {
                RopeTailVisited(input).ToString(),
                //FindHighestScenicScore(input).ToString(),
            });


        /// <summary>
        /// Counts the amount of places the tail has gone
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int RopeTailVisited(List<string> input)
        {
            List<(string Direction, int Steps)> instructions =
                input.Select(x => x.Split(" ")).Select(x => (x[0], Util.StringToInt(x[1]))).ToList();

            (int rowSize, int colSize, int startX, int startY) = CalculateMatrixSize(instructions);

            int[,] ropeBridgeMatrix = new int[rowSize, colSize];
            int[,] traversedRopeBridge = ApplyRopeInstructions(instructions, ropeBridgeMatrix, startX, startY);
            return CountPlacesTailHasTraveled(traversedRopeBridge);
        }

        /// <summary>
        /// Counts places in matrix where value is higher than 0
        /// </summary>
        /// <param name="traversedRopeBridge"></param>
        /// <returns></returns>
        private int CountPlacesTailHasTraveled(int[,] traversedRopeBridge)
        {
            int counter = 0;
            for (int i = 0; i < traversedRopeBridge.GetLength(0); i++)
            {
                for (int j = 0; j < traversedRopeBridge.GetLength(1); j++)
                {
                    if (traversedRopeBridge[i, j] > 0)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        /// <summary>
        /// Goes through with the head and tail and counts the places the tail has been
        /// </summary>
        /// <param name="instructions"></param>
        /// <param name="ropeBridgeMatrix"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <returns></returns>
        private int[,] ApplyRopeInstructions(List<(string Direction, int Steps)> instructions, int[,] ropeBridgeMatrix, int startX, int startY)
        {
            (int currentHeadX, int currentHeadY) = (startX, startY);
            (int currentTailX, int currentTailY) = (startX, startY);
            ropeBridgeMatrix[startX, startY] = 1;

            try
            {
                foreach (var instruction in instructions)
                {
                    switch (instruction.Direction)
                    {
                        case ("U"):
                            for (int i = 1; i <= instruction.Steps; i++)
                            {
                                currentHeadY++;
                                if (IsMoreThanOneAway(currentHeadX, currentHeadY, currentTailX, currentTailY))
                                {
                                    (currentTailX, currentTailY) = (currentHeadX, currentHeadY - 1);
                                    ropeBridgeMatrix[currentTailX, currentTailY]++;
                                }
                            }
                            break;
                        case ("D"):
                            for (int i = 1; i <= instruction.Steps; i++)
                            {
                                currentHeadY--;
                                if (IsMoreThanOneAway(currentHeadX, currentHeadY, currentTailX, currentTailY))
                                {
                                    (currentTailX, currentTailY) = (currentHeadX, currentHeadY + 1);
                                    ropeBridgeMatrix[currentTailX, currentTailY]++;
                                }
                            }
                            break;
                        case ("L"):
                            for (int i = 1; i <= instruction.Steps; i++)
                            {
                                currentHeadX--;
                                if (IsMoreThanOneAway(currentHeadX, currentHeadY, currentTailX, currentTailY))
                                {
                                    (currentTailX, currentTailY) = (currentHeadX + 1, currentHeadY);
                                    ropeBridgeMatrix[currentTailX, currentTailY]++;
                                }
                            }
                            break;
                        case ("R"):
                            for (int i = 1; i <= instruction.Steps; i++)
                            {
                                currentHeadX++;
                                if (IsMoreThanOneAway(currentHeadX, currentHeadY, currentTailX, currentTailY))
                                {
                                    (currentTailX, currentTailY) = (currentHeadX - 1, currentHeadY);
                                    ropeBridgeMatrix[currentTailX, currentTailY]++;
                                }
                            }
                            break;
                    }
                }
            }
            catch(Exception e)
            {
                string test = e.Message;
            }
            return ropeBridgeMatrix;
        }

        private bool IsMoreThanOneAway(int headX, int headY, int tailX, int tailY) =>
            Util.Abs(headX - tailX) > 1 || Util.Abs(headY - tailY) > 1;

        /// <summary>
        /// Calculates the matrix size needed since dynamic matrixes are not supported
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        private (int rowSize, int colSize, int startX, int startY) CalculateMatrixSize(List<(string Direction, int Steps)> instructions)
        {
            int maxTop = 0;
            int maxBottom = 0;
            int maxLeft = 0;
            int maxRight = 0;
            int currentX = 1;
            int currentY = 1;
            foreach (var inst in instructions)
            {
                switch (inst.Direction)
                {
                    case ("U"):
                        currentY += inst.Steps;
                        maxTop = currentY > maxTop ? currentY : maxTop;
                        break;
                    case ("D"):
                        currentY -= inst.Steps;
                        maxBottom = currentY < maxBottom ? currentY : maxBottom;
                        break;
                    case ("L"):
                        currentX -= inst.Steps;
                        maxLeft = currentX < maxLeft ? currentX : maxLeft;
                        break;
                    case ("R"):
                        currentX += inst.Steps;
                        maxRight = currentX > maxRight ? currentX : maxRight;
                        break;
                }
            }

            return (maxTop + Util.Abs(maxBottom) + 100, Util.Abs(maxLeft) + maxRight + 100, Util.Abs(maxLeft) + 10, Util.Abs(maxBottom) + 10);
        }
    }
}
