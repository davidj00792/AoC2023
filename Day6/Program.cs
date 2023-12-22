using System.Collections;
using System.Text.RegularExpressions;

string textFile = "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day6\\Data.txt";
List<string> lines;

// Read a text file line by line.
if (File.Exists(textFile)) {
    lines = File.ReadAllLines(textFile).ToList();
} else {
    return;
}  

// Get values from input
string pattern = @"(\d+)";
MatchCollection timesColl = Regex.Matches(lines[0], pattern);
MatchCollection distancesColl = Regex.Matches(lines[1], pattern);

//Times
List<int> times = GetNumbers(timesColl);
//Distances
List<int> distances = GetNumbers(distancesColl);


/*********************** Part 1 ***********************
List<int> wins = new List<int>();
//for each time get number of winning cases
for (int i = 0; i < times.Count(); i++) {
    int winCount = CalculateWins(times[i],distances[i]);
    wins.Add(winCount);
}

int resultOfPart1 = 1;
foreach (int travelFurther in wins){
    resultOfPart1 *= travelFurther;
}
Console.WriteLine(resultOfPart1);
*/
/*********************************************************/

/********************* Part 2 **************************/
string joinedTimes = string.Join("", times);
string joinedDistances = string.Join("", distances);

double time = double.Parse(joinedTimes);
double distance = double.Parse(joinedDistances);

double resultOfPart2 = CalculateWinsBig(time, distance);
Console.WriteLine(resultOfPart2);
/*********************************************************/


//Functions 
static List<int> GetNumbers(MatchCollection collection) {
    List<int> numbers = new List<int>();
    foreach (Match match in collection) {
        if (int.TryParse(match.Value.Trim(), out int number))
        {
            numbers.Add(number);
        }
    }
    return numbers;
}

//For each time calculate traveled distance, if it is greater than record, count it and return sum of beaten records
int CalculateWins(int time, int distance)
{
    int wins = 0;
    for (int i = 0; i < time; i++) {
        if ( i * (time-i) > distance ) {
            wins++;
        }
    }
    return wins;
}

//For each time calculate traveled distance, if it is greater than record, count it and return sum of beaten records
//When traveled distance is same or lower than previous, multiply sum of beaten records *2 and if time is even subtract 1
double CalculateWinsBig(double time, double distance) {
    double wins = 0;
    bool timeIsEven = time % 2 == 0;
    double previouslyTraveled = 0;
    
    for (int i = 1; i <= time; i++) {
        double traveled = i * (time-i);
        if (traveled <= previouslyTraveled) {
            break;
        } 
        if ( i * (time-i) > distance ) {
            wins++;
        }
        previouslyTraveled = traveled;
    }

    wins *= 2;

    return timeIsEven ? wins-1 : wins;
}
