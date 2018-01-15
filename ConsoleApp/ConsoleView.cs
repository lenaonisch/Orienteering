using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orienteering
{
    public class ConsoleView : IView
    {
        public ConsoleView()
        {
            Console.CursorVisible = false;
            Console.Clear();
        }

        public ConsoleView(Coord offset)
            : this()
        {
            _canvasOffset = offset;
        }

        public static Coord DEFAULT_MAP_OFFSET = new Coord(5, 5);

        private void PrintCell(Cell cell)
        {
            if (cell != null)
            {
                Console.SetCursorPosition((int)(_canvasOffset.x + cell.Position.x * 2), (int)(_canvasOffset.y + cell.Position.y));
                if (cell is Player)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("P");
                    return;
                }
                if (cell is Checkpoint)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("C");
                    return;
                }
                else
                {
                    switch ((cell as Obstacle)._type)
                    {
                        case ObstacleType.River:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("W");
                            break;
                        case ObstacleType.Swamp:
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("S");
                            break;
                        case ObstacleType.Tree:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("T");
                            break;
                    }
                }
            }
        }

        public void PrintMap(Map map)
        {
            Console.Clear();
            for (uint x = 0; x < map.size.x; x++)
            {
                for (uint y = 0; y < map.size.y; y++)
                {
                    PrintCell(map[y, x]);
                }
            }
        }

        public void ReprintChangedCells(params Cell[] cells)
        {
            foreach (Cell c in cells)
            {
                PrintCell(c);
            }
        }

        private void ShowText(string format, params object[] args)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(format, args);
            Console.ResetColor();
        }

        public void PrintMessage(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            ShowText(format, args);
        }

        public void PrintError(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            ShowText(format, args);
        }

        public void PrintWarning(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ShowText(format, args);
        }

        public Key GetUserInput()
        {
            System.Threading.Thread.Sleep(20);
            Key kRet = Key.A;
            ConsoleKeyInfo keyInfo = Console.ReadKey(false);
            switch (keyInfo.Key)
            {
                case ConsoleKey.DownArrow:
                    kRet = Key.Down;
                    break;
                case ConsoleKey.UpArrow:
                    kRet = Key.Up;
                    break;
                case ConsoleKey.LeftArrow:
                    kRet = Key.Left;
                    break;
                case ConsoleKey.RightArrow:
                    kRet = Key.Right;
                    break;
                case ConsoleKey.Escape:
                    kRet = Key.Escape;
                    break;
            }

            return kRet;
        }

        public bool GetYesNoAnswer(string format, params object[] args)
        {
            PrintMessage(format, args);
            Key k = GetUserInput();
            if (k == Key.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ShowHint(string hint, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            ShowText(hint, args);
        }

        public Game Owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
            }
        }

        Game _owner = null;
        Coord _canvasOffset;

        public MapParams MapParameters
        {
            get
            {
                return new MapParams();
            }
            set
            {
                
            }
        }
    }
}
