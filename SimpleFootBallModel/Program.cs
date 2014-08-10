using System;

namespace SimpleFootBallModel
{
	class MainClass
	{
		/// <summary>
		/// Main Function for Part 2 of Coding Test
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			double HOMETEAM_EXPECTED_GOAL = 1.55;
			double AWAYTEAM_EXPECTED_GOAL = 1;

			double lambda = 0.965;
			double DicksonColesAdjustment = -0.15;
			double halfTimeWeight = 0.4;
			double margin = 0.04;
			double target = 2.5;

			IFootBallModel zeroInflatedPoission = new ModifiedZiPoission(
				HOMETEAM_EXPECTED_GOAL, AWAYTEAM_EXPECTED_GOAL,lambda, halfTimeWeight);

			IFootBallModel doublePoission = new DoublePoission (
				HOMETEAM_EXPECTED_GOAL, AWAYTEAM_EXPECTED_GOAL, halfTimeWeight);

			IFootBallModel DixonColes = new DixonColes97 (
				HOMETEAM_EXPECTED_GOAL, AWAYTEAM_EXPECTED_GOAL, DicksonColesAdjustment, halfTimeWeight);

			//Zero Inflated Poission
			OddsGenerator generator = new OddsGenerator(zeroInflatedPoission, margin, target);
			generator.generateOdds();

			//Double Poission (Maher 1982)
			generator = new OddsGenerator(doublePoission, margin, target);
			generator.generateOdds();

			//Dixon Cole (Dixon Cole 1997)
			generator = new OddsGenerator(DixonColes, margin, target);
			generator.generateOdds();

			Console.WriteLine("Please Any Keys to Exit ...");
			Console.ReadLine();
		}
	}
}

