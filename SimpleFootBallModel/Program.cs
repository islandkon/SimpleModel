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
			double halfTimeWeight = 0.4;

			IFootBallModel testingModel = new ModifiedZiPoission(HOMETEAM_EXPECTED_GOAL, AWAYTEAM_EXPECTED_GOAL, lambda, halfTimeWeight);

			OddsGenerator generator = new OddsGenerator(testingModel);
			generator.generateOdds();

			Console.WriteLine("Please Any Keys to Exit ...");
			Console.ReadLine();
		}
	}
}

