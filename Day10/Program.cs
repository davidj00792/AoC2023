using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

string textFile = "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day10\\Test.txt";
List<string> lines;

// Read a text file line by line.
if (File.Exists(textFile)) {
    lines = File.ReadAllLines(textFile).ToList();
} else {
    return;
}  

//Convert list to 2D char array
char[,] floorField = ConvertStringArrayTo2DCharArray(lines);

int lineLenght = lines[0].Length;
int linesCount = lines.Count;

//Get coordinates of start point
Point start = new Point(0,0,'S');
for (int i = 0; i < linesCount; i++) {
     if (lines[i].Contains("S")) {
        start.X = lines[i].IndexOf("S");
        start.Y = i;
     }
}

//Find first pipe
Point point = FindFirstPoint(start.X,start.Y);

//Define checkpoint
Point lastPoint = new Point (start.X,start.Y,'P');

int counter = 0;

while (point.Pipe != 'S') {
    int leftOrRight = point.X - lastPoint.X; // 1 -> goRight, -1 -> goLeft
    int upOrDown = point.Y - lastPoint.Y; // 1 -> goDown, -1 -> goUp

    int x = point.X;
    int y = point.Y;

    lastPoint.X = point.X;
    lastPoint.Y = point.Y;

    //Finish conditions
    if (x > 0 && point.Pipe == '-' && leftOrRight == -1) {
         point = new Point(x-1,y,floorField[y,x-1]);
         floorField[y,x-1] = '1';
    } else if (x < lineLenght-1 && point.Pipe == '-' && leftOrRight == 1) {
         point = new Point(x+1,y,floorField[y,x+1]);
         floorField[y,x+1] = '1';
    } else if (y > 0 && point.Pipe == '|' && upOrDown == -1) {
         point = new Point(x,y-1,floorField[y-1,x]);
         floorField[y-1,x] = '1';
    } else if (y < linesCount-1 && point.Pipe == '|' && upOrDown == 1) {
         point = new Point(x,y+1,floorField[y+1,x]);
         floorField[y+1,x] = '1';
    } else if (y < linesCount-1 && point.Pipe == 'F' && leftOrRight == -1) {
         point = new Point(x,y+1,floorField[y+1,x]);
         floorField[y+1,x] = '1';
    } else if (x < lineLenght-1 && point.Pipe == 'F' && upOrDown == -1) {
         point = new Point(x+1,y,floorField[y,x+1]);
         floorField[y,x+1] = '1';
    } else if (y > 0 && point.Pipe == 'L' && leftOrRight == -1) {
         point = new Point(x,y-1,floorField[y-1,x]);
         floorField[y-1,x] = '1';
    } else if (x < lineLenght-1 && point.Pipe == 'L' && upOrDown == 1) {
         point = new Point(x+1,y,floorField[y,x+1]);
         floorField[y,x+1] = '1';
    } else if (y < linesCount-1 && point.Pipe == '7' && leftOrRight == 1) {
         point = new Point(x,y+1,floorField[y+1,x]);
         floorField[y+1,x] = '1';
    } else if (x > 0 && point.Pipe == '7' && upOrDown == -1) {
         point = new Point(x-1,y,floorField[y,x-1]);
         floorField[y,x-1] = '1';
    } else if (y > 0 && point.Pipe == 'J' && leftOrRight == 1) {
         point = new Point(x,y-1,floorField[y-1,x]);
         floorField[y-1,x] = '1';
    } else if (x > 0 && point.Pipe == 'J' && upOrDown == 1) {
         point = new Point(x-1,y,floorField[y,x-1]);
         floorField[y,x-1] = '1';
    }
    floorField[lastPoint.Y,lastPoint.X] = '1';
    counter++;
}

floorField[start.Y,start.X] = 'S';

int steps = 0;
if (counter % 2 != 0) {
    steps = (counter - 1) / 2 + 1;
} else {
    steps = counter / 2;
}

Console.WriteLine(steps);



