using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orienteering
{
    public class SuperController
    {
        public SuperController(IView view)
        {
            _view = view;
            Active = false;
        }

        public void CreateNewGame()
        {
            CreateNewGame(_view.GetNewGameType(), _view.GetMapParameters());
        }

        private void CreateNewGame(GameType gt, MapParams parameters)
        {
            switch (gt)
            {
                case GameType.Maze:
                    _game = new GameMaze(/*_view*/);
                    break;
                case GameType.Orienteering:
                    _game = new GameOrienteering(/*_view*/);
                    break;
                default:
                    break;
            }
            _game.InitNew(parameters);
            _view.CurrentGame = _game;
            _view.PrintMap(_game.Map);
            Active = true;
        }

        // returns true, if new game should be started after this one ended
        public void PlayTheGame()
        {
            Key key;
            _game.CheckpointWasTaken += _view.OnCheckpointTaken;
            _game.FoundSurrondingCheckpoint += _view.OnHiddenChkpFound;

            // SuperController is subscribed on both events from view & game
            _game.EndGame += ProcessNewRound;
            _view.EndGame += ProcessNewRound;
            /////

            do
            {  
                key = _view.GetUserInput();
                switch (key)
                {
                    case Key.Up:
                    case Key.Down:
                    case Key.Left:
                    case Key.Right:
                        if (_game.MakeMove(Offset.Get(key)))
                        {
                            _view.PrintMap(_game.Map);
                        }
                       break;
                }
            }
            while (Active);
        }

        public void ProcessNewRound(object sender, ref EndGameEventArgs args)
        {
            if (sender != null)
            {
                _view.OnGameEnded(sender, ref args);
            }
            if (args != null && args.StartNew)
            {
                CreateNewGame();
                PlayTheGame();
            }
            else  
            {
                Active = false;
                //_game = null;             //// ????
                //_view.CurrentGame = null; //// ????
            }
        }

        Game _game = null;
        IView _view = null;
        public bool Active { get; set; }
    }
}
