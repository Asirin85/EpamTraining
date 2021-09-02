using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;
namespace ConsoleGame
{
    class ConsoleInputOutput : IInputOutputHandler
    {
        public void GameResults(bool gameWon)
        {
            if (gameWon) Console.WriteLine("Congratulations! You have won!");
            else Console.WriteLine("GAME OVER!");
        }

        public DirectionEnum Input()
        {
            var inputPassed = false;
            var direction = DirectionEnum.Up;
            Console.WriteLine("Type [W | A | S | D] to move");
            while (!inputPassed)
            {
                inputPassed = true; // Making it true before switch, if input is incorrect it will change back to false to continue looping

                var key = Console.ReadKey().KeyChar;
                switch (Char.ToLowerInvariant(key))
                {
                    case 'w':
                        direction = DirectionEnum.Up;
                        break;
                    case 's':
                        direction = DirectionEnum.Down;
                        break;
                    case 'a':
                        direction = DirectionEnum.Left;
                        break;
                    case 'd':
                        direction = DirectionEnum.Right;
                        break;
                    default:
                        inputPassed = false;
                        var wrongRoute = false;
                        WrongInput(wrongRoute);
                        break;
                }
            }
            return direction;
        }

        public void Output(GameObject[,] gameField)
        {
            Console.Clear();
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    switch (gameField[j, i])
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

        public void WrongInput(bool wrongRoute)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new String(' ', Console.BufferWidth)); // removing one line in console
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            if (wrongRoute) Console.Write("You can not go there, try different route. ");
            else Console.WriteLine("Wrong input! You need to use [W | A | S | D]");
        }
    }
}
