using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_game
{
    abstract class BasicPlayer
    {
        public int Points { get; set; }
        public List<baseCard> cards { get; set; } = new List<baseCard>();
        public string Name { get; private set; }
        public BasicPlayer(string name) : this()
        {
            Name = name;
        }
        public BasicPlayer()
        {
            Points = 0;
        }
        public virtual void ChoiseCard(ref int card1, ref int card2,int level) { }
        public virtual void ResetName(string name) => this.Name = name;
        public void ShowCards(Point p)
        {

            int i = 0;
            foreach (var item in cards)
            {
                item.IsCover = false;
                item.PlaceOfCard = p;
                p.X += 3;
                item.Draw(i++);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}

