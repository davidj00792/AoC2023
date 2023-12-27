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

/*
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
*/

List<Node> currentNodes = new List<Node>();

foreach (string line in lines) {
    
    MatchCollection nodes = pattern.Matches(line);
    Node node = new Node(nodes[0].Value, nodes[1].Value, nodes[2].Value);
    nodesList[nodes[0].Value] = node;
    if (nodes[0].Value.EndsWith("A")) {
        currentNodes.Add(node);
    }
}

foreach (Node node in currentNodes) {
    Console.WriteLine(node.NodeName);
}
int steps = 0;

for (int i = 0; i < instructions.Length; i++) {
    var instruction = instructions[i];
    bool last = true;

    if (instruction == 'L') {
        for (int j = 0; j < currentNodes.Count(); j++) {
            currentNodes[j] = nodesList[currentNodes[j].NodeLeft];
            if (!currentNodes[j].NodeName.EndsWith("Z")) {
                last = false;
            }
        }
    } else if (instruction == 'R') {
        for (int j = 0; j < currentNodes.Count(); j++) {
            currentNodes[j] = nodesList[currentNodes[j].NodeRight];
            if (!currentNodes[j].NodeName.EndsWith("Z")) {
                last = false;
            }
        }
    }

    steps++;

    if (last) {
        break;
    }
    
    if (i == instructions.Length-1) {
        i = -1;
    }
}


Console.WriteLine("Number of steps is: " + steps);
//20777

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