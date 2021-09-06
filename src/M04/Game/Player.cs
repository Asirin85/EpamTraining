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
        public bool AteFruit { get; private set; }
        private IInputOutputHandler _inputOutput;
        public Player(CoordinateStructure startPosition, IInputOutputHandler inputOutput) : base(startPosition)
        {
            _inputOutput = inputOutput;
            PlayerIsAlive = true;
        }

        public IMovable.MoveResult Move(GameObject[,] gameField)
        {
            TryMove(gameField);
            AteFruit = ChangeState(gameField);
            return new IMovable.MoveResult(AteFruit); // Return true if fruit was eaten on this turn
        }

        public bool CanMoveTo(GameObject[,] gameField, int X, int Y)
        {
            return X < gameField.GetLength(0) && X >= 0 && Y < gameField.GetLength(1) && Y >= 0 && gameField[X, Y] is not Obstacle; // Check if you can step in the X,Y point in a field
        }
        private bool ChangeState(GameObject[,] gameField)
        {
            var ateFruit = false;
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
            return ateFruit;
        }
        private void TryMove(GameObject[,] gameField)
        {
            var movable = false;
            while (!movable)
            {
                var direction = _inputOutput.Input();
                switch (direction)
                {
                    case DirectionEnum.Up when CanMoveTo(gameField, Coordinates.X, Coordinates.Y - 1):
                        movable = TryChangePosition(gameField, Coordinates.X, Coordinates.Y - 1);
                        break;
                    case DirectionEnum.Left when CanMoveTo(gameField, Coordinates.X - 1, Coordinates.Y):
                        movable = TryChangePosition(gameField, Coordinates.X - 1, Coordinates.Y);
                        break;
                    case DirectionEnum.Down when CanMoveTo(gameField, Coordinates.X, Coordinates.Y + 1):
                        movable = TryChangePosition(gameField, Coordinates.X, Coordinates.Y + 1);
                        break;
                    case DirectionEnum.Right when CanMoveTo(gameField, Coordinates.X + 1, Coordinates.Y):
                        movable = TryChangePosition(gameField, Coordinates.X + 1, Coordinates.Y);
                        break;
                    default:
                        var wrongRoute = true;
                        _inputOutput.WrongInput(wrongRoute);
                        break;
                }
            }
        }
        private bool TryChangePosition(GameObject[,] gameField, int X, int Y)
        {
            gameField[Coordinates.X, Coordinates.Y] = null;
            Coordinates = new CoordinateStructure() { X = X, Y = Y };
            return true;
        }
    }
}
