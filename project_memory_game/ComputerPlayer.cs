using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_game
{
    internal class ComputerPlayer : BasicPlayer
    {
        public string Name { get; } = "computer";
        public List<baseCard> pickedCards { get; set; }

        public List<baseCard> currentCards { get; set; } = new List<baseCard>();

        public baseCard prevCard1 { get; set; } = null;
        public baseCard prevCard2 { get; set; } = null;
        public baseCard prevprevCard1 { get; set; } = null;
        public baseCard prevprevCard2 { get; set; } = null;


        public ComputerPlayer()
        {
            pickedCards = new List<baseCard>();
            currentCards = new List<baseCard>();
        }
        public override void ChoiseCard(ref int card1, ref int card2, int level)
        {
            Random rand = new Random();
            if (level == 1)
            {
                for (int i = 0; i < pickedCards.Count; i++)
                {
                    for (int j = i + 1; j < pickedCards.Count; j++)
                    {
                        if (pickedCards[i].IsSuitable(pickedCards[j]) && pickedCards[i].NumOfCard != pickedCards[j].NumOfCard)
                        {
                            card1 = pickedCards[i].NumOfCard;
                            card2 = pickedCards[j].NumOfCard;
                            Thread.Sleep(1000);
                            return;
                        }
                    }
                }
                card1 = rand.Next(currentCards.Count);
                for (int i = 0; i < pickedCards.Count; i++)
                {
                    if (currentCards[card1].NumOfCard != pickedCards[i].NumOfCard && pickedCards[i].IsSuitable(currentCards[card1]))
                    {
                        card2 = pickedCards[i].NumOfCard;
                        Thread.Sleep(1000);
                        return;
                    }
                }
            }
            else
            {
                card1 = rand.Next(currentCards.Count);
                if (!(pickedCards.Count < 4))
                    for (int i = pickedCards.Count - 4; i < pickedCards.Count; i++)
                    {
                        if (currentCards[card1].NumOfCard != pickedCards[i].NumOfCard && pickedCards[i].IsSuitable(currentCards[card1]))
                        {
                            card2 = pickedCards[i].NumOfCard;
                            Thread.Sleep(1000);
                            return;
                        }
                    }
            }
            while (currentCards[card1].NumOfCard == currentCards[card2].NumOfCard)
            {
                card2 = rand.Next(currentCards.Count);
            }
            card1 = currentCards[card1].NumOfCard;
            card2 = currentCards[card2].NumOfCard;

        }
    }
}
