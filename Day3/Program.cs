using System.Collections;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;


string textFile = "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day3\\Data.txt";
int totalValue = 0;

if (File.Exists(textFile)) {
    // Read a text file line by line.
    string[] lines = File.ReadAllLines(textFile);
    //Dictionary<(int x, int y), string> coordinates = new Dictionary<(int x, int y), string>();
    int positionY = 0;
    string number = "";
    bool nextToSymbol = false;
    bool lastNumber = false;

    foreach(string line in lines) {
        int positionX = 0;


        foreach(char letter in line) {
            //if it is number, check if it is next to symbol and if it is last number of numebr sequence and add to final number
            if (char.IsDigit(letter)) {
                //coordinates.Add((positionY,positionX),letter.ToString());
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

    /*
    foreach (var coor in coordinates) {
        Console.WriteLine($"Coordinate: ({coor.Key.x}, {coor.Key.y}), Value: {coor.Value}");
    }
    */

}

//Result
Console.WriteLine(totalValue);


//Functions
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