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

				OddsGenerator generator = new OddsGenerator(HOMETEAM_EXPECTED_GOAL,AWAYTEAM_EXPECTED_GOAL);
				generator.generateOdds();

				Console.WriteLine("Please Any Keys to Exit ...");
				Console.ReadLine();
			}
	}
}

