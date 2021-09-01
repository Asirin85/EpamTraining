using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Obstacle : Coordinates
    {
        public string Name { get; private set; }
        private CoordinateStructure _position;
        public Obstacle(string name, CoordinateStructure position)
        {
            if (name is { Length: > 0 })
            {
                Name = name;
                _position = position;
            }
            else throw new ArgumentException("Name can not be null or empty");
        }
        public override CoordinateStructure GetCoordinates()
        {
            return _position;
        }
    }
}
