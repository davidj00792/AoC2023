using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Linq;

string textFile = "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day3\\Data.txt";
int totalValue = 0;
int totalValueGears = 0;

if (File.Exists(textFile)) {
    // Read a text file line by line.
    string[] lines = File.ReadAllLines(textFile);
    Dictionary<(int x, int y), string> coordinates = new Dictionary<(int x, int y), string>();
    int positionY = 0;
    string number = "";
    bool nextToSymbol = false;
    bool lastNumber = false;

    foreach(string line in lines) {
        int positionX = 0;


        foreach(char letter in line) {
            //for part 2
            if (letter == '*') { 
                coordinates.Add((positionX,positionY),letter.ToString());
            }
        
            //if it is number, check if it is next to symbol and if it is last number of numebr sequence and add to final number
            if (char.IsDigit(letter)) {
                if (!nextToSymbol) {nextToSymbol = CheckSurroundings(positionX,positionY, lines);}
                
                lastNumber = CheckLastNumber(positionX, line);
                number += letter;
            }

            //if the number was last and was next to symbol, add it to total value and reset conditions and number
            if (char.IsDigit(letter) &&lastNumber && nextToSymbol) {
                totalValue += int.Parse(number);
                lastNumber = false;
                nextToSymbol = false;
                
                number = "";
            }

            if (char.IsDigit(letter) && lastNumber) {
                lastNumber = false;
                nextToSymbol = false;
                number = "";
            }

            positionX += 1;
        }
        positionY += 1;
    }

    //part 2
    totalValueGears = Calculate(lines, coordinates);
}

//Results
Console.WriteLine(totalValue);
Console.WriteLine(totalValueGears);


//Functions part 1
static bool CheckSurroundings(int x, int y, string[] schematic) {
    //first line
    if (y == 0) {
        if (x == 0) {
            if (IsSymbol(schematic[y+1][x]) || IsSymbol(schematic[y][x+1]) || IsSymbol(schematic[y+1][x+1])) {
                return true;
            }
        }
        else if (x == schematic[y].Length-1) {
            if (IsSymbol(schematic[y+1][x]) || IsSymbol(schematic[y][x-1]) || IsSymbol(schematic[y-1][x-1])) {
                return true;
            }
        } else {
            if (IsSymbol(schematic[y+1][x]) || IsSymbol(schematic[y][x-1]) || IsSymbol(schematic[y][x+1]) || IsSymbol(schematic[y+1][x+1]) || IsSymbol(schematic[y+1][x-1])) {
                return true;
            }
        }
    } 
    //last line
    else if (y == schematic.Length-1){
        if (x == 0) {
            if (IsSymbol(schematic[y][x+1]) || IsSymbol(schematic[y-1][x]) || IsSymbol(schematic[y-1][x+1])) {
                return true;
            }
        }
        else if (x == schematic[y].Length-1) {
            if (IsSymbol(schematic[y-1][x]) || IsSymbol(schematic[y][x-1]) || IsSymbol(schematic[y-1][x-1])) {
                return true;
            }
        } else {
            if (IsSymbol(schematic[y-1][x]) || IsSymbol(schematic[y][x-1]) || IsSymbol(schematic[y][x+1]) || IsSymbol(schematic[y-1][x+1]) || IsSymbol(schematic[y-1][x-1])) {
                return true;
            }
        }
    }
    //first character
    else if (x == 0) {
        if (IsSymbol(schematic[y+1][x]) || IsSymbol(schematic[y-1][x]) || IsSymbol(schematic[y][x+1]) || IsSymbol(schematic[y-1][x+1]) || IsSymbol(schematic[y+1][x+1])) {
            return true;
        } 
    }
    //last charter
    else if (x == schematic[y].Length-1) {
        if (IsSymbol(schematic[y+1][x]) || IsSymbol(schematic[y-1][x]) || IsSymbol(schematic[y][x-1]) || IsSymbol(schematic[y-1][x-1]) || IsSymbol(schematic[y+1][x-1])) {
            return true;
        } 
    } else {
        if (IsSymbol(schematic[y+1][x]) || IsSymbol(schematic[y-1][x]) || IsSymbol(schematic[y][x-1]) || IsSymbol(schematic[y][x+1]) || IsSymbol(schematic[y-1][x-1]) || IsSymbol(schematic[y+1][x-1]) || IsSymbol(schematic[y+1][x+1]) || IsSymbol(schematic[y-1][x+1])) {
            return true;
        } 
    }
    return false;
}

