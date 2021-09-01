using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Field : Coordinates
    {
        private object[,] _field;
        private Player _player;
        private Enemy[] _enemies;
        private Fruit[] _fruits;
        private CoordinateStructure _maxFieldSize;
        private Obstacle[] _obstacles;
        public Field(int maxX, int maxY)
        {
            if (maxX >= 5 && maxY >= 5 && maxX <= 15 && maxY <= 15) // creating gaming field
            {
                _maxFieldSize = new CoordinateStructure(maxX, maxY);
                _field = new object[_maxFieldSize.X, _maxFieldSize.Y];
            }
            else
            {
                _maxFieldSize = new CoordinateStructure(15, 15);
                _field = new object[_maxFieldSize.X, _maxFieldSize.Y];
            }
            List<int> possibleCoord = Enumerable.Range(0, _maxFieldSize.X * _maxFieldSize.Y).ToList();
            Random rnd = new Random();

            CoordinateStructure position = CreatePosition(possibleCoord, rnd);
            _player = new Player(position);                             // welcome new player
            _field[position.X, position.Y] = _player;

            _fruits = new Fruit[_maxFieldSize.X * _maxFieldSize.Y / 20]; // create fruits
            for (int i = 0; i < _fruits.Length; i++)
            {
                position = CreatePosition(possibleCoord, rnd);
                _fruits[i] = new Fruit("Apple", rnd.Next(0, 11), position);
                _field[position.X, position.Y] = _fruits[i];
            }

            _obstacles = new Obstacle[_maxFieldSize.X * _maxFieldSize.Y / 5]; //create obstacles
            for (int i = 0; i < _obstacles.Length; i++)
            {
                position = CreatePosition(possibleCoord, rnd);
                _obstacles[i] = new Obstacle("Tree", position);
                _field[position.X, position.Y] = _obstacles[i];
            }

            if (_maxFieldSize.X * _maxFieldSize.Y / 25 > 5)
            {
                _enemies = new Enemy[5];
            }
            else _enemies = new Enemy[_maxFieldSize.X * _maxFieldSize.Y / 25]; // create enemies

            for (int i = 0; i < _enemies.Length; i++)
            {
                position = CreatePosition(possibleCoord, rnd);
                _enemies[i] = new Enemy(_player, position, "Wolf");
                _field[position.X, position.Y] = _enemies[i];
            }
        }
        public int StartGame()
        {
            int leftToEat = _fruits.Length;
            while (_player.PlayerIsAlive && leftToEat > 0)
            {
                DrawField();
                if (_player.Move(_field))
                {
                    leftToEat--;
                    if (leftToEat == 0) break;
                }
                foreach (Enemy enemy in _enemies)
                {
                    enemy.Move(_field);
                    if (!_player.PlayerIsAlive)
                    {
                        DrawField();
                        Console.WriteLine("YOU DIED! GAME OVER!");
                        break;
                    }
                }

            }
            return _player.Score;
        }
        public override CoordinateStructure GetCoordinates()
        {
            return _maxFieldSize;
        }
        private CoordinateStructure CreatePosition(List<int> possibleCoord, Random rnd)
        {
            int generatedNumber = rnd.Next(0, possibleCoord.Count());
            CoordinateStructure position = new CoordinateStructure(possibleCoord[generatedNumber] % 10, possibleCoord[generatedNumber] / 10);
            possibleCoord.RemoveAt(generatedNumber);
            return position;
        }
        private void DrawField() // Draw in console
        {
            Console.Clear();
            for (int i = 0; i < _maxFieldSize.Y; i++)
            {
                for (int j = 0; j < _maxFieldSize.X; j++)
                {
                    switch (_field[j, i])
                    {
                        case Fruit:
                            Console.Write("[F]", Console.ForegroundColor = ConsoleColor.Yellow); 
                            Console.ResetColor();
                            break;
                        case Enemy:
                            Console.Write("[E]", Console.ForegroundColor = ConsoleColor.Red); 
                            Console.ResetColor();
                            break;
                        case Obstacle:
                            Console.Write("[T]", Console.ForegroundColor = ConsoleColor.Magenta);
                            Console.ResetColor();
                            break;
                        case Player:
                            Console.Write("[P]", Console.ForegroundColor = ConsoleColor.Green); 
                            Console.ResetColor();
                            break;
                        default:
                            Console.ResetColor();
                            Console.Write("[ ]");
                            break;
                    }

                }
                Console.WriteLine();
            }
        }
    }
}
