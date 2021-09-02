using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Player : GameObject, IMovable
    {
        public int Score { get; private set; } = 0;
        public bool PlayerIsAlive { get; set; }
        private IInputOutputHandler _inputOutput;
        public Player(CoordinateStructure startPosition, IInputOutputHandler inputOutput)
        {
            _inputOutput = inputOutput;
            PlayerIsAlive = true;
            Coordinates = startPosition;
        }

        public bool Move(GameObject[,] gameField)
        {
            var ateFruit = false;
            var moved = false;
            while (!moved)
            {
                var direction = _inputOutput.Input();
                switch (direction)
                {
                    case DirectionEnum.Up when CanMoveTo(gameField, Coordinates.X, Coordinates.Y - 1):
                        moved = TryMove(gameField, Coordinates.X, Coordinates.Y - 1);
                        break;
                    case DirectionEnum.Left when CanMoveTo(gameField, Coordinates.X - 1, Coordinates.Y):
                        moved = TryMove(gameField, Coordinates.X - 1, Coordinates.Y);
                        break;
                    case DirectionEnum.Down when CanMoveTo(gameField, Coordinates.X, Coordinates.Y + 1):
                        moved = TryMove(gameField, Coordinates.X, Coordinates.Y + 1);
                        break;
                    case DirectionEnum.Right when CanMoveTo(gameField, Coordinates.X + 1, Coordinates.Y):
                        moved = TryMove(gameField, Coordinates.X + 1, Coordinates.Y);
                        break;
                    case DirectionEnum.Up or DirectionEnum.Left or DirectionEnum.Down or DirectionEnum.Right:
                        var wrongRoute = true;
                        _inputOutput.WrongInput(wrongRoute);
                        break;
                }
            }
            switch (gameField[Coordinates.X, Coordinates.Y])
            {
                case Enemy:
                    PlayerIsAlive = false;
                    break;
                case Fruit fr:
                    Score += fr.Score;
                    ateFruit = true;
                    break;
            }
            gameField[Coordinates.X, Coordinates.Y] = this;
            return ateFruit; // Method returns true if a fruit was eaten on the current turn 
        }

        public bool CanMoveTo(GameObject[,] gameField, int X, int Y)
        {
            return X < gameField.GetLength(0) && X >= 0 && Y < gameField.GetLength(1) && Y >= 0 && gameField[X, Y] is not Obstacle; // Check if you can step in the X,Y point in a field
        }

        private bool TryMove(GameObject[,] gameField, int X, int Y)
        {
            gameField[Coordinates.X, Coordinates.Y] = null;
            Coordinates = new CoordinateStructure(X, Y);
            return true;
        }
    }
}
