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

// declare strenght of combinations
var combinationStrenght = new Dictionary<string, int> {
        { "High card", 1 },
        { "Pair", 2 },
        { "Two pairs", 3 },
        { "Three cards", 4 },
        { "Full house", 5 },
        { "Four cards", 6 },
        { "Five cards", 7 }
};

//Result
var totalWinnings = CalculatePart1(lines, combinationStrenght);
//var totalWinnings = CalculatePart2(lines, combinationStrenght);

Console.WriteLine("Total winnings is: " + totalWinnings);






//Functions
static int CalculatePart1(List<string> lines, Dictionary<string, int> combinationStrenght) {
    List<HandForBasicGame> handsForBasic = new List<HandForBasicGame>();

    foreach (string line in lines) {
        string[] parts = line.Split(' ');
        handsForBasic.Add(new HandForBasicGame(parts[0], int.Parse(parts[1])));
    }

    var sortedHandsByStrenght = handsForBasic.OrderBy(hand => combinationStrenght[hand.Combination]).ThenBy(hand => hand.Strenght).ToList();
    int rank = 1;
    int totalWinnings = 0;

    foreach (HandForBasicGame hand in sortedHandsByStrenght) {
        hand.Rank = rank;
        hand.CalculateWinnings();
        totalWinnings += hand.Winnings;
        rank++;
        // Console.WriteLine(hand.Cards + " is " + hand.Combination + " and has Rank " + hand.Rank + ", Strenght " + hand.Strenght + ", Rating " + hand.Rating + " and score " + hand.Winnings);
    }

    return totalWinnings;
}

static int CalculatePart2(List<string> lines, Dictionary<string, int> combinationStrenght) {
    List<HandWithJokers> handsWithJokers = new List<HandWithJokers>();

    foreach (string line in lines) {
        string[] parts = line.Split(' ');
        handsWithJokers.Add(new HandWithJokers(parts[0], int.Parse(parts[1])));
    }

    var sortedHandsByStrenght = handsWithJokers.OrderBy(hand => combinationStrenght[hand.Combination]).ThenBy(hand => hand.Strenght).ToList();
    int rank = 1;
    int totalWinnings = 0;

    foreach (HandWithJokers hand in sortedHandsByStrenght) {
        hand.Rank = rank;
        hand.CalculateWinnings();
        totalWinnings += hand.Winnings;
        rank++;
        // Console.WriteLine(hand.Cards + " is " + hand.Combination + " and has Rank " + hand.Rank + ", Strenght " + hand.Strenght + ", Rating " + hand.Rating + " and score " + hand.Winnings);
    }

    return totalWinnings;
}

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

public class HandWithJokers {
    public string Cards {get;set;}
    public string NewCards {get;set;}
    public int Rating {get;set;}
    public string? Combination {get;set;}
    public int Strenght {get;set;}
    public int Rank {get;set;}
    public int Winnings;
    public HandWithJokers(string cards, int rating)
    {
        Cards = cards;
        Rating = rating;
        GetCombinationAndStrenght(cards);
    }

    private void GetCombinationAndStrenght(string cards)
    {
        
        NewCards = ReplaceJokers(cards);
        var groupedCards = NewCards.GroupBy(c => c);

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

    private string ReplaceJokers(string cards)
    {
        if (cards.Contains('J')) {
            var groupedCards = cards.GroupBy(c => c);
            //5 jokers
            if (groupedCards.Any(group => group.Count() == 5)) {
                return "AAAAA";
            }
            //4 jokers or 4 cards
            if (groupedCards.Any(group => group.Count() == 4)) {
                var oneCard = groupedCards.FirstOrDefault(group => group.Count() == 1).Key;
                var fourCard = groupedCards.FirstOrDefault(group => group.Count() == 4).Key;
                return oneCard == 'J' ? cards.Replace(oneCard, fourCard) : cards.Replace(oneCard, fourCard);
            }
            //3 jokers or 3 cards
            if (groupedCards.Any(group => group.Count() == 3)) {
                if (groupedCards.Any(group => group.Count() == 2)) {
                    var threeCard = groupedCards.FirstOrDefault(group => group.Count() == 3).Key;
                    var twoCard = groupedCards.FirstOrDefault(group => group.Count() == 2).Key;
                    return twoCard == 'J' ? cards.Replace(twoCard, threeCard) : cards.Replace(threeCard, twoCard);
                } else {
                    var threeCard = groupedCards.FirstOrDefault(group => group.Count() == 3);
                    var otherCards = cards.Distinct().Except(threeCard).OrderByDescending(card => GetCardValueWithoutJ(card));
                    return threeCard.Key == 'J' ? cards.Replace(threeCard.Key, otherCards.FirstOrDefault()) : cards.Replace('J',threeCard.Key) ;
                } 
            }
            //2 jokers
            if (groupedCards.Any(group => group.Count() == 2)) {
                if (groupedCards.Count(group => group.Count() == 2) == 2) {
                    var pairCards = groupedCards.Where(group => group.Count() == 2).Select(group => group.Key).OrderByDescending(card => GetCardValueWithoutJ(card));
                    var oneCard = groupedCards.FirstOrDefault(group => group.Count() == 1).Key;
                    return cards.Replace('J', pairCards.FirstOrDefault());
                } else {
                    var twoCard = groupedCards.FirstOrDefault(group => group.Count() == 2);
                    var otherCards = cards.Distinct().Except(twoCard).OrderByDescending(card => GetCardValueWithoutJ(card));
                    return twoCard.Key == 'J' ? cards.Replace(twoCard.Key, otherCards.FirstOrDefault()) : cards.Replace('J',twoCard.Key) ;
                }
            }
            //1 joker
            var highestCard = cards.OrderByDescending(card => GetCardValueWithoutJ(card)).FirstOrDefault();
            return cards.Replace('J',highestCard);
        } else {
            return cards;
        }
    }

    private int CalculateStrenght(string cards)
    {
        int i = 3200000; //(20 pow 5)
        return cards.Select(c => {
                i /= 20;
                return GetCardValueWithoutJ(c)*i;
                })
             .Sum();
    }

        static int GetCardValueWithoutJ(char card)
    {
        // Define card values based on your criteria
        string cardValues = "J23456789TQKA";
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