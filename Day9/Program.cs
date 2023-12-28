using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

string textFile = "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day9\\Data.txt";
List<string> lines;

// Read a text file line by line.
if (File.Exists(textFile)) {
    lines = File.ReadAllLines(textFile).ToList();
} else {
    return;
}  

int totalSum = 0;

foreach (string line in lines) {
    string[] numbers = line.Split(' ');
    List<int> numberValues = new List<int>();
    foreach (string num in numbers) {
        numberValues.Add(int.Parse(num));
    }

    //Part1
    //int extrapolatedValue = Extrapolate(nums);

    //Part2
    int extrapolatedValue = ExtrapolateBackwards(numberValues);

    totalSum += extrapolatedValue;
}

Console.WriteLine(totalSum);



int Extrapolate(List<int> nums)
{
    int maxRows = nums.Count;
    List<int> lastNumbers = new List<int>(){nums.Last()};
    
    //For each line of numbers, get last number
    for (int i = 0; i < maxRows; i++) {
        bool allZero = true;
        List<int> newLine = new List<int>();

        for (int j = 1; j < nums.Count; j++) {
            int diff = nums[j] - nums[j-1];
            newLine.Add(diff);
            
            if (diff != 0) {
                allZero = false;
            }
        }

        lastNumbers.Add(newLine.Last()); 

        if (allZero) {
            break;
        }

        nums = newLine;
    }

    //reverse numbers
    lastNumbers.Reverse();
    
    int newLastNumber = 0;

    //extrapolate to get last new number in first original row
    for (int i = 1; i < lastNumbers.Count; i++) {
        newLastNumber = newLastNumber + lastNumbers[i];
    }

    return newLastNumber;
}


int ExtrapolateBackwards(List<int> nums)
{
    int maxRows = nums.Count;
    List<int> firstNumbers = new List<int>(){nums.First()};
    
    //For each line of numbers, get last number
    for (int i = 0; i < maxRows; i++) {
        bool allZero = true;
        List<int> newLine = new List<int>();

        for (int j = 1; j < nums.Count; j++) {
            int diff = nums[j] - nums[j-1];
            newLine.Add(diff);
            
            if (diff != 0) {
                allZero = false;
            }
        }

        firstNumbers.Add(newLine.First()); 

        if (allZero) {
            break;
        }

        nums = newLine;
    }

    //reverse numbers
    firstNumbers.Reverse();
    
    int newFirstNumber = 0;

    //extrapolate to get first new number in first original row
    for (int i = 1; i < firstNumbers.Count; i++) {
        newFirstNumber = firstNumbers[i] - newFirstNumber;
    }

    return newFirstNumber;
}