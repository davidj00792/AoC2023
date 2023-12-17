using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;

string textFile = "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day5\\Data.txt";
List<double> locationNumbers = new List<double>();
List<string> lines = new List<string>();

if (File.Exists(textFile)) {
    // Read a text file line by line.
    lines = File.ReadAllLines(textFile).ToList();
} else {
    return;
}  

string pattern = @"(\d+)";

//Get seeds
MatchCollection seedsColl = Regex.Matches(lines[0], pattern);

//Seeds for part 1
List<int> seeds = GetNumbers(seedsColl);
//Seeds for part 2
List<Seed> seeds2 = GetNewSeeds(seeds);

List<Section> sections = new List<Section>();

//Get lines for each section
string name = "";
List<string> maps = new List<string>();
int index = 0;
foreach (string line in lines) {
    if (line != "" && line[line.Length-1] == ':') {
        name = line;
    }
    if (line != "" && char.IsDigit(line[0])) {
        maps.Add(line);
    }
    if (index == lines.Count -1 || (line == "" && name != "")) {
        Section section = new Section(name,maps);
        sections.Add(section);
        name = "";
        maps = new List<string>();
    }
    index++;
}

foreach (int seed in seeds) {
    //get corresponding value and add it to locationNumbers
    double positon = seed;
    foreach (Section section in sections) {
        double location = section.GetLocation(positon);
        positon = location;
        if (section.Name == "humidity-to-location map:") {
            locationNumbers.Add(location);
        }
    }
}


//implement new part to get lowest number for each seed

//Results
Console.WriteLine(locationNumbers.Min());


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

List<Seed> GetNewSeeds(List<int> seeds)
{
    List<Seed> newSeeds = new List<Seed>();
    for (int i = 0; i < seeds.Count/2; i+=2) {
        Seed seed = new Seed(seeds[i], seeds[i+1]);
        newSeeds.Add(seed);
    }
    return newSeeds;
}



//Classes
public class Section {
    public string Name {get;set;}
    public List<string> Lines {get;set;}

    public Section(string name, List<string> lines) {
        Name = name;
        Lines = lines;
    }


    internal double GetLocation(double positon)
    {
        string pattern = @"(\d+)";
        double location = positon;
        foreach (string line in Lines) {
            MatchCollection locationsColl = Regex.Matches(line, pattern);
            List<double> locations = GetNumbers(locationsColl);

            if (positon >= locations[1] && positon < locations[1]+locations[2]) {
                location = locations[0] + (positon - locations[1]);
            } 
        }
        return location;
    }

    internal List<double> GetNumbers(MatchCollection collection) {
        List<double> numbers = new List<double>();
        foreach (Match match in collection) {
                numbers.Add(double.Parse(match.Value.Trim()));
        }
        return numbers;
    }
}

public class Seed {
    public int Number {get;set;}
    public int Range {get;set;}

    public Seed(int number, int range) {
        Number = number;
        Range = range;
    }
}