static bool CheckLastNumber(int x, string line) {
    if (x == line.Length-1) { 
        return true;
    } else if (!char.IsDigit(line[x+1])) {
        return true;
    }
    return false;
}

static bool IsSymbol(char symbol) {
    return !(char.IsDigit(symbol) || symbol == '.');
}

//Functions part 2
static int Calculate(string[] lines, Dictionary<(int x, int y), string> coordinates) {
    int value = 0;
    foreach (var symbol in coordinates) {
        List<int> numbers = GetAdjentNumbers(symbol.Key.x, symbol.Key.y, lines);
        if (numbers.Count() == 2) {
            value += numbers[0] * numbers[1];
        }
    }
    return value;
}

static List<int> GetAdjentNumbers(int x, int y, string[] lines) {
    List<int> adjentNumbers = new List<int>();

    if (char.IsDigit(lines[y][x+1])){
        adjentNumbers.Add(ReadNumberRight(lines[y], x+1));
    }
    if (char.IsDigit(lines[y][x-1])){
        adjentNumbers.Add(ReadNumberLeft(lines[y], x-1));
    }
    if (char.IsDigit(lines[y+1][x])){
        adjentNumbers.Add(ReadNumberMiddle(lines[y+1], x));
    }
    if (char.IsDigit(lines[y-1][x])){
        adjentNumbers.Add(ReadNumberMiddle(lines[y-1], x));
    }
    if (char.IsDigit(lines[y-1][x-1]) && !char.IsDigit(lines[y-1][x])) {
        adjentNumbers.Add(ReadNumberLeft(lines[y-1], x-1));
    }
    if (char.IsDigit(lines[y-1][x+1]) && !char.IsDigit(lines[y-1][x])) {
        adjentNumbers.Add(ReadNumberRight(lines[y-1], x+1));
    }
    if (char.IsDigit(lines[y+1][x-1]) && !char.IsDigit(lines[y+1][x])) {
        adjentNumbers.Add(ReadNumberLeft(lines[y+1], x-1));
    }
    if (char.IsDigit(lines[y+1][x+1]) && !char.IsDigit(lines[y+1][x])) {
        adjentNumbers.Add(ReadNumberRight(lines[y+1], x+1));
    }

    return adjentNumbers;
}

static int ReadNumberRight(string line, int x) {
    string number = "";
    int i = 0;
    while (char.IsDigit(line[x+i])) {
        number += line[x+i];
        i++;
        if (x + i == line.Length) break;
    }
    return int.Parse(number);
}

static int ReadNumberLeft(string line, int x) {
    string number = "";
    int i = 0;
    while (char.IsDigit(line[x-i])) {
        number += line[x-i];
        i++;
        if (x - i < 0) break;
    }
    number = new string(number.Reverse().ToArray());
    return int.Parse(number);
}

static int ReadNumberMiddle(string line, int x) {
    string number = "";
    int i = 0;
    while (char.IsDigit(line[x-i])) {
        number += line[x-i];
        i++;
        if (x - i < 0) break;
    }
    number = new string(number.Reverse().ToArray());
    i = 1;
    while (char.IsDigit(line[x+i])) {
        number += line[x+i];
        i++;
        if (x + i > line.Length) break;
    }
    return int.Parse(number);
}