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
                    _moveInitiated(this, new ChangePositionEventArgs(Key.Down));
                    break;
                case Keys.Up:
                    _moveInitiated(this, new ChangePositionEventArgs(Key.Up));
                    break;
                case Keys.Left:
                    _moveInitiated(this, new ChangePositionEventArgs(Key.Left));
                    break;
                case Keys.Right:
                    _moveInitiated(this, new ChangePositionEventArgs(Key.Right));
                    break;
                default:
                    break;
            }
        }

        #region events 
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
        #endregion

        Game _owner = null;

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
            string sVal = " ";
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

            gvcell.Style.BackColor = c;
            gvcell.Value = sVal;
        }

        private void CreateGrid(int width, int height)
        {
            if (gridMap.RowCount == 0)
            {
                DataGridViewColumn[] cols = new DataGridViewColumn[width];
                for (int i = 0; i < width; i++)
                {
                    cols[i] = new DataGridViewTextBoxColumn();
                    cols[i].Width = 25;
                    cols[i].HeaderText = i.ToString();
                }
                gridMap.Columns.AddRange(cols);

                string[] template = new string[] { "", "" };
                DataGridViewRow[] rows = new DataGridViewRow[height];
                for (int i = 0; i < height; i++)
                {
                    gridMap.Rows.Add(template);
                    gridMap.Rows[i].HeaderCell.Value = i.ToString();
                }
                gridMap.Height = height * gridMap.Rows[0].Height + 25;
                gridMap.Width = width * 25 + gridMap.RowHeadersWidth + 15;
            }
        }

        public void PrintMap(Map map)
        {
            CreateGrid((int)map.Width, (int)map.Height);
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
            for (int i = 0; i < cells.Length; i++)
            {
                PrintCell(cells[i], GetCellByCoord(cells[i].Position));
            }
        }

        public void ShowHint(string hint, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void GetUserInput()
        {
            frmMain_KeyDown(null, new System.Windows.Forms.KeyEventArgs(Keys.None));
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
            ShowHint("Checkpoint {0} was taken!", (Checkpoint)args._cells[0]);
            ReprintChangedCells(args._cells);
        }

        public void OnGameEnded(object sender, ref GameControlEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnHiddenChkpFound(object sender, CellsEventArgs args)
        {
            PrintMessage("Nearest checkpoint is in {0} cells distance on {1} and has coord {2}!", args._len[0], args._direct[0], args._cells[0]);
            PrintCell(args._cells[0], GetCellByCoord(args._cells[0].Position));
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

        public DataGridViewCell GetCellByCoord(Coord c)
        {
            return gridMap.Rows[(int)c.y].Cells[(int)c.x];
        }

        public void OnPersonMoved(object sender, ChangePositionEventArgs args)
        {
            PrintCell(null, GetCellByCoord(args.OldCoord));
            PrintCell(args.NewCell, GetCellByCoord(args.NewCell.Position));
        }

        #region parameters...
        private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _parametersDialog.ShowDialog();
        }
        public GameType GetNewGameType()
        {
            return _parametersDialog.GetType();
        }

        public MapParams GetMapParameters()
        {
            return _parametersDialog.GetMapParameters();
        }
        frmGameParams _parametersDialog = new frmGameParams();
        #endregion

        private void startNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameControlEventArgs arg = new GameControlEventArgs();
            arg.StartNew = true;
            arg.MapParameters = GetMapParameters();
            arg.NewGameType = GetNewGameType();
            _endGame(null, ref arg);
        }
    }
}
