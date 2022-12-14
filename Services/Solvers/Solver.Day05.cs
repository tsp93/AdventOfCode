namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private Task<List<string>> SolveDay5(List<string> input) =>
            Task.FromResult(new List<string>
            {
                CraneMover9000(input),
                CraneMover9001(input),
            });

        /// <summary>
        /// Retrieves crates on top of each stack after the series of movements in the input have been made
        /// </summary>
        /// <param name="input"></param>
        /// <returns>String of top crates</returns>
        private string CraneMover9000(List<string> input)
        {
            List<string> crates = input.Where(x => !x.StartsWith("move")).Where(x => !Util.IsEmptyString(x)).ToList();
            List<string> moves = input.Where(x => x.StartsWith("move")).ToList();
            Dictionary<int, List<string>> crateMatrix = CrateMatrixMaker(crates);
            Dictionary<int, List<string>> movedCrates = MoveCrates(moves, crateMatrix, true);

            return string.Join("", movedCrates.Select(x => x.Value.Last()));
        }

        /// <summary>
        /// Retrieves crates on top of each stack after the series of movements in the input have been made
        /// </summary>
        /// <param name="input"></param>
        /// <returns>String of top crates</returns>
        private string CraneMover9001(List<string> input)
        {
            List<string> crates = input.Where(x => !x.StartsWith("move")).Where(x => !Util.IsEmptyString(x)).ToList();
            List<string> moves = input.Where(x => x.StartsWith("move")).ToList();
            Dictionary<int, List<string>> crateMatrix = CrateMatrixMaker(crates);
            Dictionary<int, List<string>> movedCrates = MoveCrates(moves, crateMatrix, false);

            return string.Join("", movedCrates.Select(x => x.Value.Last()));
        }

        /// <summary>
        /// Applies moves to crates
        /// </summary>
        /// <param name="moves"></param>
        /// <param name="crates"></param>
        /// <param name="craneMover9000"></param>
        /// <returns>Moved crates</returns>
        private Dictionary<int, List<string>> MoveCrates(List<string> moves, Dictionary<int, List<string>> crates, bool craneMover9000)
        {
            List<(int amount, int columnFrom, int columnTo)> movesTransformed =
                moves.Select(x => x.Split(" ").Where(x => Util.IsNumber(x)).ToList())
                     .Select(x => (Util.StringToInt(x[0]), Util.StringToInt(x[1]), Util.StringToInt(x[2])))
                     .ToList();

            foreach (var move in movesTransformed)
            {
                if (craneMover9000)
                {
                    for (int i = 0; i < move.amount; i++)
                    {
                        string crate = crates[move.columnFrom].Last();
                        crates[move.columnFrom].RemoveAt(crates[move.columnFrom].Count - 1);
                        crates[move.columnTo].Add(crate);
                    }
                }
                else
                {
                    int startPos = crates[move.columnFrom].Count() - move.amount;
                    List<string> cratesToMove = crates[move.columnFrom].Skip(startPos).Take(move.amount).ToList();
                    crates[move.columnFrom].RemoveRange(startPos, move.amount);
                    crates[move.columnTo].AddRange(cratesToMove);
                }
            }

            return crates;
        }

        /// <summary>
        /// Creates a 2D matrix for the crates
        /// </summary>
        /// <param name="crates"></param>
        /// <returns>2D matrix</returns>
        private Dictionary<int, List<string>> CrateMatrixMaker(List<string> crates)
        {
            string columnNumbers = crates[crates.Count - 1];
            crates.RemoveAt(crates.Count - 1);

            List<int> columnNumbersParsed = columnNumbers.Split(" ")
                                                         .Where(x => !Util.IsEmptyString(x))
                                                         .Select(Util.StringToInt)
                                                         .ToList();
            List<int> setColumnNumbers = Enumerable.Range(1, columnNumbersParsed.Count).ToList();
            int columns = columnNumbersParsed.Last();
            int rows = crates.Count;

            Dictionary<int, List<string>> crateMatrixDict = setColumnNumbers.ToDictionary(x => x, y => new List<string>());

            int spacing = 4;
            for (int i = rows - 1; i >= 0; i--)
            {
                int positionCounter = 1;
                for (int j = 0; j < columns; j++)
                {
                    try
                    {
                        string crate = crates[i][positionCounter + j * spacing].ToString();
                        if (!Util.IsEmptyString(crate))
                        {
                            crateMatrixDict[j + 1].Add(crate);
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
            }

            return crateMatrixDict;
        }
    }
}
