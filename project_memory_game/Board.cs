using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace memory_game
{
    internal class Board
    {
        public const int sizeOfarr = 42;
        public static int X { get; set; } = 28;
        public static int Y { get; set; } = 6;
        public static int CountOfCurrentCards { get; private set; } = sizeOfarr;

        public int Size { get; set; }
        public baseCard[] BoardCards { get; set; }
        public Board(baseCard[] boardCards)
        {
            this.BoardCards = boardCards;

        }
        public void ResetBoard()
        {
            Random rand = new Random();
            baseCard temp;
            for (int i = 0; i < sizeOfarr * 2; i++)
            {
                int x = rand.Next(sizeOfarr);
                int y = rand.Next(sizeOfarr);
                temp = BoardCards[x];
                BoardCards[x] = BoardCards[y];
                BoardCards[y] = temp;
                BoardCards[x].NumOfCard = x;
                BoardCards[y].NumOfCard = y;
            }
            DrawBoard();
            for (int i = 0; i < BoardCards.Length; i++)
            {
                PrintCards(BoardCards[i], i);
            }
        }
        public void sendCardsToPrint()
        {
            for (int i = 0; i < BoardCards.Length; i++)
            {
                if (BoardCards[i].IsExist)
                    BoardCards[i].Draw(i);
            }
        }

        public void PrintCards(baseCard cardToprint, int index)
        {
            ConsoleColor prevBackgroundcolor = Console.BackgroundColor;
            ConsoleColor prevLetter = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            X = X + 12;
            cardToprint.PlaceOfCard = new Point(X, Y);

            if (cardToprint.IsCover)
            {
                Console.SetCursorPosition(X - 1, Y - 1);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Console.SetCursorPosition(X + i, Y + j);
                        Console.BackgroundColor = (ConsoleColor)(15);
                        Console.Write(" ");
                    }
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(X + 3, Y + 2);
                Console.Write(index + 1);
            }
            if (X == 112)
            {
                Y += 7;
                X = 28;
            }
            Console.BackgroundColor = prevBackgroundcolor;
            Console.ForegroundColor = prevLetter;
        }

        public void DrawBoard()
        {
            Console.Clear();
            ConsoleColor orginal = Console.BackgroundColor;
            Console.SetCursorPosition(35, 3);
            Console.BackgroundColor = ConsoleColor.Magenta;
            for (int i = 0; i < Size; i++)
            {
                Console.Write(" ");
            }
            for (int i = 0; i < Size / 2 + 2; i++)
            {
                Console.BackgroundColor = orginal;
                Console.SetCursorPosition(35, 3 + i);
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.WriteLine("  ");
            }
            Console.SetCursorPosition(35, 3 + Size / 2 +2);
            Console.BackgroundColor = ConsoleColor.Magenta;
            for (int i = 0; i < Size+2; i++)
            {
                Console.Write(" ");
            }

            for (int i = 0; i < Size / 2 +2; i++)
            {
                Console.BackgroundColor = orginal;
                Console.SetCursorPosition(35 + Size, 3 + i);
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.WriteLine("  ");
            }
            Console.BackgroundColor = orginal;
        }
        public bool CheckLigalPlace(int numOfCard)
        {
            if (numOfCard < 0 || numOfCard > sizeOfarr)
            {
                return false;
            }
            if (!BoardCards[numOfCard].IsCover || !BoardCards[numOfCard].IsExist)
                return false;
            return true;
        }
        public bool CheckContainsCards()
        {
            if (CountOfCurrentCards > 2)
                return true;
            return false;
        }
        public void reverseCard(int numOfCard)
        {
            BoardCards[numOfCard].Draw(numOfCard);
        }
        public bool IsSame(int card1, int card2)
        {
            if (BoardCards[card1].IsSuitable(BoardCards[card2]))
            {
                CountOfCurrentCards -= 2;
                return true;
            }
            return false;
        }
    }

}

