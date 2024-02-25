using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_game
{
    internal class SymbolCard : baseCard
    {
        public char Sign { get; set; }
        public override bool Equals(object? obj)
        {
            if ((obj as SymbolCard).Sign == Sign && (obj as SymbolCard).Color == Color)
                return true;
            return false;
        }
        public override bool IsSuitable(object? obj)
        {
            if (this.Equals(obj))
                return true;
            return false;
        }
        public override void Draw(int index)
        {
            ConsoleColor prev = Console.BackgroundColor;
            ConsoleColor prevfor = Console.ForegroundColor;
            Console.SetCursorPosition(PlaceOfCard.X - 1, PlaceOfCard.Y - 1);
            Console.ForegroundColor = ConsoleColor.Black;
            if (index != -1 && IsCover)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Console.SetCursorPosition(PlaceOfCard.X + i, PlaceOfCard.Y + j);
                        Console.BackgroundColor = (ConsoleColor)(15);
                        Console.Write(" ");
                    }
                }
                Console.SetCursorPosition(PlaceOfCard.X + 3, PlaceOfCard.Y + 2);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(index + 1);
            }
            else
            {
                if (index != -1)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            Console.SetCursorPosition(PlaceOfCard.X + i, PlaceOfCard.Y + j);
                            Console.BackgroundColor = Color;
                            Console.Write(" ");
                        }
                    }
                    Console.SetCursorPosition(PlaceOfCard.X + 3, PlaceOfCard.Y + 2);
                    Console.Write(Sign);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            Console.SetCursorPosition(PlaceOfCard.X + i, PlaceOfCard.Y + j);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write(" ");
                        }
                    }
                }
            }
            Console.ForegroundColor = prevfor;
            Console.BackgroundColor = prev;
        }

    }
}
