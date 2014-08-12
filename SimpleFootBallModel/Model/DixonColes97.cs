using System;

namespace SimpleFootBallModel
{
	public class DixonColes97 : AbstractModel
	{
		private double lambda;
		private double halfTimeWeight;

		public DixonColes97 (double home, double away, double lambda, double weight) : base(home, away)
		{
			this.lambda = lambda;
			this.halfTimeWeight = weight;
		}
			
		protected override double[,,] generateScoreMatrix () 
		{
			double[,,] probabilityMatrix = new double[HALF_AND_FULLTIME,MATRIX_LENGTH,MATRIX_LENGTH];
			for (int i = 0; i < HALF_AND_FULLTIME; i++)
				for (int j = 0; j < MATRIX_LENGTH; j++)
					for (int k = 0; k < MATRIX_LENGTH; k++)
					{
						probabilityMatrix[i, j, k] = adjustMent(j,k,homeExpectancy,awayExpectancy,lambda)
							*homeScoreProbability[i,j] * awayScoreProbability[i,k];
					}

			return probabilityMatrix;
		}

		protected override double[,] generateScoreProbability(double expectancy)
		{
			double[,] array = new double[2,MATRIX_LENGTH];

			for (int i = 0; i < HALF_AND_FULLTIME;i++)
				for (int j = 0; j < MATRIX_LENGTH; j++)
				{
					if (i == HALF_TIME)
						array[i, j] = MathUtil.Poission(j, expectancy*halfTimeWeight);
					else
						array[i, j] = MathUtil.Poission(j, expectancy);
				}

			return array;
		}

		private double adjustMent(double x, double y, double home, double away, double p)
		{
			if (x == y) {
				if (x == 0) {
					return 1.0 - p * home * away;
				} else if (x == 1) {
					return 1.0 - p;
				} else {
					return 1.0;
				}
			} else {
				if (x == 0 && y == 1) {
					return 1 + p * home;
				} else if (x == 1 && y == 0) {
					return 1.0 + p * away;
				} else {
					return 1.0;
				}
			}
		}

	}
}

