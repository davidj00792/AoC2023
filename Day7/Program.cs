using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

string textFile = "C:\\Users\\xjanc\\OneDrive\\Plocha\\AdventOfCode2023\\Day7\\Data.txt";
List<string> lines;

// Read a text file line by line.
if (File.Exists(textFile)) {
    lines = File.ReadAllLines(textFile).ToList();
} else {
    return;
}  

var combinationStrenght = new Dictionary<string, int> {
        { "High card", 1 },
        { "Pair", 2 },
        { "Two pairs", 3 },
        { "Three cards", 4 },
        { "Full house", 5 },
        { "Four cards", 6 },
        { "Five cards", 7 }
};


List<HandForBasicGame> handsForBasic = new List<HandForBasicGame>();

foreach (string line in lines) {
    string[] parts = line.Split(' ');
    handsForBasic.Add(new HandForBasicGame(parts[0], int.Parse(parts[1])));
}

var handsForBasicByStrenght = handsForBasic.GroupBy(x => x.Combination);

var sortedHandsByStrenght = handsForBasic.OrderBy(hand => combinationStrenght[hand.Combination]).ThenBy(hand => hand.Strenght).ToList();

int rank = 1;
int totalWinnings = 0;

foreach (HandForBasicGame hand in sortedHandsByStrenght) {
    hand.Rank = rank;
    hand.CalculateWinnings();
    totalWinnings += hand.Winnings;
    rank++;
}

foreach (HandForBasicGame hand in sortedHandsByStrenght) {
    Console.WriteLine(hand.Cards + " is " + hand.Combination + " and has Rank " + hand.Rank + ", Strenght " + hand.Strenght + ", Rating " + hand.Rating + " and score " + hand.Winnings);
}

Console.WriteLine("Total winnings is: " + totalWinnings);


//Class



public class HandForBasicGame {
    public string Cards {get;set;}
    public int Rating {get;set;}
    public string? Combination {get;set;}
    public int Strenght {get;set;}
    public int Rank {get;set;}
    public int Winnings;
    public HandForBasicGame(string cards, int rating)
    {
        Cards = cards;
        Rating = rating;
        GetCombinationAndStrenght(cards);
    }

    private void GetCombinationAndStrenght(string cards)
    {
        var groupedCards = cards.GroupBy(c => c);

        if (groupedCards.Any(group => group.Count() == 5)) {
            Combination = "Five cards";
            Strenght = CalculateStrenght(cards);
            return;
        }

        if (groupedCards.Any(group => group.Count() == 4)) {
            Combination = "Four cards";
            Strenght = CalculateStrenght(cards);
            return;
        }

        if (groupedCards.Any(group => group.Count() == 3) && groupedCards.Any(group => group.Count() == 2)) {
            Combination = "Full house";
            Strenght = CalculateStrenght(cards);
            return;
        }

        if (groupedCards.Any(group => group.Count() == 3)) {
            Combination = "Three cards";
            Strenght = CalculateStrenght(cards);
            return;
        }

        if (groupedCards.Count(group => group.Count() == 2) == 2) {
            Combination = "Two pairs";
            Strenght = CalculateStrenght(cards);
            return;
        }

        if (groupedCards.Any(group => group.Count() == 2)) {
            Combination = "Pair";
            Strenght = CalculateStrenght(cards);
            return;
        }

        Combination = "High card";
        Strenght = CalculateStrenght(cards);
    }

    private int CalculateStrenght(string cards)
    {
        int i = 3200000; //(20 pow 5)
        return cards.Select(c => {
                i /= 20;
                return GetCardValue(c)*i;
                })
             .Sum();
    }

    static int GetCardValue(char card)
    {
        // Define card values based on your criteria
        string cardValues = "23456789TJQKA";
        return cardValues.IndexOf(card)+1;
    }

    public void CalculateWinnings() {
        Winnings = Rank * Rating;
    }

}





/* This is for regular poker combinations
public class Hand {
    public string Cards {get;set;}
    public int Rating {get;set;}
    public string? Combination {get;set;}
    public int Strenght {get;set;}
    public int Rank {get;set;}
    public int Winnings;
    public Hand(string cards, int rating)
    {
        Cards = cards;
        Rating = rating;
        GetCombinationAndStrenght(cards);
    }

    private void GetCombinationAndStrenght(string cards)
    {
        var groupedCards = cards.GroupBy(c => c);
        int i = 1;

        if (groupedCards.Any(group => group.Count() == 5)) {
            Combination = "Five cards";
            Strenght = GetCardValue(cards[0]);
            return;
        }

        if (groupedCards.Any(group => group.Count() == 4)) {
            Combination = "Four cards";
            var fourCard = groupedCards
                .Where(group => group.Count() == 4)
                .Select(group => group.Key)
                .FirstOrDefault();
            var oneCard = groupedCards.FirstOrDefault(group => group.Count() == 1);
            Strenght = GetCardValue(fourCard)*100 + GetCardValue(oneCard.Key);
            return;
        }

        if (groupedCards.Any(group => group.Count() == 3) && groupedCards.Any(group => group.Count() == 2)) {
            Combination = "Full house";
            var threeCard = groupedCards.FirstOrDefault(group => group.Count() == 3);
            var twoCard = groupedCards.FirstOrDefault(group => group.Count() == 2);
            Strenght = GetCardValue(threeCard.Key)*100 + GetCardValue(twoCard.Key);
            return;
        }

        if (groupedCards.Any(group => group.Count() == 3)) {
            Combination = "Three cards";
            var threeCard = groupedCards.FirstOrDefault(group => group.Count() == 3);
            var otherCards = cards.Distinct().Except(threeCard).OrderBy(card => GetCardValue(card));
            Strenght = otherCards
                .Select(c => {
                    i *= 10; 
                    return GetCardValue(c)*i;
                    })
                .Sum() + GetCardValue(threeCard.Key)*1000;
            return;
        }

        if (groupedCards.Count(group => group.Count() == 2) == 2) {
            Combination = "Two pairs";
            var pairCards = groupedCards.Where(group => group.Count() == 2).Select(group => group.Key).OrderBy(card => GetCardValue(card));
            var oneCard = groupedCards.FirstOrDefault(group => group.Count() == 1);
            Strenght = pairCards
                .Select(c => {
                    i *= 20;
                    return GetCardValue(c)*i;
                    })
                .Sum() + GetCardValue(oneCard.Key);
            return;
        }

        if (groupedCards.Any(group => group.Count() == 2)) {
            Combination = "Pair";
            var twoCard = groupedCards.FirstOrDefault(group => group.Count() == 2);
            var otherCards = cards.Distinct().Except(twoCard).OrderBy(card => GetCardValue(card));
            Strenght = otherCards
                .Select(c => {
                    i *= 10;
                    return GetCardValue(c)*i;
                    })
                .Sum() + GetCardValue(twoCard.Key)*10000;
            return;
        }

        Combination = "High card";
        var orderedCards = cards.OrderBy(card => GetCardValue(card));
        Strenght = orderedCards
            .Select(c => {
                i *= 10;
                return GetCardValue(c)*i;
                })
            .Sum();
    }

    static int GetCardValue(char card)
    {
        // Define card values based on your criteria
        string cardValues = "23456789TJQKA";
        return cardValues.IndexOf(card)+1;
    }

    public void CalculateWinnings() {
        Winnings = Rank * Rating;
    }

}
*/