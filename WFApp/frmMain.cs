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

        #region print messages, hints etc.
        public void PrintMessage(string format, params object[] args)
        {
            lblMessage.ForeColor = Color.Black;
            //MessageBox.Show(String.Format(format, args), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblMessage.Text = String.Format(format, args);
        }

        public void PrintError(string format, params object[] args)
        {
            MessageBox.Show(String.Format(format, args), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void PrintWarning(string format, params object[] args)
        {
            MessageBox.Show(String.Format(format, args), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // убрать в отдельный контрол!!!!
        public void ShowHint(string hint, params object[] args)
        {
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = String.Format(hint, args);
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
        #endregion

        #region WF events
        private void frmMain_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    GameControlEventArgs gcargs = new GameControlEventArgs();
                    _endGame(this, ref gcargs);
                    break;
                case Keys.Down:
                    lblMessage.Text = "";
                    _moveInitiated(this, new ChangePositionEventArgs(Key.Down));
                    break;
                case Keys.Up:
                    lblMessage.Text = "";
                    _moveInitiated(this, new ChangePositionEventArgs(Key.Up));
                    break;
                case Keys.Left:
                    lblMessage.Text = "";
                    _moveInitiated(this, new ChangePositionEventArgs(Key.Left));
                    break;
                case Keys.Right:
                    lblMessage.Text = "";
                    _moveInitiated(this, new ChangePositionEventArgs(Key.Right));
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region events from IView
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

        GameControlDelegate _startGame;
        public event GameControlDelegate StartGame
        {
            add
            {
                _startGame += value;
            }
            remove
            {
                _startGame -= value;
            }
        }
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
        #endregion

        //Game _owner = null;

        //public Game CurrentGame
        //{
        //    get
        //    {
        //        return _owner;
        //    }
        //    set
        //    {
        //        _owner = value;
        //    }
        //}

        #region cell & map printing, create grid...
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

        private void CreateCols(int width)
        {
            if (gridMap.ColumnCount < width)
            {
                DataGridViewColumn[] cols = new DataGridViewColumn[width];
                for (int i = 0; i < width - gridMap.ColumnCount; i++)
                {
                    cols[i] = new DataGridViewTextBoxColumn();
                    cols[i].Width = 25;
                    cols[i].HeaderText = i.ToString();
                }
                gridMap.Columns.AddRange(cols);
            }
            else
            {
                for (int i = gridMap.ColumnCount; i > width; i--) //// test!!
                {
                    gridMap.Columns.RemoveAt(i);
                }
            }
            gridMap.Width = width * 25 + gridMap.RowHeadersWidth + 15;
        }
        private void CreateRows(int height)
        {
            if (gridMap.RowCount < height)
            {
                string[] template = new string[] { "", "" };
                for (int i = gridMap.RowCount; i < height; i++)
                {
                    gridMap.Rows.Add(template);
                    gridMap.Rows[i].HeaderCell.Value = i.ToString();
                }
            }
            else
            {
                for (int i = gridMap.RowCount; i > height; i--) //// test!!
                {
                    gridMap.Rows.RemoveAt(i);
                }
            }

            gridMap.Height = height * gridMap.Rows[0].Height + gridMap.ColumnHeadersHeight+25;  
        }
        private void CreateGrid(int width, int height)
        {
            CreateCols(width);
            CreateRows(height);
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

        public void PrintCells(params Cell[] cells)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                PrintCell(cells[i], GetCellByCoord(cells[i].Position));
            }
        }

        public void PrintNullCell(Coord coord) // print cell with background
        {
            PrintCell(null, GetCellByCoord(coord));
        }

        private DataGridViewCell GetCellByCoord(Coord c)
        {
            return gridMap.Rows[(int)c.y].Cells[(int)c.x];
        }
        #endregion

        #region event handlers
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
            PrintNullCell(args.OldCoord);
            PrintCells(args.NewCell);
        }
        #endregion

        #region get & set parameters...
        private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _parametersDialog.ShowDialog();
        }
        public GameType GetNewGameType()
        {
            return _parametersDialog.GetGameType();
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
            _startGame(null, ref arg);
        }


        public void LaunchNewGame(Map map)
        {
            PrintMap(map);
        }


        public bool exit
        {
            get
            {
                return false;
            }
            set
            {
                if (value == true)
                {
                    this.Close();
                }
            }
        }


        public void ShowResults(GameResults gr)
        {
            PrintWarning("Scored: {0} points in time {1}", gr.Score, gr.SecondsPassed);
        }
    }

    class BufferedDataGridView : DataGridView
    {
        public BufferedDataGridView()
        {
            DoubleBuffered = true;
        }
    }
}
