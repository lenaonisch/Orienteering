using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

using Orienteering;

namespace WFApp
{
    public partial class frmMain : Form, IView 
    {
        public frmMain()
        {
            InitializeComponent();
            
        }

        public void PrintMessage(string format, params object[] args)
        {
            MessageBox.Show(String.Format(format, args), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void PrintError(string format, params object[] args)
        {
            MessageBox.Show(String.Format(format, args), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void PrintWarning(string format, params object[] args)
        {
            MessageBox.Show(String.Format(format, args), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public bool GetYesNoAnswer(string format, params object[] args)
        {
            if (MessageBox.Show(String.Format(format, args), "Yes/No", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void frmMain_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.N:
                    _owner.InitNew();
                    break;
                case Keys.Escape:

                    //_owner.OnGameEnded(this, new EndGameEventArgs());
                    PrintMessage("Game is finished!");

                    if (GetYesNoAnswer("Do you want to restart with the same parameters?"))
                    {
                        _owner.InitNew();
                    }
                    break;
                case Keys.Down:
                    _move(this, new ChangePositionEventArgs(Key.Down));
                    break;
                case Keys.Up:
                    _move(this, new ChangePositionEventArgs(Key.Up));
                    break;
                case Keys.Left:
                    _move(this, new ChangePositionEventArgs(Key.Left));
                    break;
                case Keys.Right:
                    _move(this, new ChangePositionEventArgs(Key.Right));
                    break;
                default:
                    break;
            }
        }

        #region events  
        public event ChangePositionDelegate MakeMove
        {
            add
            {
                _move += value;
            }
            remove
            {
                _move -= value;
            }
        }

        ChangePositionDelegate _move;
        #endregion

        Game _owner = null;
        SuperController _sc = null;

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

        private void PrintCell(Cell cell, DataGridViewCell gvcell)
        {
            string sVal = ".";
            Color c = Color.White;
            if (cell != null)
            {
                if (cell.Visible)
                {

                    if (cell is Person)
                    {
                        c = Color.Red;
                        sVal = "P";
                    }
                    else
                    {
                        if (cell is Checkpoint)
                        {
                            c = Color.Yellow;
                            sVal = "C";
                        }
                        else
                        {
                            switch ((cell as Obstacle)._type)
                            {
                                case ObstacleType.River:
                                    c = Color.Blue;
                                    sVal = "W";
                                    break;
                                case ObstacleType.Swamp:
                                    c = Color.DarkGreen;
                                    sVal = "S";
                                    break;
                                case ObstacleType.Tree:
                                    c = Color.Green;
                                    sVal = "T";
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                gvcell.Value = " ";
            }

            gvcell.Style.BackColor = c;
            gvcell.Value = sVal;
        }

        public void PrintMap(Map map)
        {
            if (gridMap.RowCount == 0)
            {
                gridMap.Columns.AddRange(new DataGridViewColumn[map.Width]);
                gridMap.Rows.AddRange(new DataGridViewRow[map.Height]);
            }
            for (uint x = 0; x < map.size.x; x++)
            {
                for (uint y = 0; y < map.size.y; y++)
                {
                    PrintCell(map[y, x], gridMap.Rows[(int)y].Cells[(int)x]);
                }
            }
        }

        public void ReprintChangedCells(params Cell[] cells)
        {
            throw new NotImplementedException();
        }

        //public System.Windows.Input.Key GetUserInput()
        //{
        //    throw new NotImplementedException();
        //}

        //public MapParams MapParameters
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public void OnCheckpointTaken(object sender, CellsEventArgs args)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnGameEnded(object sender, EndGameEventArgs args)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnHiddenChkpFound(object sender, CellsEventArgs args)
        //{
        //    throw new NotImplementedException();
        //}


        public void ShowHint(string hint, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void GetUserInput()
        {
            System.Threading.Thread.Sleep(20);
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
                    GameControlEventArgs arg = new GameControlEventArgs();
                    _endGame(this, ref arg);
                    kRet = Key.Escape;
                    break;
                default:
                    Enum.TryParse(keyInfo.Key.ToString(), out kRet);
                    break;
            }

            //return kRet;
        }

        public MapParams MapParameters
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void OnCheckpointTaken(object sender, CellsEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnGameEnded(object sender, ref GameControlEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnHiddenChkpFound(object sender, CellsEventArgs args)
        {
            throw new NotImplementedException();
        }

        public GameType GetNewGameType()
        {
            throw new NotImplementedException();
        }

        public MapParams GetMapParameters()
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

        private void btnStartNew_Click(object sender, EventArgs e)
        {

        }


        public void OnPersonMoved(object sender, ChangePositionEventArgs args)
        {
            throw new NotImplementedException();
        }


        public event ChangePositionDelegate MoveInitiated;
    }
}
