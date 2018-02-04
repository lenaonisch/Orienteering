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
            //lblMessage.ForeColor = Color.Black;
            MessageBox.Show(String.Format(format, args), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //lblMessage.Text = String.Format(format, args);
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

        public void ShowResults(GameResults gr)
        {
            PrintWarning("Scored: {0} points in time {1}", gr.Score, gr.SecondsPassed);
        }
        #endregion

        #region WF events
        private void frmMain_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    GameControlEventArgs gcargs = new GameControlEventArgs();
                    _endGame(this, ref gcargs);
                    break;
                case Keys.Down:   
                case Keys.Up:
                case Keys.Left:   
                case Keys.Right:
                    lblMessage.Text = "";
                    Key key;
                    Enum.TryParse(e.KeyData.ToString(), out key);
                    if (e.Shift)
                    {
                        switch (e.KeyValue)
                        {
                            case 37:
                                key = Key.Left;
                                break;
                            case 38:
                                key = Key.Up;
                                break;
                            case 39:
                                key = Key.Right;
                                break;
                            case 40:
                                key = Key.Down;
                                break;
                        }
                        _crossingInitiated(this, new CrossingEventArgs(key) { Type = CrossingType.Rope});
                    }
                    else
                    {
                        _moveInitiated(this, new ChangePositionEventArgs(key));
                    }
                    break;
                default:
                    break;
            }
        }

        private void startNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameControlEventArgs arg = new GameControlEventArgs();
            arg.StartNew = true;
            arg.MapParameters = GetMapParameters();
            arg.NewGameType = GetNewGameType();
            _startGame(null, ref arg);
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

        public event CrossingDelegate CrossingCreationInitiated
        {
            add { _crossingInitiated += value; }
            remove { _crossingInitiated -= value; }
        }
        CrossingDelegate _crossingInitiated;
        #endregion

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
                            if ((cell as Checkpoint).Taken)
                            {
                                c = Color.DarkGoldenrod;
                                sVal = "X";
                            }
                            else
                            {
                                c = Color.Yellow;
                                sVal = "C";
                            }
                        }
                        if (cell is Crossing)
                        {
                            c = Color.SandyBrown;
                            sVal = "+";
                        }
                        if (cell is Obstacle)
                        {
                            switch ((cell as Obstacle).Type)
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
                else
                {
                    sVal = "*";
                }
            }

            gvcell.Style.BackColor = c;
            gvcell.Value = sVal;
        }
        public void PrintCell(Coord c, Cell cell)
        {
            PrintCell(cell, GetCellByCoord(c));
        }

        public void PrintMap(Map map)
        {
            gridMap.Create((int)map.Width, (int)map.Height);

            for (uint x = 0; x < map.Size.x; x++)
            {
                for (uint y = 0; y < map.Size.y; y++)
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

        public void PrintCells(Map map, params Coord[] coord)
        {
            foreach (Coord c in coord)
            {
                PrintCell(map[c], GetCellByCoord(c));
            }  
        }

        private DataGridViewCell GetCellByCoord(Coord c)
        {
            return gridMap.Rows[(int)c.y].Cells[(int)c.x];
        }
        #endregion

        #region event handlers
        public void OnCheckpointTaken(object sender, CellsEventArgs args)
        {
            
            PrintMessage("Checkpoint {0} was taken!", (Checkpoint)args._cells[0]);
            //ShowHint("Checkpoint {0} was taken!", (Checkpoint)args._cells[0]);
            PrintCells(args._cells);
        }

        public void OnHiddenChkpFound(object sender, CellsEventArgs args)
        {
            ShowHint("Nearest checkpoint is in {0} cells distance on {1} and has coord {2}!", args._len[0], args._direct[0], args._cells[0]);
            //PrintMessage("Nearest checkpoint is in {0} cells distance on {1} and has coord {2}!", args._len[0], args._direct[0], args._cells[0]);
            PrintCells(args._cells);
        }

        public void OnPersonMoved(object sender, ChangePositionEventArgs args)
        {
            PrintCell(args.OldCoord, args.OldCell);
            PrintCells(args.NewCell);
        }

        public void OnCrossingCreated(object sender, CellsEventArgs args)
        {
            PrintCells(args._cells);
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

        private void gridMap_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in gridMap.SelectedCells)
            {
                cell.Selected = false;
            }
        }
    }

    class BufferedDataGridView : DataGridView
    {
        public BufferedDataGridView()
        {
            DoubleBuffered = true;
        }

        private void CreateCols(int width)
        {
            RowHeadersWidth = 50;
            if (ColumnCount < width)
            {
                DataGridViewColumn[] cols = new DataGridViewColumn[width];
                for (int i = 0; i < width - ColumnCount; i++)
                {
                    cols[i] = new DataGridViewTextBoxColumn();
                    cols[i].Width = 25;
                    cols[i].HeaderText = i.ToString();
                }
                Columns.AddRange(cols);
            }
            else
            {
                for (int i = ColumnCount; i > width; i--) //// test!!
                {
                    Columns.RemoveAt(i);
                }
            }
            Width = width * 25 + RowHeadersWidth + 15;
        }
        private void CreateRows(int height)
        {
            if (RowCount < height)
            {
                string[] template = new string[] { "", "" };
                for (int i = RowCount; i < height; i++)
                {
                    Rows.Add(template);
                    Rows[i].HeaderCell.Value = i.ToString();
                }
            }
            else
            {
                for (int i = RowCount; i > height; i--) //// test!!
                {
                    Rows.RemoveAt(i);
                }
            }

            Height = height * Rows[0].Height + ColumnHeadersHeight + 25;
        }
        public void Create(int width, int height)
        {
            CreateCols(width);
            CreateRows(height);
        }

    }
}
