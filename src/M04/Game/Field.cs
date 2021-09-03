using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Field : GameObject
    {
        private GameObject[,] _field;
        private Player _player;
        private Enemy[] _enemies;
        private Fruit[] _fruits;
        private Obstacle[] _obstacles;
        private IInputOutputHandler _inputOutput;
        public Field(int maxX, int maxY, IInputOutputHandler inputOutput) : base (new CoordinateStructure()  { X = maxX, Y = maxY })
        {
            _inputOutput = inputOutput;
            if (maxX >= 5 && maxY >= 5 && maxX <= 15 && maxY <= 15) // creating gaming field
            {
                _field = new GameObject[Coordinates.X, Coordinates.Y];
            }
            else
            {
                Coordinates = new CoordinateStructure() { X = 15, Y = 15 };
                _field = new GameObject[Coordinates.X, Coordinates.Y];
            }
            List<int> possibleCoord = Enumerable.Range(0, Coordinates.X * Coordinates.Y).ToList();
            var rnd = new Random();
            CreatePlayer(possibleCoord, rnd);
            CreateFruits(possibleCoord, rnd);
            CreateObstacles(possibleCoord, rnd);
            CreateEnemies(possibleCoord, rnd);
        }
        public int StartGame()
        {
            var leftToEat = _fruits.Length;
            while (_player.PlayerIsAlive && leftToEat > 0)
            {
                _inputOutput.Output(_field);
                if (_player.Move(_field).HasEaten)
                {
                    leftToEat--;
                    if (leftToEat == 0)
                    {
                        var gameWon = true;
                        _inputOutput.GameResults(gameWon);
                        break;
                    }
                }
                foreach (Enemy enemy in _enemies)
                {
                    if (enemy.Move(_field).HasEaten) // condition is true if player is dead
                    {
                        _inputOutput.Output(_field);
                        var gameWon = false;
                        _inputOutput.GameResults(gameWon);
                        break;
                    }
                }

            }
            return _player.Score;
        }
        private CoordinateStructure CreatePosition(List<int> possibleCoord, Random rnd)
        {
            var generatedNumber = rnd.Next(0, possibleCoord.Count());
            CoordinateStructure position = new CoordinateStructure() { X = possibleCoord[generatedNumber] % 10, Y = possibleCoord[generatedNumber] / 10 };
            possibleCoord.RemoveAt(generatedNumber);
            return position;
        }
        private void CreateFruits(List<int> possibleCoord, Random rnd)
        {
            _fruits = new Fruit[Coordinates.X * Coordinates.Y / 20];
            for (int i = 0; i < _fruits.Length; i++)
            {
                CoordinateStructure fruitPosition = CreatePosition(possibleCoord, rnd);
                _fruits[i] = new Fruit("Apple", rnd.Next(0, 11), fruitPosition);
                _field[fruitPosition.X, fruitPosition.Y] = _fruits[i];
            }
        }
        private void CreateObstacles(List<int> possibleCoord, Random rnd)
        {
            _obstacles = new Obstacle[Coordinates.X * Coordinates.Y / 5];
            for (int i = 0; i < _obstacles.Length; i++)
            {
                CoordinateStructure obstaclePosition = CreatePosition(possibleCoord, rnd);
                _obstacles[i] = new Obstacle("Tree", obstaclePosition);
                _field[obstaclePosition.X, obstaclePosition.Y] = _obstacles[i];
            }
        }
        private void CreateEnemies(List<int> possibleCoord, Random rnd)
        {
            if (Coordinates.X * Coordinates.Y / 25 > 5)
            {
                _enemies = new Enemy[5];
            }
            else _enemies = new Enemy[Coordinates.X * Coordinates.Y / 25];
            for (int i = 0; i < _enemies.Length; i++)
            {
                CoordinateStructure enemyPosition = CreatePosition(possibleCoord, rnd);
                _enemies[i] = new Enemy(_player, enemyPosition, "Wolf");
                _field[enemyPosition.X, enemyPosition.Y] = _enemies[i];
            }
        }
        private void CreatePlayer(List<int> possibleCoord, Random rnd)
        {
            CoordinateStructure position = CreatePosition(possibleCoord, rnd);
            _player = new Player(position, _inputOutput);
            _field[position.X, position.Y] = _player;
        }
    }
}
