Solution to Generate Football Odds for:
 
1. HAD (Home, Away, Draw)
2. First Half HAD
3. HiLo
 
Approach:
The basis of my model is the model proposed by Maher(1982) which model goals as independent Poisson variables.
As discussed in Dixon and Coles(1997), Poission distribution overestimate the home and away teams score one or two goals,
while underestimate the the probability for zero goal match. 
 
To cater this issue, my model model goals with zero-inflated Poission Model, using estimate from Daan van Gemert(2010)
to model the probability of extra zeros in football match, this probability should be a variable base on estimate and varies across matches.
 
In order to price a fair "First Half HAD odds", the first thing we have to consider is, 
"Are scoring rate remain constant throughout 90 minutes?". 
 
As state in Dixon and Robinson(1996), football matches shows a gradual increase in scoring rate. 
In order to cater this pheonmenon, I apply a weighting factor of 0.4 on individual team goal expectancy, this weighting factor should be a variable base on estimate and varies across matches. 

Reference:
Dixon M.J. AND Robinson M.E.(1996) A birth process model for association football scores
Dixon M.J. AND S.G.Coles(1997) Modelling association football scores and inefficiencies in the football market 
Daan van Gemert(2010) Modelling the score of premier league football matches
Maher M.J.(1982) Modelling association football scores 

Given that
Home Team goal expectancy = 1.55
Away Team goal expectancy = 1.00
Margin = 0.04
 
The Generated Odds are :
 
| Home | Away | Draw | (With Margin)
| 1.88 | 3.46 | 3.35 |
 
| Home | Away | Draw | First Half HAD (With Margin)
| 2.65 | 4.23 | 1.97 |
 
| Line | High | Low  | HiLo (With Margin)
| 2.5  | 2.06 | 1.68 |