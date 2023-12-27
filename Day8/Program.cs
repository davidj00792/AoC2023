using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

string textFile = "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day8\\Data.txt";
List<string> lines;

// Read a text file line by line.
if (File.Exists(textFile)) {
    lines = File.ReadAllLines(textFile).ToList();
} else {
    return;
}  

string instructions = lines.FirstOrDefault();

lines.RemoveRange(0, 2);
Regex pattern = new Regex(@"\b[A-Za-z]{3}\b");
Dictionary<string,Node> nodesList = new Dictionary<string, Node>();

if (instructions == null) {
    return;
}

int stepsForPart1 = GetSteps1(instructions, nodesList);
Console.WriteLine("Number of steps is: " + stepsForPart1);
//20777
double stepsForPart2 = GetSteps2(instructions, nodesList);
Console.WriteLine("Number of steps is: " + stepsForPart2);
//13289612809129

int GetSteps1(string instructions, Dictionary<string, Node> nodesList)
{
    foreach (string line in lines) {
    MatchCollection nodes = pattern.Matches(line);
    nodesList[nodes[0].Value] = new Node(
                                    nodes[0].Value,
                                    nodes[1].Value,
                                    nodes[2].Value);
    }

    Node currentNode = nodesList["AAA"];
    int steps = 0;

    for (int i = 0; i < instructions.Length; i++) {
        var instruction = instructions[i];

        if (instruction == 'L') {
            currentNode = nodesList[currentNode.NodeLeft];
        } else if (instruction == 'R') {
            currentNode = nodesList[currentNode.NodeRight];
        }

        steps++;

        if (currentNode.NodeName == "ZZZ") {
            break;
        }

        if (i == instructions.Length-1) {
            i = -1;
        }
    }

    return steps;
}

double GetSteps2(string instructions, Dictionary<string, Node> nodesList)
{
    List<Node> currentNodes = new List<Node>();
    List<int> stepsForEachNode = new List<int>();
    int steps = 0;

    foreach (string line in lines) {
        MatchCollection nodes = pattern.Matches(line);
        Node node = new Node(nodes[0].Value, nodes[1].Value, nodes[2].Value);
        nodesList[nodes[0].Value] = node;
        if (nodes[0].Value.EndsWith("A")) {
            currentNodes.Add(node);
        }
    }

    for (int j = 0; j < currentNodes.Count; j++){
        Node mainNode = currentNodes[j];

        for (int i = 0; i < instructions.Length; i++) {
            var instruction = instructions[i];

            if (instruction == 'L') {
                mainNode = nodesList[mainNode.NodeLeft];
            } else if (instruction == 'R') {
                mainNode = nodesList[mainNode.NodeRight];
            }

            steps++;

            if (mainNode.NodeName.EndsWith("Z")) {
                break;
            }
            
            if (i == instructions.Length-1) {
                i = -1;
            }
        }

        stepsForEachNode.Add(steps);
        steps = 0;
    }

    return FindLowestCommonNumber(stepsForEachNode[0],stepsForEachNode[1],stepsForEachNode[2],stepsForEachNode[3],stepsForEachNode[4],stepsForEachNode[5]);
}

static double FindLowestCommonNumber(params int[] numbers) {
    double commonNumber = numbers[0];

    for (int i = 1; i < numbers.Length; i++) {
        commonNumber = FindLCM(commonNumber, numbers[i]);
    }

    return commonNumber;
}

static double FindLCM(double a, double b) {
    return (a * b) / FindBiggestCommonDivisor(a, b);
}

static double FindBiggestCommonDivisor(double a, double b) {
    while (b != 0) {
        double temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}


public class Node {
    public string NodeName {get;set;}
    public string NodeLeft {get;set;}
    public string NodeRight {get;set;}

    public Node(string node, string left, string right)
    {
        NodeName = node;
        NodeLeft = left;
        NodeRight = right;
    }
}