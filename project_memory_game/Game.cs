using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_game
{
    public enum KindOfCards { LetterCard = 1, SymbolCard, ExcersizeCard };
    internal class Game
    {
        public ComputerPlayer computer { get; set; }
        public const int sizeOfarr = 42;
        public static int CountOfCurrentCards { get; set; } = sizeOfarr;
        public int AmountOfPlayer { get; set; }
        private baseCard[] letterArr = new LetterCard[sizeOfarr];
        private baseCard[] symbolArr = new SymbolCard[sizeOfarr];
        private baseCard[] excersizeArr = new ExcersizeCard[sizeOfarr];
        public List<BasicPlayer> listOfPlayer { get; set; } = new List<BasicPlayer>();


        public Dictionary<KindOfCards, baseCard[]> AllCards { get; set; } = new Dictionary<KindOfCards, baseCard[]>();
        public int CurrentPlayer { get; set; }
        public KindOfCards kindOfgame { get; set; }
        public Board BoardGame { get; set; } = null;
        public Game(KindOfCards kindOfgame)
        {
            Random rand = new Random();
            for (int i = 0; i < sizeOfarr; i += 2)
            {
                letterArr[i] = new LetterCard() { NumOfCard = i, IsExist = true, IsCover = true, Letter = (char)rand.Next(65, 91), Color = (ConsoleColor)rand.Next(1, 15) };
                letterArr[i + 1] = new LetterCard() { NumOfCard = i + 1, IsCover = letterArr[i].IsCover, IsExist = letterArr[i].IsExist, Letter = (letterArr[i] as LetterCard).Letter, Color = letterArr[i].Color };
            }
            for (int i = 0; i < sizeOfarr; i += 2)
            {
                symbolArr[i] = new SymbolCard() { NumOfCard = i, IsCover = true, IsExist = true, Sign = (char)rand.Next(33, 43), Color = (ConsoleColor)rand.Next(1, 15) };
                symbolArr[i + 1] = new SymbolCard() { NumOfCard = i + 1, IsCover = symbolArr[i].IsCover, IsExist = letterArr[i].IsExist, Color = symbolArr[i].Color, Sign = (symbolArr[i] as SymbolCard).Sign };
            }
            for (int i = 0; i < sizeOfarr; i += 2)
            {
                excersizeArr[i] = new ExcersizeCard() { NumOfCard = i, IsCover = true, IsExist = true, Excersize = (char)rand.Next(48, 58) + "+" + (char)rand.Next(48, 58), Color = (ConsoleColor)rand.Next(1, 15) };
                (excersizeArr[i] as ExcersizeCard).Solution = ((int)((excersizeArr[i] as ExcersizeCard).Excersize[0] - 48) + (int)((excersizeArr[i] as ExcersizeCard).Excersize[2] - 48)).ToString();
                excersizeArr[i + 1] = new ExcersizeCard() { NumOfCard = i + 1, IsCover = excersizeArr[i].IsCover, IsExist = letterArr[i].IsExist, Excersize = (excersizeArr[i] as ExcersizeCard).Solution, Solution = (excersizeArr[i] as ExcersizeCard).Solution, Color = excersizeArr[i].Color };
            }

            AllCards.Add(KindOfCards.LetterCard, letterArr);
            AllCards.Add(KindOfCards.SymbolCard, symbolArr);
            AllCards.Add(KindOfCards.ExcersizeCard, excersizeArr);

            this.kindOfgame = kindOfgame;
        }
        public void Playing(int card)
        {
            BoardGame.BoardCards[card].IsCover = false;
            BoardGame.DrawBoard();
            BoardGame.sendCardsToPrint();
            Console.SetCursorPosition(0, 0);
        }
        public void RestartGame(int flag)
        {
            BoardGame = new Board(AllCards[kindOfgame]) { Size = 87 };
            if (flag == 1)
                computer.currentCards.AddRange(BoardGame.BoardCards);
            Console.Clear();
            BoardGame.ResetBoard();
        }
        public void FinishToPick(int card1, int card2, int IndexOfPlayer, int flag)
        {
            Thread.Sleep(2000);
            if (BoardGame.IsSame(card1, card2))
            {
                listOfPlayer[IndexOfPlayer].Points++;
                listOfPlayer[IndexOfPlayer].cards.Add(BoardGame.BoardCards[card1]);
                listOfPlayer[IndexOfPlayer].cards.Add(BoardGame.BoardCards[card2]);
                BoardGame.BoardCards[card1].Draw(-1);
                BoardGame.BoardCards[card2].Draw(-1);
                BoardGame.BoardCards[card1].IsExist = false;
                BoardGame.BoardCards[card2].IsExist = false;

                if (flag == 1)
                {
                    computer.currentCards.Remove(BoardGame.BoardCards[card1]);
                    computer.currentCards.Remove(BoardGame.BoardCards[card2]);

                    computer.pickedCards.Remove(BoardGame.BoardCards[card1]);
                    computer.pickedCards.Remove(BoardGame.BoardCards[card2]);
                }             

            }
            else
            {
                BoardGame.BoardCards[card1].IsCover = true;
                BoardGame.reverseCard(card1);
                if (flag == 1)
                {
                    computer.pickedCards.Add(BoardGame.BoardCards[card1]);
                    computer.pickedCards.Add(BoardGame.BoardCards[card2]);
                }
                BoardGame.BoardCards[card2].IsCover = true;
                BoardGame.reverseCard(card2);
            }
        }
        public void FinishGame()
        {
            Console.Clear();
            int max = -1;
            int imax = 0;
            string maxName = "";
            for (int i = 0; i < listOfPlayer.Count; i++)
            {
                if (listOfPlayer[i].Points > max)
                {
                    imax = i;
                    max = listOfPlayer[i].Points;
                    maxName = listOfPlayer[i].Name;
                }
            }
            foreach (var item in listOfPlayer)
            {
                if (item.Points == max && item.Name != maxName)
                {
                    Console.SetCursorPosition(55, 16);
                    Console.WriteLine("Bingo!!!!!!!!!!!");
                    Console.SetCursorPosition(55, 17);
                    Console.WriteLine(item.Name + " you won!!!!!!!!!!!!");
                    Console.SetCursorPosition(65, 18);
                    Console.Write("Your cards:");
                    Point p1 = new Point(75, 18);
                    item.ShowCards(p1);
                    Console.WriteLine();

                }
            }
            Console.SetCursorPosition(55, 20);
            Console.WriteLine(maxName + " you won!!!!!!!!!!!!");
            Console.SetCursorPosition(45, 21);
            Console.WriteLine("Your cards:");
            Point p = new Point(55, 21);
            listOfPlayer[imax].ShowCards(p);

            int x = 29;
            Console.SetCursorPosition(55, x);
            foreach (var item in listOfPlayer)
            {
                Console.WriteLine(item.Name+": "+item.Points);
                x++;
                Console.SetCursorPosition(55, x);
            }

            Random random = new Random();
            while (!Console.KeyAvailable)
            {
                Console.SetCursorPosition(random.Next(120), random.Next(50));
                Console.ForegroundColor = (ConsoleColor)random.Next(1, 15);
                Thread.Sleep(250);
                Console.Write("*");
            }

        }
        public void PlaywithThecomputer(ComputerPlayer c)
        {
            computer = c;

        }
    }
}

