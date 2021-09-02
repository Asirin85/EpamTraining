using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    interface IMovable
    {
        bool Move(GameObject[,] gameField);
        bool CanMoveTo(GameObject[,] gameField, int X, int Y);
    }
}
