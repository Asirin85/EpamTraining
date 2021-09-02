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
        public Enemy(Player player, CoordinateStructure monsterPosition, string name)
        {
            _player = player;
            Coordinates = monsterPosition;
            Name = name;
        }

        public bool Move(GameObject[,] gameField)
        {
            if (_player.GetCoordinates().X < Coordinates.X)
            {
                if (CanMoveTo(gameField, Coordinates.X - 1, Coordinates.Y))
                {
                    ChangePos(gameField, Coordinates.X - 1, Coordinates.Y);
                }
                else MoveByY(gameField);
            }
            else if (_player.GetCoordinates().X > Coordinates.X)
            {
                if (CanMoveTo(gameField, Coordinates.X + 1, Coordinates.Y))
                {
                    ChangePos(gameField, Coordinates.X + 1, Coordinates.Y);
                }
                else MoveByY(gameField);
            }
            else MoveByY(gameField);
            return !_player.PlayerIsAlive; // did player die?
        }
        private void ChangePos(GameObject[,] gameField, int X, int Y)
        {
            gameField[Coordinates.X, Coordinates.Y] = null;
            Coordinates = new CoordinateStructure(X, Y);
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
                    ChangePos(gameField, Coordinates.X, Coordinates.Y - 1);
                }
            }
            else if (_player.GetCoordinates().Y > Coordinates.Y)
            {
                if (CanMoveTo(gameField, Coordinates.X, Coordinates.Y + 1))
                {
                    ChangePos(gameField, Coordinates.X, Coordinates.Y + 1);
                }
            }
        }

        public bool CanMoveTo(GameObject[,] gameField, int X, int Y)
        {
            return gameField[X, Y] is null or Player; // Checks if point is empty or Player is in it
        }
    }
}
