using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    abstract class Character : Coordinates
    {
        abstract public bool Move(object[,] gameField);
    }
}
