using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class GameObject
    {
        protected CoordinateStructure Coordinates { get; set; }
        protected GameObject(CoordinateStructure coordinates)
        {
            Coordinates = coordinates;
        }
        public virtual CoordinateStructure GetCoordinates()
        {
            return Coordinates;
        }
    }
}
