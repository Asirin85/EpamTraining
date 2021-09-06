using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IMovable
    {
        MoveResult Move(GameObject[,] gameField);
        record MoveResult(bool HasEaten);
        bool CanMoveTo(GameObject[,] gameField, int X, int Y);
    }
}
