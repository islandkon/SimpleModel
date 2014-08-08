using System;

namespace SimpleFootBallModel
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

		/// <summary>
		/// Main class for odds generation
		/// </summary>
		class OddsGenerator
		{
			//Length for our score probability vector and matrix
			private const int MATRIX_LENGTH = 15;

			private const int OVER_TARGET = 0;
			private const int UNDER_TARGET = 1;

			private const int HALF_TIME = 0;
			private const int FULL_TIME = 1;
			private const int HALF_AND_FULLTIME = 2;

			private double[,] homeScoreProbability = new double[2,MATRIX_LENGTH];
			private double[,] awayScoreProbability = new double[2,MATRIX_LENGTH];

			private double[,,] probabilityMatrix = new double[2,MATRIX_LENGTH,MATRIX_LENGTH];

			private double homeTeamExpectedGoal;
			private double awayTeamExpectedGoal;

			private enum Result { HomeWin, Draw, AwayWin };

			/// <summary>
			/// (1-lamdba) is the estimated probability for extra zeros in Zero Inflated Poission Model
			/// This is an estimate variable and should change according to different matches and league.
			/// Please refer to "Modelling the Scores of Premier League Football Matched, Daan Van Gamert"
			/// </summary>
			private double lamdba = 0.965;
			/// <summary>
			/// Dixon and Robinson (1998) indicate theres a increasing rate of goal in football match,
			/// I expect a 40% of the goal occur in first half
			/// This is an estimate variable and should change according to matches and league
			/// ([Dixon, Robinson (1998)] A birth process model for association football matches)
			/// </summary>
			private double halfTimeWeight = 0.4;
			/// <summary>
			/// 4 percent margin
			/// </summary>
			private double margin = 0.04;

			public OddsGenerator(double home, double away)
			{
				homeTeamExpectedGoal = home;
				awayTeamExpectedGoal = away;
			}

			/// <summary>
			/// Generate score probabiility for each teamm then generate the joint score probability matrix
			/// </summary>
			private void runGenerator()
			{
				generateScoreProbability(homeTeamExpectedGoal,ref homeScoreProbability);
				generateScoreProbability(awayTeamExpectedGoal,ref awayScoreProbability);

				generateScoreMatrix();
			}

			private void printResult(double[] homeWin, double[] awayWin, double[] draw, double[] hilo) {
				Console.WriteLine("Results: ");
				Console.WriteLine();

				Console.WriteLine("| Home | Away | Draw | (With Margin)");
				Console.WriteLine("| " + oddsFormat(probabilityToOdds(homeWin[FULL_TIME], margin)) + " | " + oddsFormat(probabilityToOdds(awayWin[FULL_TIME], margin)) + " | " + oddsFormat(probabilityToOdds(draw[FULL_TIME],margin)) + " |");
				Console.WriteLine();

				Console.WriteLine("| Home | Away | Draw | (WithOut Margin)");
				Console.WriteLine("| " + oddsFormat(probabilityToOdds(homeWin[FULL_TIME], 0.0)) + " | " + oddsFormat(probabilityToOdds(awayWin[FULL_TIME], 0.0)) + " | " + oddsFormat(probabilityToOdds(draw[FULL_TIME], 0.0)) + " |");
				Console.WriteLine();

				Console.WriteLine("| Home | Away | Draw | First Half HAD (With Margin)");
				Console.WriteLine("| " + oddsFormat(probabilityToOdds(homeWin[HALF_TIME], margin)) + " | " + oddsFormat(probabilityToOdds(awayWin[HALF_TIME], margin)) + " | " + oddsFormat(probabilityToOdds(draw[HALF_TIME], margin)) + " |");
				Console.WriteLine();

				Console.WriteLine("| Home | Away | Draw | First Half HAD (WithOut Margin)");
				Console.WriteLine("| " + oddsFormat(probabilityToOdds(homeWin[HALF_TIME], 0.0)) + " | " + oddsFormat(probabilityToOdds(awayWin[HALF_TIME], 0.0)) + " | " + oddsFormat(probabilityToOdds(draw[HALF_TIME], 0.0))+ " |");
				Console.WriteLine();

				Console.WriteLine("| Line | High | Low  | HiLo (With Margin)");
				Console.WriteLine("| 2.5  | " +oddsFormat(probabilityToOdds(hilo[OVER_TARGET], margin)) + " | " + oddsFormat(probabilityToOdds(hilo[UNDER_TARGET], margin)) + " |");
				Console.WriteLine();

				Console.WriteLine("| Line | High | Low  | HiLo (WithOut Margin)");
				Console.WriteLine("| 2.5  | " +oddsFormat(probabilityToOdds(hilo[OVER_TARGET], 0.0)) + " | " + oddsFormat(probabilityToOdds(hilo[UNDER_TARGET], 0.0)) + " |");
				Console.WriteLine();
			}

			/// <summary>
			/// Core function to generate all odds (HAD, HiLo, First Half HAD)
			/// </summary>
			public void generateOdds()
			{
				runGenerator();

				double[] homeWin = generateHADProbability(Result.HomeWin);
				double[] awayWin = generateHADProbability(Result.AwayWin);
				double[] draw = generateHADProbability(Result.Draw);

				double[] hilo = generateHiLoProbability(2.5);

				printResult(homeWin, awayWin, draw, hilo);
			}
			/// <summary>
			/// Function to generate score probability, we use Zero Inflated Poission to model our score
			/// Distribution and we apply a half time weight to represent Half time goal expectany for each team.
			/// </summary>
			/// <param name="expectancy">Goal expectany for team</param>
			/// <param name="array">Score Probaility for each team</param>
			private void generateScoreProbability(double expectancy, ref double[,] array)
			{
				for (int i = 0; i < HALF_AND_FULLTIME;i++)
					for (int j = 0; j < MATRIX_LENGTH; j++)
					{
						if (i == HALF_TIME)
							array[i, j] = MathUtil.ZeroInflatedPoisson(j, expectancy*halfTimeWeight, lamdba);
						else
							array[i, j] = MathUtil.ZeroInflatedPoisson(j, expectancy, lamdba);
					}
			}

			private void generateScoreMatrix()
			{
				for (int i = 0; i < HALF_AND_FULLTIME; i++)
					for (int j = 0; j < MATRIX_LENGTH; j++)
						for (int k = 0; k < MATRIX_LENGTH; k++)
						{
							probabilityMatrix[i, j, k] = homeScoreProbability[i,j] * awayScoreProbability[i,k];
						}
			}

			private double[] generateHADProbability(Result result)
			{
				double[] probability = new double[2] {0.0,0.0};

				for (int i = 0; i < HALF_AND_FULLTIME; i++)
					for (int j = 0; j < MATRIX_LENGTH; j++)
						for (int k = 0; k < MATRIX_LENGTH; k++)
						{
							if (result == Result.HomeWin)
							{
								if (j > k)
									probability[i] += probabilityMatrix[i, j, k];
							}
							else if (result == Result.AwayWin)
							{
								if (j < k)
									probability[i] += probabilityMatrix[i, j, k];
							}
							else
							{
								if (j == k)
									probability[i] += probabilityMatrix[i, j, k];
							}
						}

				return probability;
			}

			private double[] generateHiLoProbability(double target)
			{
				double[] probability = new double[2] { 0.0, 0.0 };

				for (int i = 0; i < MATRIX_LENGTH; i++)
					for (int j = 0; j < MATRIX_LENGTH; j++)
					{
						if (i + j > target)
							probability[OVER_TARGET] += probabilityMatrix[FULL_TIME, i, j];
						else
							probability[UNDER_TARGET] += probabilityMatrix[FULL_TIME, i, j];
					}

				return probability;
			}

			private double probabilityToOdds(double probability, double margin)
			{
				return 1 / (probability + margin);
			}

			private string oddsFormat(double odds)
			{
				return String.Format("{0:0.00}",odds);
			}
		}
}

