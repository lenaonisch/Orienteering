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
                    Console.SetCursorPosition((int)(_canvasOffset.x + cell.Position.x), (int)(_canvasOffset.y + cell.Position.y));
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(".");
                    //PrintBlack(cell.Position);
                    return;
                }

                Console.SetCursorPosition((int)(_canvasOffset.x + cell.Position.x), (int)(_canvasOffset.y + cell.Position.y));
                if (cell is Person)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("P");
                    return;
                }
                if (cell is Crossing)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("-");
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
                    switch ((cell as Obstacle).Type)
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
            for (uint x = 0; x < map.Size.x; x++)
            {
                for (uint y = 0; y < map.Size.y; y++)
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

        public void PrintCells(params Cell[] cells)
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
                Console.SetCursorPosition(x-2, y + i);
                Console.Write("{0:00}", i-1);
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

        public void PrintCell(Coord coord, Cell cell) // print cell with background
        {
            if (cell == null)
            {
                PrintBlack(coord.y, coord.x);
            }
            else
            {
                PrintCell(cell);
            }
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
        private Key GetPressedKey()
        {
            Key kRet = Key.None;
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
                default:
                    Enum.TryParse(keyInfo.Key.ToString(), out kRet);
                    break;
            }
            return kRet;
        }

        private void GetUserInput()
        {
            do
            {
                System.Threading.Thread.Sleep(20);
                Key kRet = GetPressedKey();
                switch (kRet)
                {
                    case Key.Down:
                    case Key.Up:

                    case Key.Right:
                    case Key.Left:
                        ChangePositionEventArgs cpargs = new ChangePositionEventArgs(kRet);
                        _moveInitiated(this, cpargs);
                        break;
                    case Key.Escape:
                        GameControlEventArgs arg = new GameControlEventArgs();
                        _endGame(this, ref arg);

                        break;
                }
            }
            while (!exit);
        }

        public bool GetYesNoAnswer(string format, params object[] args)
        {
            PrintMessage(format, args);
            bool error = false;
            bool yes = true;
            do
            {
                switch (GetPressedKey())
                {
                    case Key.Y:
                        yes = true;
                        error = false;
                        break;
                    case Key.N:
                        yes = false;
                        error = false;
                        break;
                    default:
                        PrintError("Invalid input! Try once more: Y/N");
                        error = true;
                        break;
                }
            }
            while (error);
            return yes;   
        }

        public GameType GetNewGameType()
        {
            GameType gt = GameType.None;
            bool error = false;
            do
            {
                PrintMessage("Select game type: m - simple maze, o - orienteering, Esc - exit program");
                switch (GetPressedKey())
                {
                    case Key.M:
                        gt = GameType.Maze;
                        break;
                    case Key.O:
                        gt = GameType.Orienteering;
                        break;
                    case Key.Escape:
                        GameControlEventArgs arg = new GameControlEventArgs();
                        _endGame(this, ref arg);
                        break;
                    default:
                        PrintError("Invalid input! Try once more: m - simple maze, o - orienteering, Esc - exit program");
                        error = true;
                        break;
                }
            }
            while (error);
            return gt;
        }

        // dummy
        public MapParams GetMapParameters()
        {
            return new MapParams();
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
        
        #endregion

        #region events, handlers...

        GameControlDelegate _endGame;
        public event GameControlDelegate EndGame
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

        ChangePositionDelegate _moveInitiated;
        public event ChangePositionDelegate MoveInitiated
        {
            add
            {
                _moveInitiated += value;
            }
            remove
            {

                _moveInitiated -= value;
            }
        }
        public event CrossingDelegate CrossingCreationInitiated
        {
            add { _crossingInitiated += value; }
            remove { _crossingInitiated -= value; }
        }
        CrossingDelegate _crossingInitiated;

        public void OnCheckpointTaken(object sender, CellsEventArgs args)
        {
            ShowHint("Checkpoint {0} was taken!", (Checkpoint)args._cells[0]);
            PrintCells(args._cells);
        }

        public void OnHiddenChkpFound(object sender, CellsEventArgs args)
        {
            PrintMessage("Nearest checkpoint is in {0} cells distance on {1} and has coord {2}!", args._len[0], args._direct[0], args._cells[0]);
            PrintCells(args._cells[0]);
        }

        public void OnPersonMoved(object sender, ChangePositionEventArgs args)
        {
            //PrintNullCell(args.OldCoord);
            PrintCell(args.OldCoord, args.OldCell);
            PrintCells(args.NewCell);
        }

        public void OnCrossingCreated(object sender, CellsEventArgs args)
        {
            PrintCells(args._cells);
        }
        #endregion

        public void ShowResults(GameResults gr)
        {
            PrintMessage("Scored: {0} points in time {1}", gr.Score, gr.SecondsPassed);
        }
        public void LaunchNewGame(Map map)
        {
            PrintMap(map);
            GetUserInput();
        }

        public event GameControlDelegate StartGame;


        public bool exit
        {
            get
            {
                return _exit;
            }
            set
            {
                _exit = value;
            }
        }
        private bool _exit = false;
    }
}
