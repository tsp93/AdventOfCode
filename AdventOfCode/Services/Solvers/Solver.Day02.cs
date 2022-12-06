using AdventOfCode.Enums;

namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private List<string> SolveDay2(List<string> input)
        {
            return new List<string>
            {
                TotalScoreFromStrategyGuideWithWrongInfo(input).ToString(),
                TotalScoreFromStrategyGuideWithRightInfo(input).ToString()
            };
        }

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

        private int RockPaperScissorsScorer(RockPaperScissors opponentPlayed, RockPaperScissors playerPlayed)
        {
            int win = 6;
            int tie = 3;
            int loss = 0;
            int score = (int)playerPlayed;

            switch (opponentPlayed)
            {
                case RockPaperScissors.Rock:
                    switch (playerPlayed)
                    {
                        case RockPaperScissors.Paper:
                            return score + win;
                        case RockPaperScissors.Scissors:
                            return score + loss;
                        default:
                            return score + tie;
                    }
                case RockPaperScissors.Paper:
                    switch (playerPlayed)
                    {
                        case RockPaperScissors.Scissors:
                            return score + win;
                        case RockPaperScissors.Rock:
                            return score + loss;
                        default:
                            return score + tie;
                    }
                case RockPaperScissors.Scissors:
                    switch (playerPlayed)
                    {
                        case RockPaperScissors.Rock:
                            return score + win;
                        case RockPaperScissors.Paper:
                            return score + loss;
                        default:
                            return score + tie;
                    }
            }
            return 0;
        }

        private RockPaperScissorsResult RockPaperScissorsResultConverter(string result) => result switch
        {
            "X" => RockPaperScissorsResult.Loss,
            "Y" => RockPaperScissorsResult.Tie,
            "Z" => RockPaperScissorsResult.Win,
            _ => throw new Exception("Invalid result string"),
        };

        private RockPaperScissors RockPaperScissorsConverter(string rockPaperScissor) => rockPaperScissor switch
        {
            "A" or "X" => RockPaperScissors.Rock,
            "B" or "Y" => RockPaperScissors.Paper,
            "C" or "Z" => RockPaperScissors.Scissors,
            _ => throw new Exception("Invalid RockPaperScissors string"),
        };
    }
}
