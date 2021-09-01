using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Enemy : Character
    {
        public string Name { get; set; }
        private Player _player;
        private CoordinateStructure _currentMonsterPosition;
        public Enemy(Player player, CoordinateStructure monsterPosition, string name)
        {
            _player = player;
            _currentMonsterPosition = monsterPosition;
            Name = name;
        }
        public override CoordinateStructure GetCoordinates()
        {
            return _currentMonsterPosition;
        }

        public override bool Move(object[,] gameField)
        {
            if (_player.GetCoordinates().X < _currentMonsterPosition.X)
            {
                if (gameField[_currentMonsterPosition.X - 1, _currentMonsterPosition.Y] is null or Player)
                {
                    ChangePos(gameField, _currentMonsterPosition.X - 1, _currentMonsterPosition.Y);
                }
                else MoveByY(gameField);
            }
            else if (_player.GetCoordinates().X > _currentMonsterPosition.X)
            {
                if (gameField[_currentMonsterPosition.X + 1, _currentMonsterPosition.Y] is null or Player)
                {
                    ChangePos(gameField, _currentMonsterPosition.X + 1, _currentMonsterPosition.Y);
                }
                else MoveByY(gameField);
            }
            else MoveByY(gameField);
            return !_player.PlayerIsAlive; // did player die?
        }
        private void ChangePos(object[,] gameField, int X, int Y)
        {
            gameField[_currentMonsterPosition.X, _currentMonsterPosition.Y] = null;
            _currentMonsterPosition.X = X;
            _currentMonsterPosition.Y = Y;
            if (gameField[X, Y] is Player)
            {
                _player.PlayerIsAlive = false;
            }
            gameField[X, Y] = this;
        }
        private void MoveByY(object[,] gameField)
        {
            if (_player.GetCoordinates().Y < _currentMonsterPosition.Y)
            {
                if (gameField[_currentMonsterPosition.X, _currentMonsterPosition.Y - 1] is null or Player)
                {
                    ChangePos(gameField, _currentMonsterPosition.X, _currentMonsterPosition.Y - 1);
                }
            }
            else if (_player.GetCoordinates().Y > _currentMonsterPosition.Y)
            {
                if (gameField[_currentMonsterPosition.X, _currentMonsterPosition.Y + 1] is null or Player)
                {
                    ChangePos(gameField, _currentMonsterPosition.X, _currentMonsterPosition.Y + 1);
                }
            }
        }
    }
}
