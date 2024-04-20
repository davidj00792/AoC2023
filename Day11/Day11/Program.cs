using System.Collections;
using System.Collections.Generic;
using System.Text;

string textFile = "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day11\\Day11\\Data.txt";
List<string> lines;

// Read a text file line by line.
if (File.Exists(textFile)) {
    lines = File.ReadAllLines(textFile).ToList();
} else {
    return;
}  

Expand(lines);

var galaxy = lines;


foreach (string row in galaxy)
    {
        Console.WriteLine(row);
    }


var totalDistances = Count(galaxy);

Console.WriteLine(totalDistances);

//Expand the galaxy
static void Expand(List<string> map) {
    var columns = map[0].Length;

    //expand rows
    for (int i = 0; i < map.Count; i++) {
        var isEmpty = true;
        foreach (var symbol in map[i]) {
            if (symbol != '.') {
                isEmpty = false;
                break;
            }
        }
        if (isEmpty) {
            StringBuilder newRow = new StringBuilder();
            for (int j = 0; j < columns; j++)
            {
                newRow.Append(".");
            }
            map.Insert(i, newRow.ToString());
            i++;
        }
    }

    var rows = map.Count;

    //expand columns
    for (int i = 0; i < map[0].Length; i++) { 
        var isEmpty = true;
        for (int j = 0; j < rows; j++) {
            if (map[j][i] != '.') {
                isEmpty = false;
                break;
            }
        }
        if (isEmpty) {
            for (int k = 0; k < rows; k++) {
                StringBuilder rowBuilder = new StringBuilder(map[k]);
                rowBuilder.Insert(i, ".");
                map[k] = rowBuilder.ToString();
            }
            i++;
        }
    }
}

//Count distances
static int Count(List<string> map) {
    var columns = map[0].Length;
    var rows = map.Count;
    var stars = new List<(int, int)>();

    for (int i = 0; i < rows; i++) {
        for (int j = 0; j < columns; j++) {
            if ((map[i][j]) != '.') {
                stars.Add((j, i));
            }
        }
    }

    var countStars = stars.Count;
    var distance = 0;

    for(int i = 0; i < countStars; i++) {
        for(int j = i+1; j < countStars; j++) {
            distance += Math.Abs(stars[i].Item1 - stars[j].Item1) + Math.Abs(stars[i].Item2 - stars[j].Item2);
        }
    }

    return distance;
}