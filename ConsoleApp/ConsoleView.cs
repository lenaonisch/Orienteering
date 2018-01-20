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
        public static Coord DEFAULT_MAP_OFFSET = new Coord(5, 5);

        #region ctors
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

        #endregion

        #region print map, cells...
        private void PrintCell(Cell cell)
        {
            if (cell != null)
            {    
                if (!cell.Visible)
                {
                    PrintBlack(cell.Position);
                    return;
                }

                Console.SetCursorPosition((int)(_canvasOffset.x + cell.Position.x), (int)(_canvasOffset.y + cell.Position.y));
                if (cell is Person)
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
            PrintBounds(new Coord(_canvasOffset.y - 1, _canvasOffset.x - 1), new Coord(_canvasOffset.y + map.Height, _canvasOffset.x + map.Width));
            for (uint x = 0; x < map.size.x; x++)
            {
                for (uint y = 0; y < map.size.y; y++)
                {
                    if (map[y, x] == null)
                    {
                        PrintBlack(y, x);
                    }
                    else
                    {
                        PrintCell(map[y, x]);
                    }
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

        #endregion

        #region print messages, errors...
        private void ShowText(string format, params object[] args)
        {
            Console.SetCursorPosition(0, 0);
            PrintBlack(Console.WindowWidth);
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
        
        private void PrintBounds(Coord topLeft, Coord bottomRight)
        {
            ConsoleColor cc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;

            int x = (int)topLeft.x;
            int y = (int)topLeft.y;
            int width = (int)(bottomRight.x - topLeft.x);
            int height = (int)(bottomRight.y - topLeft.y);

            Console.SetCursorPosition(x, y);
            Console.Write('\u250C');
            for (int i = 1; i < width; i++) // upper
            {
                Console.Write('\u2500');
            }
            Console.SetCursorPosition(x + width, y);
            Console.Write('\u2510');
            for (int i = 1; i <= height; i++) // |     |
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write('\u2502');
                Console.SetCursorPosition(x + width, y + i);
                Console.Write('\u2502');
            }
            Console.SetCursorPosition(x, y + height);
            Console.Write('\u2514');
            for (int i = 1; i < width; i++) //bottom
            {
                Console.Write('\u2500');
            }
            Console.SetCursorPosition(x + width, y + height);
            Console.Write('\u2518');

            Console.ForegroundColor = cc;
        }

        private void PrintBlack(int len = 1)
        {
            char[] cs = new char[len];
            for (int i = 0; i < len; i++)
			{
			    cs[i] = ' ';
			}
            ConsoleColor cc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(new string(cs));
            Console.ForegroundColor = cc;
        }
        private void PrintBlack(Coord c)
        {
            Console.SetCursorPosition((int)(_canvasOffset.x + c.x), (int)(_canvasOffset.y + c.y));
            PrintBlack();
        }
        private void PrintBlack(uint y, uint x)
        {
            Console.SetCursorPosition((int)(_canvasOffset.x + x), (int)(_canvasOffset.y + y));
            PrintBlack();
        }
        public void ShowHint(string hint, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            ShowText(hint, args);
        }
        #endregion

        #region interact with user
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
        #endregion

        #region fields, properties
        public Game CurrentGame
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
        #endregion

        #region events, handlers...
        public void OnCheckpointTaken(object sender, CellsEventArgs args)
        {
            ShowHint("Checkpoint {0} was taken!", (Checkpoint)args._cells[0]);
            ReprintChangedCells(args._cells);
        }

        public void OnGameEnded(object sender, ref EndGameEventArgs args)
        {
            if (args == null)
            {
                args = new EndGameEventArgs();
            }
            args.Restart = GetYesNoAnswer("Press Y to repeat: ?"); 
        }

        public void OnHiddenChkpFound(object sender, CellsEventArgs args)
        {
            throw new NotImplementedException();
        }

        EndGameDelegate _endGame;
        public event EndGameDelegate EndGame
        {
            add
            {
                _endGame += value;
            }
            remove
            {
                _endGame -= value;
            }
        }
        #endregion
    }
}
