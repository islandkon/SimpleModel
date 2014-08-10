using System;

namespace SimpleFootBallModel
{
	public abstract class AbstractModel : IFootBallModel
	{
		public enum Result {HomeWin, Draw, AwayWin };

		public const int MATRIX_LENGTH = 15;

		public const int OVER_TARGET = 0;
		public const int UNDER_TARGET = 1;

		public const int HALF_TIME = 0;
		public const int FULL_TIME = 1;

		public const int HALF_AND_FULLTIME = 2;

		protected double homeExpectancy;
		protected double awayExpectancy;

		protected double[,] homeScoreProbability = new double[2,MATRIX_LENGTH];
		protected double[,] awayScoreProbability = new double[2,MATRIX_LENGTH];

		protected double[,,] probabilityMatrix = new double[2,MATRIX_LENGTH,MATRIX_LENGTH];

		protected abstract double[,] generateScoreProbability(double expectancy);

		public AbstractModel(double home, double away) {
			homeExpectancy = home;
			awayExpectancy = away;
		}

		public void runModel() {
			homeScoreProbability = generateScoreProbability(homeExpectancy);
			awayScoreProbability = generateScoreProbability (awayExpectancy);

			probabilityMatrix = generateScoreMatrix();
		}

		public double[] getHiLoProbabilty(double target) 
		{
			return generateHiLoProbability(target);
		}

		public double[] getHADProbability(Result result) 
		{
			return generateHADProbability(result);
		}

		protected virtual double[,,] generateScoreMatrix () 
		{
			double[,,] probabilityMatrix = new double[HALF_AND_FULLTIME,MATRIX_LENGTH,MATRIX_LENGTH];
			for (int i = 0; i < HALF_AND_FULLTIME; i++)
				for (int j = 0; j < MATRIX_LENGTH; j++)
					for (int k = 0; k < MATRIX_LENGTH; k++)
					{
						probabilityMatrix[i, j, k] = homeScoreProbability[i,j] * awayScoreProbability[i,k];
					}

			return probabilityMatrix;
		}

		protected double[] generateHADProbability(Result result)
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

		protected double[] generateHiLoProbability(double target)
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
	}
}

