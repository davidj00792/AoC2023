using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Linq;
using System.ComponentModel.DataAnnotations;

string textFile = "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day4\\Data.txt";
double totalValue = 0;

if (File.Exists(textFile)) {
    // Read a text file line by line.
    string[] lines = File.ReadAllLines(textFile);
    var cards = new List<Card>();


    //each line get lists of winning and card numbers
    foreach (string line in lines) {
        string[] parts = line.Split('|');
        string[] part1 = parts[0].Split(':');
        string pattern = @"(\s\d+)";

        var winningNumbersColl = Regex.Matches(part1[1], pattern);
        var cardNumbersColl = Regex.Matches(parts[1], pattern);

        List<int> winningNumbers = GetNumbers(winningNumbersColl);
        List<int> cardNumbers = GetNumbers(cardNumbersColl);

        int numbersCount = winningNumbers.Intersect(cardNumbers).Count();


        //Part1 -- just add relevant power of 2 to total numebr
        /*
        if (numbersCount >= 1) {
            totalValue += Math.Pow(2, numbersCount-1);
        }
        */


        //Part2 -- create new Card with card number, number of winning numbers and 1 copies (original card)
        var cardNumber = Regex.Match(part1[0], pattern);
        Card card = new(int.Parse(cardNumber.Value),numbersCount,1);
        cards.Add(card);

    }
    
    //Part2 -- continue
    //For each card
    for (int i = 0; i < cards.Count(); i++) {
        //For each copy, add copies to next cards according to winning numbers
        for (int j = i+1; j < i+1+cards[i].WinNumbers; j++) {
            //break if on the end of cards
            if (j >= cards.Count()) {
                break;
            }
            cards[j].Copies += cards[i].Copies;
        }
    }
    
    //Sum up cards
    foreach (var card in cards) {
        totalValue += card.Copies;
    }
    
    /* 
    //Write cards to terminal
    foreach (var card in cards) {
        Console.WriteLine("Card" + card.Number + " has " + card.WinNumbers + " winning numbers and " + card.Copies + " copies.");
    }
    */
}

//Results
Console.WriteLine(totalValue);


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

//Classes
public class Card {
    public int Number {get;set;}
    public int WinNumbers {get;set;}
    public int Copies {get;set;}

    public Card(int numeber, int winNumbers, int copies)
    {
        Number = numeber;
        WinNumbers = winNumbers;
        Copies = copies;
    }
}