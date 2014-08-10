using System;

namespace SimpleFootBallModel
{
	public class SimpleDoublePoission : IFootBallModel
	{
		private double homeExpectancy;
		private double awayExpectancy;
		private double halfTimeWeight;

		public SimpleDoublePoission (double home, double away)
		{
			homeExpectancy = home;
			awayExpectancy = away;
		}

		public double[] generateHADProbaility (Result result) 
		{
			return null;
		}
		public double[] generateHiLoProbability (double target)
		{
			return null;
		}

	}
}

