using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_game
{
    abstract class baseCard
    {
        public ConsoleColor Color { get; set; }
        public Point PlaceOfCard { get; set; }
        public bool IsCover { get; set; }
        public bool IsExist { get; set; }
        public int NumOfCard { get; set; }
        public abstract override bool Equals(object? obj);
        public abstract bool IsSuitable(object? obj);
        public abstract void Draw(int index);


    }
}
