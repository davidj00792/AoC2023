using System.Collections;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;


//string rootFolder =  "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day1";
string textFile = "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day2\\Test.txt";
int totalValue = 0;

if (File.Exists(textFile)) {
    // Read a text file line by line.
    string[] lines = File.ReadAllLines(textFile);

    const int maxBlue = 14;
    const int maxRed = 12;
    const int maxGreen = 13;

    string gamePattern = @"(\w+)\s(\d+)";
    string cubePattern = @"(\d+)\s(\w+)";
    // Go line by line and every time initialize new number
    foreach(string line in lines) {
        

        Match game = Regex.Match(line, gamePattern);
        MatchCollection cubes = Regex.Matches(line, cubePattern);

        int blue = 0;
        int green = 0;
        int red = 0;

        /* part 1
        bool possible = true;

        foreach (Match cube in cubes) {
            if (cube.Groups[2].Value == "blue" && int.Parse(cube.Groups[1].Value) > maxBlue) {
                possible = false;
                break;
            } else if (cube.Groups[2].Value == "green" && int.Parse(cube.Groups[1].Value) > maxGreen) {
                possible = false;
                break;
            } else if (cube.Groups[2].Value == "red" && int.Parse(cube.Groups[1].Value) > maxRed) {
                possible = false;
                break;
            } 

            if (blue > maxBlue || green > maxGreen || red > maxRed) {
                possible = false;
                break;
            }
        }


        totalValue += possible ? int.Parse(game.Groups[2].Value) : 0;
        */

        //part 2
        foreach (Match cube in cubes) {
            if (cube.Groups[2].Value == "blue" && int.Parse(cube.Groups[1].Value) > blue) {
                blue = int.Parse(cube.Groups[1].Value);
            } else if (cube.Groups[2].Value == "green" && int.Parse(cube.Groups[1].Value) > green) {
                green = int.Parse(cube.Groups[1].Value);
            } else if (cube.Groups[2].Value == "red" && int.Parse(cube.Groups[1].Value) > red) {
                red = int.Parse(cube.Groups[1].Value);
            } 
        }
        
        totalValue += blue * green * red;
    }
}

//Result
Console.WriteLine(totalValue);


//Functions
