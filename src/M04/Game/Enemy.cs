using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Enemy : GameObject, IMovable
    {
        public string Name { get; }
        private Player _player;
        public Enemy(Player player, CoordinateStructure monsterPosition, string name) : base(monsterPosition)
        {
            if (name is { Length: > 0 })
            {
                _player = player;
                Name = name;
            }
            else throw new ArgumentException("Name can not be null or empty");
        }

        public IMovable.MoveResult Move(GameObject[,] gameField)
        {
            if (_player.GetCoordinates().X < Coordinates.X)
            {
                if (CanMoveTo(gameField, Coordinates.X - 1, Coordinates.Y))
                {
                    ChangePosition(gameField, Coordinates.X - 1, Coordinates.Y);
                }
                else MoveByY(gameField);
            }
            else if (_player.GetCoordinates().X > Coordinates.X)
            {
                if (CanMoveTo(gameField, Coordinates.X + 1, Coordinates.Y))
                {
                    ChangePosition(gameField, Coordinates.X + 1, Coordinates.Y);
                }
                else MoveByY(gameField);
            }
            else MoveByY(gameField);
            return new IMovable.MoveResult(!_player.PlayerIsAlive); // Did player die?
        }
        private void ChangePosition(GameObject[,] gameField, int X, int Y)
        {
            gameField[Coordinates.X, Coordinates.Y] = null;
            Coordinates = new CoordinateStructure() { X = X, Y = Y };
            if (gameField[X, Y] is Player)
            {
                _player.PlayerIsAlive = false;
            }
            gameField[X, Y] = this;
        }
        private void MoveByY(GameObject[,] gameField)
        {
            if (_player.GetCoordinates().Y < Coordinates.Y)
            {
                if (CanMoveTo(gameField, Coordinates.X, Coordinates.Y - 1))
                {
                    ChangePosition(gameField, Coordinates.X, Coordinates.Y - 1);
                }
            }
            else if (_player.GetCoordinates().Y > Coordinates.Y)
            {
                if (CanMoveTo(gameField, Coordinates.X, Coordinates.Y + 1))
                {
                    ChangePosition(gameField, Coordinates.X, Coordinates.Y + 1);
                }
            }
        }

        public bool CanMoveTo(GameObject[,] gameField, int X, int Y)
        {
            return gameField[X, Y] is null or Player; // Checks if point is empty or Player is in it
        }
    }
}
