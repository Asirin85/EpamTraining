using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Fruit : GameObject
    {
        public string Name { get; }
        public int Score { get; }
        public Fruit(string name, int score, CoordinateStructure position) : base(position)
        {
            if (name is { Length: > 0 })
            {
                Name = name;
                Score = score;
            }
            else throw new ArgumentException("Name can not be null or empty");
        }
    }
}
