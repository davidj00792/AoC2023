using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

//string rootFolder =  "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day1";
string textFile = "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day1\\Data.txt";
int totalValue = 0;

if (File.Exists(textFile)) {
    // Read a text file line by line.
    string[] lines = File.ReadAllLines(textFile);

    // Go line by line and every time initialize new number
    foreach(string line in lines) {
        string number = "";
     
        // for every letter in line, check if it is number if yes - add it to number string, if the number would be 3rd - replace 2nd number
        /* Part 1
        foreach(char letter in line) {         
            if (char.IsNumber(letter) && number.Length < 2) {
                number += letter;
            } else if (char.IsNumber(letter) && number.Length == 2) {
                number = number.Remove(1) + letter;
            }
        }
        //Handle 1 number lines
        if (number.Length == 1) {
            number += number;
        }
        */

        number = FindNumbersInString(line);   
        Console.WriteLine(number);
        totalValue += int.Parse(number);
    }
}

//Result
Console.WriteLine(totalValue);
//57325 wrong

//Functions
static string FindNumbersInString(string input)
{
    List<string> numbers = new List<string>();
    // Define the regex pattern
    string pattern = @"(?:one|two|three|four|five|six|seven|eight|nine|1|2|3|4|5|6|7|8|9)";

    // Create a regex object
    Regex regexLeft = new Regex(pattern, RegexOptions.IgnoreCase);
    Regex regexRight = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.RightToLeft);

    // Find all matches
   string leftNumber = regexLeft.Match(input).Value.Length == 1 ? regexLeft.Match(input).Value : ReplaceWordsWithNumbers(regexLeft.Match(input).Value);
   string rightNumber = regexRight.Match(input).Value.Length == 1 ? regexRight.Match(input).Value : ReplaceWordsWithNumbers(regexRight.Match(input).Value);

    // Print the matche
    return leftNumber+rightNumber;
}


static string ReplaceWordsWithNumbers(string input)
{
   Dictionary<string, string> wordNumberMap = new Dictionary<string, string>
        {
            {"one", "1"},
            {"two", "2"},
            {"three", "3"},
            {"four", "4"},
            {"five", "5"},
            {"six", "6"},
            {"seven", "7"},
            {"eight", "8"},
            {"nine", "9"}
        };

    // Construct the regular expression pattern for matching the words
    string pattern = @"\b(" + string.Join("|", wordNumberMap.Keys) + @")\b";

    // Use Regex.Replace to replace matched words with their numeric values
    string result = Regex.Replace(input, pattern, match => wordNumberMap[match.Value.ToLower()]);

    return result;
}


/**************************************************************************************************************
Documentation on reading txt file


namespace ReadATextFile {
 class Program {
  // Default folder
  static readonly string rootFolder =  "C:\Users\xjanc\OneDrive\Plocha\AdventOfCode2023\Day1";
  //Default file. MAKE SURE TO CHANGE THIS LOCATION AND FILE PATH TO YOUR FILE
  static readonly string textFile = "C:\Users\xjanc\OneDrive\Plocha\AdventOfCode2023\Day1\Data.txt";

  static void Main(string[] args) {
   
   if (File.Exists(textFile)) {
    // Read entire text file content in one string
    string text = File.ReadAllText(textFile);
    Console.WriteLine(text);
   }
    

   if (File.Exists(textFile)) {
    // Read a text file line by line.
    string[] lines = File.ReadAllLines(textFile);
    //foreach(string line in lines)
    Console.WriteLine(lines[0]);
   }

    
   if (File.Exists(textFile)) {
    // Read file using StreamReader. Reads file line by line
    using(StreamReader file = new StreamReader(textFile)) {
     int counter = 0;
     string ln;

     while ((ln = file.ReadLine()) != null) {
      Console.WriteLine(ln);
      counter++;
     }
     file.Close();
     Console.WriteLine($ "File has {counter} lines.");
    }
   }
    
   Console.ReadKey();
  }
 }
}
*/