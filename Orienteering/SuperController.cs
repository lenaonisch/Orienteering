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
            _view.EndGame += OnEndGame;
            _view.StartGame += ProcessNewRound;
            Active = false;
        }

        public void CreateNewGame()
        {
            CreateNewGame(_view.GetNewGameType(), _view.GetMapParameters());
        }

        private void CreateNewGame(GameType gt, MapParams parameters)
        {
            Unsubscribe();
            switch (gt)
            {
                case GameType.Maze:
                    _game = new GameMaze();
                    break;
                case GameType.Orienteering:
                    _game = new GameOrienteering();
                    break;
                default:
                    throw new GameUndefinedException();
            }
            _game.InitNew(parameters);
            Active = true;
        }

        // returns true, if new game should be started after this one ended
        public void PlayTheGame()
        {
            _game.CheckpointWasTaken += _view.OnCheckpointTaken;
            _game.FoundSurrondingCheckpoint += _view.OnHiddenChkpFound;
            _game.Player.PlayerMoved += _view.OnPersonMoved;
            _view.CrossingCreationInitiated += _game.Player.MakeCross;
            _game.Player.CrossingCreated += _view.OnCrossingCreated;

            // SuperController is subscribed on both events from view & game
            _game.EndGame += OnEndGame;
            _view.MoveInitiated += _game.MakeMove;


        }

        public void Unsubscribe()
        {
            if (_game != null)
            {
                _view.MoveInitiated -= _game.MakeMove;
                _game.Player.PlayerMoved -= _view.OnPersonMoved;
                _game.CheckpointWasTaken -= _view.OnCheckpointTaken;
                _game.FoundSurrondingCheckpoint -= _view.OnHiddenChkpFound;

                _view.CrossingCreationInitiated -= _game.Player.MakeCross;
                _game.Player.CrossingCreated -= _view.OnCrossingCreated;

                _game.EndGame -= OnEndGame;
            }
        }

        public void OnEndGame(object sender, ref GameControlEventArgs args)
        {
            if (_game != null)
            {
                if (sender == _game)
                {
                    _view.ShowResults(_game.GetGameResults());
                    Active = false;
                }
                else
                {
                    if (_view.GetYesNoAnswer("Do you want to finish?"))
                    {
                        Active = false;
                    }
                }
                if (!Active)
                {
                    if (_view.GetYesNoAnswer("Do you want to start new?"))
                    {
                        args.StartNew = true;
                        args.MapParameters = _view.GetMapParameters();
                        args.NewGameType = _view.GetNewGameType();
                        ProcessNewRound(this, ref args);
                    }
                    else
                    {
                        _view.exit = true;
                    }
                }
            }
            else
            {
                if (_view.GetYesNoAnswer("Do you really want to exit?"))
                {
                    _view.exit = true;
                }
            }
        }

        public void ProcessNewRound(object sender, ref GameControlEventArgs args)
        {
            if (args != null && args.StartNew)
            {
                CreateNewGame(args.NewGameType, args.MapParameters);
                PlayTheGame();
                _view.LaunchNewGame(_game.Map);
            }
        }

        Game _game = null;
        IView _view = null;
        public bool Active { get; set; }
    }
}
