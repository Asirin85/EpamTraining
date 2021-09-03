using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Obstacle : GameObject
    {
        public string Name { get; }
        public Obstacle(string name, CoordinateStructure position) : base(position)
        {
            if (name is { Length: > 0 })
            {
                Name = name;
            }
            else throw new ArgumentException("Name can not be null or empty");
        }
    }
}
