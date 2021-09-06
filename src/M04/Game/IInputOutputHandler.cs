using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IInputOutputHandler
    {
        DirectionEnum Input();
        void Output(GameObject[,] gameField);
        void WrongInput(bool wrongRoute);
        void GameResults(bool gameWon);
    }
}