//Start again , find first 1 
if (floorField[start.X+1,start.Y] == '1') {
    point = new Point(start.X+1,start.Y,'1');
} else if (floorField[start.X,start.Y-1] == '1') {
    point = new Point(start.X,start.Y-1,'1');
} else if (floorField[start.X-1,start.Y] == '1') {
    point = new Point(start.X-1,start.Y,'1');
}

//update last point
lastPoint.X = start.X;
lastPoint.Y = start.Y;

//  LP

//start flood filling
while (point.Pipe != 'S') {
    /*
    int x = point.X;
    int y = point.Y;
    if (x-lastPoint.X == 1) {
        if (floorField)
    } else if (y-lastPoint.Y == -1) {

    } else if (x - lastPoint.X == -1) {

    } else {

    }
    lastPoint.X = x;
    lastPoint.Y = y;
    */

    //pokus
    int leftOrRight = point.X - lastPoint.X; // 1 -> goRight, -1 -> goLeft
    int upOrDown = point.Y - lastPoint.Y; // 1 -> goDown, -1 -> goUp

    int x = point.X;
    int y = point.Y;

    lastPoint.X = point.X;
    lastPoint.Y = point.Y;

    //Finish conditions

}

static void FloodFill (int x, int y, char[,] array) {
    if (array[x,y] == '1' || array[x,y] == '2' || x < 0 || y < 0 || y >= array.GetLength(1) || x >= array.GetLength(0)) {
        return;
    }

    array[x,y] = '2';
    FloodFill(x+1,y,array);
    FloodFill(x-1,y,array);
    FloodFill(x,y+1,array);
    FloodFill(x,y-1,array);
}




Console.WriteLine($"Number of symbols inside the closed loop: {steps}");


//Functions
static char[,] ConvertStringArrayTo2DCharArray(List<string> lines) {
    int numRows = lines.Count;
    int rowLenght = lines[0].Length;

    // Create a 2D array to hold the characters
    char[,] char2DArray = new char[numRows, rowLenght];

    // Populate the 2D array
    for (int i = 0; i < numRows; i++)
    {
        string str = lines[i];

        for (int j = 0; j < str.Length; j++)
        {
            char2DArray[i, j] = str[j];
        }
    }

    return char2DArray;
}

Point FindFirstPoint(int x, int y){
    if (x > 0 && CheckLeft(x-1,y)) {
        return new Point(x-1,y,lines[y][x-1]);
    } else if (x < lines[y].Length-1 && CheckRight(x+1,y)) {
        return new Point(x+1,y,lines[y][x+1]);
    } else if (y > 0 && CheckUp(x,y-1)) {
        return new Point(x,y-1,lines[y-1][x]);
    } else if (y < lines.Count-1 && CheckDown(x,y+1)) {
        return new Point(x,y+1,lines[y+1][x]);
    }
    throw new Exception("No following pipe");
    ;
}




bool CheckLeft(int x, int y)
{
    List<char> connectedPipes = new List<char>(){'-','L','F'};
    return connectedPipes.Contains(lines[y][x]);
}

bool CheckRight(int x, int y)
{
    List<char> connectedPipes = new List<char>(){'-','J','7'};
    return connectedPipes.Contains(lines[y][x]);
}

bool CheckUp(int x, int y)
{
    List<char> connectedPipes = new List<char>(){'|','7','F'};
    return connectedPipes.Contains(lines[y][x]);
}

bool CheckDown(int x, int y)
{
    List<char> connectedPipes = new List<char>(){'|','L','J'};
    return connectedPipes.Contains(lines[y][x]);
}


public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Pipe { get; set; }

    public Point(int x, int y, char pipe)
    {
        X = x;
        Y = y;
        Pipe = pipe;
    }
}

/*
| is a vertical pipe connecting north and south.
- is a horizontal pipe connecting east and west.
L is a 90-degree bend connecting north and east.
J is a 90-degree bend connecting north and west.
7 is a 90-degree bend connecting south and west.
F is a 90-degree bend connecting south and east.
. is ground; there is no pipe in this tile.
*/


