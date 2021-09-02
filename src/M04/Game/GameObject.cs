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
        virtual public CoordinateStructure GetCoordinates()
        {
            return Coordinates;
        }
    }
}
