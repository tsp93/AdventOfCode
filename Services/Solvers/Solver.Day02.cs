using AdventOfCode.Enums;

namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private Task<List<string>> SolveDay2(List<string> input) =>
            Task.FromResult(new List<string>
            {
                TotalScoreFromStrategyGuideWithWrongInfo(input).ToString(),
                TotalScoreFromStrategyGuideWithRightInfo(input).ToString(),
            });


        /// <summary>
        /// Gets total score of the strategy guide the elf gave, with no instructions on how to use it
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Total score</returns>
        private int TotalScoreFromStrategyGuideWithWrongInfo(List<string> input)
        {
            List<(string Opponent, string Player)> opponentVsPlayer = input.Select(x => x.Split(' ')).Select(x => (x[0], x[1])).ToList();

            List<int> scorePerRound = opponentVsPlayer.Select(x =>
                RockPaperScissorsScorer(
                    RockPaperScissorsConverter(x.Opponent),
                    RockPaperScissorsConverter(x.Player)))
                .ToList();

            return scorePerRound.Sum();
        }

        /// <summary>
        /// Gets total score of the strategy guide the elf gave, with instructions on how to use it
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Total score</returns>
        private int TotalScoreFromStrategyGuideWithRightInfo(List<string> input)
        {
            List<(string Opponent, string Result)> opponentVsPlayer = input.Select(x => x.Split(' ')).Select(x => (x[0], x[1])).ToList();

            List<int> scorePerRound = opponentVsPlayer.Select(x =>
                RockPaperScissorsScorer(
                    RockPaperScissorsConverter(x.Opponent),
                    RockPaperScissorsShouldPlay(
                        RockPaperScissorsConverter(x.Opponent),
                        RockPaperScissorsResultConverter(x.Result))))
                .ToList();

            return scorePerRound.Sum();
        }

        /// <summary>
        /// Finds what player should play according to the strategy guide
        /// </summary>
        /// <param name="opponentPlayed"></param>
        /// <param name="neededResult"></param>
        /// <returns>RockPaperScissors</returns>
        private RockPaperScissors RockPaperScissorsShouldPlay(RockPaperScissors opponentPlayed, RockPaperScissorsResult neededResult)
        {
            switch (opponentPlayed)
            {
                case RockPaperScissors.Rock:
                    switch (neededResult)
                    {
                        case RockPaperScissorsResult.Win:
                            return RockPaperScissors.Paper;
                        case RockPaperScissorsResult.Loss:
                            return RockPaperScissors.Scissors;
                        default:
                            return RockPaperScissors.Rock;
                    }
                case RockPaperScissors.Paper:
                    switch (neededResult)
                    {
                        case RockPaperScissorsResult.Win:
                            return RockPaperScissors.Scissors;
                        case RockPaperScissorsResult.Loss:
                            return RockPaperScissors.Rock;
                        default:
                            return RockPaperScissors.Paper;
                    }
                case RockPaperScissors.Scissors:
                    switch (neededResult)
                    {
                        case RockPaperScissorsResult.Win:
                            return RockPaperScissors.Rock;
                        case RockPaperScissorsResult.Loss:
                            return RockPaperScissors.Paper;
                        default:
                            return RockPaperScissors.Scissors;
                    }
            }
            return RockPaperScissors.Rock;
        }

        /// <summary>
        /// Rock, paper, scissors game logic
        /// </summary>
        /// <param name="opponentPlayed"></param>
        /// <param name="playerPlayed"></param>
        /// <returns>Score</returns>
        private int RockPaperScissorsScorer(RockPaperScissors opponentPlayed, RockPaperScissors playerPlayed)
        {
            int score = (int)playerPlayed;
            switch (opponentPlayed)
            {
                case RockPaperScissors.Rock:
                    switch (playerPlayed)
                    {
                        case RockPaperScissors.Paper:
                            return score + (int)RockPaperScissorsResult.Win;
                        case RockPaperScissors.Scissors:
                            return score + (int)RockPaperScissorsResult.Loss;
                        default:
                            return score + (int)RockPaperScissorsResult.Tie;
                    }
                case RockPaperScissors.Paper:
                    switch (playerPlayed)
                    {
                        case RockPaperScissors.Scissors:
                            return score + (int)RockPaperScissorsResult.Win;
                        case RockPaperScissors.Rock:
                            return score + (int)RockPaperScissorsResult.Loss;
                        default:
                            return score + (int)RockPaperScissorsResult.Tie;
                    }
                case RockPaperScissors.Scissors:
                    switch (playerPlayed)
                    {
                        case RockPaperScissors.Rock:
                            return score + (int)RockPaperScissorsResult.Win;
                        case RockPaperScissors.Paper:
                            return score + (int)RockPaperScissorsResult.Loss;
                        default:
                            return score + (int)RockPaperScissorsResult.Tie;
                    }
            }
            return 0;
        }

        /// <summary>
        /// Converts inputstring to RockPaperScissorsResult
        /// </summary>
        /// <param name="result"></param>
        /// <returns>RockPaperScissorsResult</returns>
        /// <exception cref="Exception"></exception>
        private RockPaperScissorsResult RockPaperScissorsResultConverter(string result) => result switch
        {
            "X" => RockPaperScissorsResult.Loss,
            "Y" => RockPaperScissorsResult.Tie,
            "Z" => RockPaperScissorsResult.Win,
            _ => throw new Exception("Invalid result string"),
        };

        /// <summary>
        /// Converts string to RockPaperScissors
        /// </summary>
        /// <param name="rockPaperScissor"></param>
        /// <returns>RockPaperScissors</returns>
        /// <exception cref="Exception"></exception>
        private RockPaperScissors RockPaperScissorsConverter(string rockPaperScissor) => rockPaperScissor switch
        {
            "A" or "X" => RockPaperScissors.Rock,
            "B" or "Y" => RockPaperScissors.Paper,
            "C" or "Z" => RockPaperScissors.Scissors,
            _ => throw new Exception("Invalid RockPaperScissors string"),
        };
    }
}
