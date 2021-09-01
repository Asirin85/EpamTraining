using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Player : Character
    {
        private CoordinateStructure _currentPosition;
        public int Score { get; private set; } = 0;
        public bool PlayerIsAlive { get; set; }
        public Player(CoordinateStructure startPosition)
        {
            PlayerIsAlive = true;
            _currentPosition = startPosition;
        }
        public override CoordinateStructure GetCoordinates()
        {
            return _currentPosition;
        }

        public override bool Move(object[,] gameField)
        {
            bool ateFruit = false;
            bool moved = false;
            bool wrongKey = false;
            while (!moved)
            {
                Console.WriteLine("Type w,a,s,d to move");
                var key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (key)
                {
                    case 'w':

                        if (_currentPosition.Y - 1 >= 0 && gameField[_currentPosition.X, _currentPosition.Y - 1] is not Obstacle)
                        {
                            wrongKey = false;
                            moved = MoveHelper(gameField, _currentPosition.X, _currentPosition.Y - 1);
                        }
                        else
                        {
                            WrongInput(wrongKey);
                            wrongKey = true;
                            Console.WriteLine("You can not go there, try different route");
                        }
                        break;
                    case 'a':

                        if (_currentPosition.X - 1 >= 0 && gameField[_currentPosition.X - 1, _currentPosition.Y] is not Obstacle)
                        {
                            wrongKey = false;
                            moved = MoveHelper(gameField, _currentPosition.X - 1, _currentPosition.Y);
                        }
                        else
                        {
                            WrongInput(wrongKey);
                            wrongKey = true;
                            Console.WriteLine("You can not go there, try different route");
                        }
                        break;
                    case 's':

                        if (_currentPosition.Y + 1 < gameField.GetLength(1) && gameField[_currentPosition.X, _currentPosition.Y + 1] is not Obstacle)
                        {
                            wrongKey = false;
                            moved = MoveHelper(gameField, _currentPosition.X, _currentPosition.Y + 1);
                        }
                        else
                        {
                            WrongInput(wrongKey);
                            wrongKey = true;
                            Console.WriteLine("You can not go there, try different route");
                        }
                        break;
                    case 'd':

                        if (_currentPosition.X + 1 < gameField.GetLength(0) && gameField[_currentPosition.X + 1, _currentPosition.Y] is not Obstacle)
                        {
                            wrongKey = false;
                            moved = MoveHelper(gameField, _currentPosition.X + 1, _currentPosition.Y);
                        }
                        else
                        {
                            WrongInput(wrongKey);
                            wrongKey = true;
                            Console.WriteLine("You can not go there, try different route");
                        }
                        break;
                    default:
                        WrongInput(wrongKey);
                        wrongKey = true;
                        Console.WriteLine("Wrong key, try again");
                        break;
                }
            }
            switch (gameField[_currentPosition.X, _currentPosition.Y])
            {
                case Enemy:
                    PlayerIsAlive = false;
                    break;
                case Fruit fr:
                    Score += fr.Score;
                    ateFruit = true;
                    break;
            }
            gameField[_currentPosition.X, _currentPosition.Y] = this;
            return ateFruit;
        }
        private bool MoveHelper(object[,] gameField, int X, int Y)
        {
            gameField[_currentPosition.X, _currentPosition.Y] = null;
            _currentPosition.X = X;
            _currentPosition.Y = Y;
            return true;
        }
        private void WrongInput(bool secondMistake)
        {
            int counter = 2;
            if (secondMistake) counter = 3;
            while (counter > 0)// amount of lines
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
                counter--;
            }
        }
    }
}
