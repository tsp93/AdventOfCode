using Syncfusion.Blazor.RichTextEditor.Internal;

namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private Task<List<string>> SolveDay9(List<string> input) =>
            Task.FromResult(new List<string>
            {
                RopeTailVisited(input).ToString(),
                LongerRopeTailVisited(input).ToString(),
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
            int[,] traversedRopeBridge = ApplyRopeInstructions(instructions, ropeBridgeMatrix, startX, startY, 1);
            return CountPlacesTailHasTraveled(traversedRopeBridge);
        }

        /// <summary>
        /// Counts the amount of places the tail has gone in a longer rope
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int LongerRopeTailVisited(List<string> input)
        {
            List<(string Direction, int Steps)> instructions =
                input.Select(x => x.Split(" ")).Select(x => (x[0], Util.StringToInt(x[1]))).ToList();

            (int rowSize, int colSize, int startX, int startY) = CalculateMatrixSize(instructions);

            int[,] ropeBridgeMatrix = new int[rowSize, colSize];
            int[,] traversedRopeBridge = ApplyRopeInstructions(instructions, ropeBridgeMatrix, startX, startY, 9);
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
        private int[,] ApplyRopeInstructions(List<(string Direction, int Steps)> instructions, int[,] ropeBridgeMatrix, int startX, int startY, int amountOfTails)
        {
            Dictionary<int, (int positionX, int positionY)> knots = Enumerable.Range(0, amountOfTails + 1)
                                                                              .ToDictionary(x => x, x => (positionX: startX, positionY: startY));
            ropeBridgeMatrix[startX, startY] = 1;

            foreach (var instruction in instructions)
            {
                for (int i = 1; i <= instruction.Steps; i++)
                {
                    var tempKnot = knots[0];
                    var prevPos = knots[0];
                    switch (instruction.Direction)
                    {
                        case ("U"):
                            tempKnot.positionY++;
                            break;
                        case ("D"):
                            tempKnot.positionY--;
                            break;
                        case ("L"):
                            tempKnot.positionX--;
                            break;
                        case ("R"):
                            tempKnot.positionX++;
                            break;
                    }
                    knots[0] = tempKnot;
                    for (int k = 1; k < knots.Count(); k++)
                    {
                        if (IsMoreThanOneAway(knots[k - 1].positionX, knots[k - 1].positionY, knots[k].positionX, knots[k].positionY))
                        {
                            tempKnot = knots[k];

                            if (Util.Abs(knots[k - 1].positionY - knots[k].positionY) > 0)
                            {
                                int add = knots[k - 1].positionY - knots[k].positionY > 0 ? 1 : -1;
                                tempKnot.positionY += add;
                            }

                            if (Util.Abs(knots[k - 1].positionX - knots[k].positionX) > 0)
                            {
                                int add = knots[k - 1].positionX - knots[k].positionX > 0 ? 1 : -1;
                                tempKnot.positionX += add;
                            }
                            knots[k] = tempKnot;

                            if (k == knots.Count() - 1)
                            {
                                ropeBridgeMatrix[knots[k].positionX, knots[k].positionY]++;
                            }
                        }
                    }
                }

            }
            return ropeBridgeMatrix;
        }

        /// <summary>
        /// Checks if tail is more than one away from head
        /// </summary>
        /// <param name="prevX"></param>
        /// <param name="prevY"></param>
        /// <param name="currentX"></param>
        /// <param name="currentY"></param>
        /// <returns></returns>
        private bool IsMoreThanOneAway(int prevX, int prevY, int currentX, int currentY) =>
            Util.Abs(prevX - currentX) > 1 || Util.Abs(prevY - currentY) > 1;

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
