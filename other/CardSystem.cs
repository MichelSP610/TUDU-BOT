using System;

namespace TUDU_BOT.other
{
    public class CardSystem
    {
        public int[] cardNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        public string[] cardSuits = { "Clubs", "Spades", "Hearts", "Diamonds" };

        public int selectedNumber {  get; set; }
        public string selectedCard { get; set; }

        public CardSystem()
        {
            var random = new Random();
            int numberIndex = random.Next(0, cardNumbers.Length -1);
            int suitIndex = random.Next(0, cardSuits.Length -1);

            this.selectedNumber = cardNumbers[numberIndex];
            this.selectedCard = $"{cardNumbers[numberIndex]} of {cardSuits[suitIndex]}";
        }
    }
}
