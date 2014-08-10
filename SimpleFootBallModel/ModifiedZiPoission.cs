using System;

namespace SimpleFootBallModel
{
	public class ModifiedZiPoission : AbstractModel
	{
		private double homeExpectancy;
		private double awayExpectancy;
		private double lambda;
		private double halfTimeWeight;

		public ModifiedZiPoission (double home, double away, double lambda, double weight)
		{
			homeExpectancy = home;
			awayExpectancy = away;
			this.lambda = lambda;
			this.halfTimeWeight = weight;
		}

		public override void runModel() {
			base.homeScoreProbability = generateScoreProbability(homeExpectancy);
			base.awayScoreProbability = generateScoreProbability (awayExpectancy);

			base.probabilityMatrix = base.generateScoreMatrix();
		}

		protected override double[,] generateScoreProbability(double expectancy)
		{
			double[,] array = new double[2,MATRIX_LENGTH];

			for (int i = 0; i < HALF_AND_FULLTIME;i++)
				for (int j = 0; j < MATRIX_LENGTH; j++)
				{
					if (i == HALF_TIME)
						array[i, j] = MathUtil.ZeroInflatedPoisson(j, expectancy*halfTimeWeight, lambda);
					else
						array[i, j] = MathUtil.ZeroInflatedPoisson(j, expectancy, lambda);
				}

			return array;
		}

		public override double[] getHiLoProbabilty(double target) 
		{
			return base.generateHiLoProbability(target);
		}

		public override double[] getHADProbability(Result result) 
		{
			return base.generateHADProbability(result);
		}
	}
}

